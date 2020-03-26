using HAYC_ProcessCommunicate_Library;
using HAYC_Speech_Service.HotKey;
using HAYC_Speech_Service.Voice;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace HAYC_Speech_Service
{
    public class SpeechWorker
    {
        #region 键盘监控相关参数
        private const int SpaceKey = 32;
        private const int LeftAltKey = 164;
        private const int RightAltKey = 165;
        private bool SpaceKeyPress = false;//是否按下空格键
        private bool AltKeyPress = false;//是否按下Alt键
        private KeyEventHandler keyDownEventHandeler = null;//按键按下钩子
        private KeyEventHandler keyUpEventHandeler = null;//按键抬起钩子
        private KeyboardHook k_hook = new KeyboardHook();
        #endregion

        #region 声音采集相关参数
        private WaveInEvent wie = new WaveInEvent();// 声音采集器
        private string wakeUpKeyword = "小安小安";
        private byte[] data = new byte[] { };//存储采集到的声音数据
        private byte[] currentData = new byte[] { };//本次要识别的声音数据
        private int currentUnkownTime = 0;//当前连续说出几次未识别命令的次数
        private int endPickAdditional;//用户输入声音变小后，继续采集的次数。用于解决声音尾音采集不全的问题
        private bool isPicking = false;//是否正在采集，用于控制在endpick并识别时，不再进行采集声音
        private bool isWakeUp = false;//是否是唤醒状态，拥有控制是否执行命令动作
        #endregion

        #region 进程交互相关参数
        public string processName = "系统登录";
        //CommunicateWorker communicateWorker;
        public PipeCommunicateClient communicateWorker;

        public bool IsPicking
        {
            get { return isPicking; }
            set { isPicking = value; }
        }

        public bool IsWakeUp
        {
            get{ return isWakeUp;}
            set
            {
                isWakeUp = value;
                if (value == false && communicateWorker!=null)
                {
                    LogHelper.WriteLog("*********开始休眠*************");
                    ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = "开始休眠" };
                    communicateWorker.sendMessage(message.toJson());
                }
            }
        }
        #endregion

        public SpeechWorker(PipeCommunicateClient _communicateWorker)
        {
            communicateWorker = _communicateWorker;
            AsrWoker.init();
            MicVolumnPicker.init();
            Speaker.init(this);
            wie.WaveFormat = new WaveFormat(16000,16,1);
            useLocalSetting();
            wie.DeviceNumber = MicVolumnPicker.getRespeakerIndexByWaveInEvent();
            wie.DataAvailable += Wie_DataAvailable;
            wie.RecordingStopped += Wie_RecordingStopped;
        }

        public void useLocalSetting()
        {
            wie.BufferMilliseconds = LocalParams.BufferMilliseconds;
            endPickAdditional = LocalParams.EndPickAdditional;
        }

        /// <summary>
        /// 清理工作
        /// </summary>
        public void dispose()
        {
            k_hook = null;
            MicVolumnPicker.dispose();
            Speaker.dispose();
            AsrWoker.logOut();
        }

        private void Wie_RecordingStopped(object sender, StoppedEventArgs e)
        {

        }

        private void Wie_DataAvailable(object sender, WaveInEventArgs e)
        {
            float volumn = MicVolumnPicker.getVolumn();
            //LogHelper.WriteLog("采集到是音量为" + volumn.ToString());
            if (volumn > LocalParams.VolumnCommandThreshold)//当大声说话时
            {
                MicVolumnPicker.lastLoudlyTime = DateTime.Now;//更新上次大声说话时间
                if (currentData.Length == 0)//如果时是开始大声说话，补齐前三次DataAvailable采集的数据。以防止声音采集不全
                {
                    currentData = currentData.Concat(data.Take(-96000)).ToArray();
                }
                currentData = currentData.Concat(e.Buffer).ToArray();//正常追加数据
            }
            if (volumn < LocalParams.VolumnSleepThreshold
                && currentData.Length > 0
                && (DateTime.Now - MicVolumnPicker.lastLoudlyTime).Milliseconds > 300
                && (DateTime.Now - MicVolumnPicker.lastLoudlyTime).Milliseconds < 500)//如果声音过小&&之前有过大声说话&&声音过小持续了300-500毫秒，说明一句话说完了
            {
                endPickAdditional--;//再数据后追加补齐endPickAdditional次DataAvailable采集的数据。以防止声音采集不全
                if (endPickAdditional > 0)
                {
                    currentData = currentData.Concat(e.Buffer).ToArray();
                }
                if (endPickAdditional == 0)//补齐结束后，进行识别
                {
                    if (currentData.Length > 32000 * (1000 / LocalParams.BufferMilliseconds) * 3)
                    {
                        currentData = currentData.Take(-32000 * (1000 / LocalParams.BufferMilliseconds) * 3).ToArray();
                    }
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
            LogHelper.WriteLog("开始采集,isPicking:"+IsPicking.ToString());
            try
            {
                if (IsPicking == false)
                {
                    IsPicking = true;
                    data = new byte[] { };
                    currentData = new byte[] { };
                    useLocalSetting();
                    //ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.MESSAGE, ProcessName = processName, Message = "开始接收语音指令……" };
                    //communicateWorker.sendMessage(message.toJson());
                    wie.StartRecording();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("start Pick异常："+ex.Message);
                throw;
            }
        }

        public void endPick()
        {
            if (IsPicking == true)
            {
                wie.StopRecording();
                string speechResult = AsrWoker.run_asr(currentData);
                LogHelper.WriteLog("识别结果是：" + speechResult);
                if (string.IsNullOrEmpty(speechResult) == false)//如果识别出结果
                {
                    string result = getResultFromXml(speechResult);
                    currentUnkownTime = 0;
                    MicVolumnPicker.lastCommandTime = DateTime.Now;//如果识别出结果，设置最后一次听到命令的时间
                    if (result == wakeUpKeyword) //如果听到唤醒词
                    {
                        IsWakeUp = true;//设置为唤醒状态
                        Speaker.speech(Speaker.TextOnWake);
                        ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = result };
                        communicateWorker.sendMessage(message.toJson());
                    }
                    else//如果为其他指令
                    {
                        if (IsWakeUp)//如果是唤醒状态，则执行命令；否则什么都不做
                        {
                            Speaker.speech(Speaker.TextOnCommand);
                            ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = result };
                            communicateWorker.sendMessage(message.toJson());
                        }
                        else
                        {
                            //如果在睡眠状态下识别到其他指令，则发聩空字符串，接收者什么都不做
                            ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = string.Empty };
                            communicateWorker.sendMessage(message.toJson());
                        }
                    }
                }
                else //如果识别不出结果，则提示未能识别出语音命令
                {
                    if (IsWakeUp)
                    {
                        Speaker.speech(Speaker.TextOnUnkown);
                        currentUnkownTime++;
                        if (currentUnkownTime >= LocalParams.UnKownTimes && this.IsWakeUp)//如果连续未识别命令达到规定次数，则进入休眠
                        {
                            this.IsWakeUp = false;
                        }
                        else//如果未到规定次数，则按普通处理，反馈未能识别命令
                        {
                            ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = "未能识别命令"};
                            communicateWorker.sendMessage(message.toJson());
                        }
                    }
                    else//如果在睡眠状态下识别不出指令，则发聩空字符串，接收者什么都不做
                    {
                        ProcessCommunicateMessage message = new ProcessCommunicateMessage() { MessageType = CommunicateMessageType.SPEECHRESULT, ProcessName = processName, Message = string.Empty };
                        communicateWorker.sendMessage(message.toJson());
                    }
                }
            }
            IsPicking = false;
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
