using HAYC_ProcessCommunicate_Library;
using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HAYC_FaceDetect_Service
{
    public partial class FaceDetectService : ServiceBase
    {
        string processName = "FaceDetect";
        string pipeName = "FaceDetectPipe";
        PipeCommunicateClient pipeClient;
        byte[] loginUserFeatCodes;

        public FaceDetectService()
        {
            InitializeComponent();
            pipeClient = new PipeCommunicateClient(pipeName);
            pipeClient.OnServerMessage += PipeClient_OnServerMessage;
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        public void OnStart()
        {
            int init = VyFaceDLL.face_sdk_init(@"E:\\vyface\\data");
            pipeClient.startClient();
            while (true)
            { }
        }
        
        private void PipeClient_OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            ProcessCommunicateMessage messageEntity = ProcessCommunicateMessage.fromJson(message);
            if (messageEntity == null)
            {
                LogHelper.WriteLog("收到pipe服务端消息："+message+"。反序列化失败");
            }
            else
            {
                switch(messageEntity.MessageType)   
                {
                    case CommunicateMessageType.COMMAND:
                        dealCommand(messageEntity);
                        break;
                    case CommunicateMessageType.SCORE://请求计算人脸对比得分
                        dealScoreCommand(messageEntity);
                        break;
                    case CommunicateMessageType.FEAT://请求计算人脸特征码
                        dealFeatCommand(messageEntity);
                        break;
                    case CommunicateMessageType.START://请求开始识别
                        dealStartCommand(messageEntity);
                        break;
                    case CommunicateMessageType.STOP://请求停止识别
                        dealStopCommand(messageEntity);
                        break;
                    case CommunicateMessageType.SPEECHRESULT://语音识别结果
                        dealSpeechResult(messageEntity);
                        break;
                    case CommunicateMessageType.MESSAGE://普通文本消息
                        dealMessage(messageEntity);
                        break;
                    default:
                        LogHelper.WriteLog("未指定的消息类型");
                        break;
                }
            }
        }

        private void dealCommand(ProcessCommunicateMessage message)
        { }

        /// <summary>
        /// 按照远程调用的message内容执行操作，此处message为图片路径，返回图片和基准图片的对比得分
        /// </summary>
        /// <param name="message"></param>
        private void dealScoreCommand(ProcessCommunicateMessage message)
        {
            //byte[] featcodes1 = new byte[640];
            byte[] currentFeatCodes = new byte[640];
            int feat1 = VyFaceDLL.face_sdk_extract(@"E:\\vyface\\TestImgSource\\seal.jpg", ref loginUserFeatCodes[0]);
            int feat2 = VyFaceDLL.face_sdk_extract(message.Message, ref currentFeatCodes[0]);
            if (feat1 == 0 && feat2 == 0)
            {
                float score = VyFaceDLL.face_sdk_compare(ref loginUserFeatCodes[0], ref currentFeatCodes[0], 640);
                ProcessCommunicateMessage messageEntity = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.SCORE, Message = score.ToString() };
                pipeClient.sendMessage(messageEntity.toJson());
            }
            else
            {

            }
        }
        /// <summary>
        /// 按照远程调用的message内容执行操作，此处message为图片路径，返回图片特征码
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void dealFeatCommand(ProcessCommunicateMessage message)
        {
            byte[] featcodes = new byte[640];
            int feat = VyFaceDLL.face_sdk_extract(message.Message, ref featcodes[0]);
            ProcessCommunicateMessage communicateMessage = new ProcessCommunicateMessage();
            if (feat == 0)
            {
                communicateMessage = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.FEAT, Message = string.Join(".", featcodes) };
            }
            else
            {
                communicateMessage = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.FEAT, Message = "" };
            }
            pipeClient.sendMessage(communicateMessage.toJson());
        }

        private void dealStartCommand(ProcessCommunicateMessage message)
        {
        }

        private void dealStopCommand(ProcessCommunicateMessage message)
        {
        }

        private void dealSpeechResult(ProcessCommunicateMessage message)
        {
        }

        private void dealMessage(ProcessCommunicateMessage message)
        {
        }
    }
}
