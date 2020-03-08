using HAYC_ProcessCommunicate_Library;
using HAYC_Speech_Service.HotKey;
using HAYC_Speech_Service.Voice;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace HAYC_Speech_Service
{
    public class SpeechWorker
    {
        const int SpaceKey = 32;
        const int LeftAltKey = 164;
        const int RightAltKey = 165;
        
        WaveInEvent wie = new WaveInEvent();//声音采集器
        private string wakeUpKeyword = "小安小安";
        byte[] data = new byte[] { };//存储采集到的声音数据
        byte[] currentData = new byte[] { };//本次要识别的声音数据
        private bool isPicking = false;//是否正在采集，用于控制在endpick并识别时，不再进行采集声音
        public bool isWakeUp = false;//是否是唤醒状态，拥有控制是否执行命令动作
        private bool SpaceKeyPress = false;//是否按下空格键
        private bool AltKeyPress = false;//是否按下Alt键
        private KeyEventHandler keyDownEventHandeler = null;//按键按下钩子
        private KeyEventHandler keyUpEventHandeler = null;//按键抬起钩子
        private KeyboardHook k_hook = new KeyboardHook();
        public string processName = "系统登录";
        //CommunicateWorker communicateWorker;
        public PipeCommunicateClient communicateWorker;


        public SpeechWorker(PipeCommunicateClient _communicateWorker)
        {
            communicateWorker = _communicateWorker;
            AsrWoker.init();
            MicVolumnPicker.init();
            wie.WaveFormat = new WaveFormat(16000,16,1);
            wie.BufferMilliseconds = 25;
            wie.DeviceNumber = MicPicker.getRespeakerIndex();
            wie.DataAvailable += Wie_DataAvailable;
            wie.RecordingStopped += Wie_RecordingStopped;
        }

        private void Wie_RecordingStopped(object sender, StoppedEventArgs e)
        {
            startPick();
        }

        List<float> sampleAggregator = new List<float>();//用来储存波形数据
        int end = 5;
        private void Wie_DataAvailable(object sender, WaveInEventArgs e)
        {
            float volumn = MicVolumnPicker.getVolumn();
            if (volumn > MicVolumnPicker.volumnCommandThreshold)
            {
                MicVolumnPicker.lastLoudlyTime = DateTime.Now;
                if (currentData.Length == 0)
                {
                    currentData = currentData.Concat(data.Take(-64000)).ToArray();
                }
                currentData = currentData.Concat(e.Buffer).ToArray();
            }
            if (volumn < MicVolumnPicker.volumnSleepThreshold
                && currentData.Length > 0
                && (DateTime.Now - MicVolumnPicker.lastLoudlyTime).Milliseconds > 300
                && (DateTime.Now - MicVolumnPicker.lastLoudlyTime).Milliseconds < 500)
            {
                end--;
                if (end > 0)
                {
                    currentData = currentData.Concat(e.Buffer).ToArray();
                }
                if (end == 0)
                {
                    endPick();
                }
            }
            data = data.Concat(e.Buffer).ToArray();
        }

        #region 全局热键处理
        public void startKeyHookListen()
        {
            keyDownEventHandeler = new KeyEventHandler(hook_KeyDown);
            k_hook.KeyDownEvent += keyDownEventHandeler;//钩住键按下
            keyUpEventHandeler = new KeyEventHandler(hook_KeyUp);
            k_hook.KeyUpEvent += keyUpEventHandeler;//勾住键抬起
            k_hook.Start();//安装键盘钩子
        }
        public void stopKeyHookListen()
        {
            if (keyDownEventHandeler != null)
            {
                k_hook.KeyDownEvent -= keyDownEventHandeler;//取消按键按下事件
                k_hook.KeyUpEvent -= keyUpEventHandeler;//取消按键按下事件
                keyDownEventHandeler = null;
                keyUpEventHandeler = null;
                k_hook.Stop();//关闭键盘钩子
            }
        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == SpaceKey) { startPick(); }
        }
        private void hook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == SpaceKey) { endPick(); }
        }
        #endregion

        #region 声音采集及语音识别
        public void startPick()
        {
            if (isPicking == false)
            {
                isPicking = true;
                data = new byte[] { };
                currentData = new byte[] { };
                end = 5;
                ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.MESSAGE, ProcessName = processName, Message = "开始接收语音指令……" };
                wie.StartRecording();
            }
        }
        public void endPick()
        {
            if (isPicking == true)
            {
                wie.StopRecording();
                string speechResult = AsrWoker.run_asr(currentData);
                if (string.IsNullOrEmpty(speechResult) == false)//如果识别出结果
                {
                    string result = getResultFromXml(speechResult);
                    MicVolumnPicker.lastCommandTime = DateTime.Now;//如果识别出结果，设置最后一次听到命令的时间
                    if (result == wakeUpKeyword) //如果听到唤醒词
                    {
                        isWakeUp = true;//设置为唤醒状态
                        Console.WriteLine("进入唤醒状态");
                    }
                    else//如果为其他指令
                    {
                        if (isWakeUp)//如果是唤醒状态，则执行命令；否则什么都不做
                        {
                            Console.WriteLine("执行命令:"+result);
                            ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = result };
                            communicateWorker.sendMessage(message.toJson());
                        }
                    }
                }
                else //如果识别不出结果，则提示未能识别出语音命令
                {
                    ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.MESSAGE, ProcessName = processName, Message = "未能成功识别语音指令，请重新下达语音指令！" };
                    communicateWorker.sendMessage(message.toJson());
                }
                isPicking = false;
            }
        }

        private string getResultFromXml(string strXml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);
            XmlNode rootNode = xmlDoc.SelectSingleNode("nlp");
            foreach (XmlNode xxNode in rootNode.ChildNodes)
            {
                if (xxNode.Name == "rawtext")
                {
                    return xxNode.InnerText;
                }
            }
            return "";
        }
        #endregion
    }
    
}
