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
using System.Windows.Forms;

namespace HAYC_FaceDetect_Service
{
    public partial class FaceDetectService : ServiceBase
    {
        Thread threadforwork;

        string processName = "FaceDetect";
        string pipeName = "FaceDetectPipe";
        PipeCommunicateClient pipeClient;
        byte[] loginUserFeatCodes;
        List<UserFaceInfo> userFaceInfoList = new List<UserFaceInfo>();

        public FaceDetectService()
        {
            InitializeComponent();
            LogHelper.WriteLog("人脸识别服务正在启动");
            try
            {
                pipeClient = new PipeCommunicateClient(pipeName);
                pipeClient.OnServerMessage += PipeClient_OnServerMessage;
                pipeClient.OnDisConnection += PipeClient_OnDisConnection;
                pipeClient.OnPipeError += PipeClient_OnPipeError;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("人脸识别服务启动异常:"+ex.Message);
            }
        }

        private void PipeClient_OnPipeError(Exception exception)
        {
            LogHelper.WriteLog("Pipe连接异常"+exception.Message);
        }

        private void PipeClient_OnDisConnection(NamedPipeConnection<string, string> connection)
        {
            LogHelper.WriteLog("Pipe连接断开" + connection.Name);
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            if (threadforwork == null)
            {
                threadforwork = new Thread(workFunction);
            }
            threadforwork.IsBackground = true;
            threadforwork.Start();
        }

        protected override void OnStop()
        {
        }

        public void OnStart()
        {
            // TODO: Add code here to start your service.
            if (threadforwork == null)
            {
                threadforwork = new Thread(workFunction);
            }
            threadforwork.IsBackground = true;
            threadforwork.Start();
        }

        public void workFunction()
        {
            try
            {
                //Debugger.Launch();
                userFaceInfoList = FaceScoreHelper.faceInfoDeserialize();
                LogHelper.WriteLog("人脸识别服务正在初始化");
                int init = VyFaceDLL.face_sdk_init(Application.StartupPath + @"\vyface\data");
                LogHelper.WriteLog("人脸识别服务初始化成功");
                pipeClient.startClient();
                while (true)
                { }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("人脸识别服务初始化异常：" + ex.Message);
            }
        }
        
        private void PipeClient_OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            LogHelper.WriteLog("收到的消息是"+message);
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
                    case CommunicateMessageType.SETTING://配置更改消息
                        dealSetting(messageEntity);
                        break;
                    default:
                        LogHelper.WriteLog("未指定的消息类型");
                        break;
                }
            }
        }

        private void dealSetting(ProcessCommunicateMessage messageEntity)
        { }

        private void dealCommand(ProcessCommunicateMessage message)
        { }

        /// <summary>
        /// 按照远程调用的message内容执行操作，此处message为图片路径，返回图片和基准图片的对比得分
        /// </summary>
        /// <param name="message"></param>
        private void dealScoreCommand(ProcessCommunicateMessage message)
        {
            try
            {
                #region 之前图片路径+原始特征码的方式
                //string[] paramArray = message.Message.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);//paramArray[0]：图片路径；paramArray[1]：系统保存的人脸特征码
                //if (paramArray.Length < 2)
                //{
                //    ProcessCommunicateMessage messageEntity = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.MESSAGE, Message = "人脸识别计算参数格式不正确" };
                //    pipeClient.sendMessage(messageEntity.toJson());
                //    return;
                //}
                //byte[] currentFeatCodes = new byte[640];
                ////int feat1 = VyFaceDLL.face_sdk_extract(@"E:\\vyface\\TestImgSource\\seal2.jpg", ref loginUserFeatCodes[0]);
                ////string temp = string.Join(".", loginUserFeatCodes);
                ////byte[] loginUserFeatCodes1 = TranscateHelper.stringToBytes(temp, ".");
                //loginUserFeatCodes = TranscateHelper.stringToBytes(paramArray[1], ".");
                //int feat2 = VyFaceDLL.face_sdk_extract(paramArray[0], ref currentFeatCodes[0]);
                ////Debugger.Launch();
                //if (feat2 == 0)
                //{
                //    float score = VyFaceDLL.face_sdk_compare(ref loginUserFeatCodes[0], ref currentFeatCodes[0], 640);
                //    ProcessCommunicateMessage messageEntity = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.SCORE, Message = score.ToString() };
                //    pipeClient.sendMessage(messageEntity.toJson());
                //}
                //else
                //{
                //    ProcessCommunicateMessage messageEntity = new ProcessCommunicateMessage { ProcessName = processName, MessageType = CommunicateMessageType.MESSAGE, Message = "人脸识别计算失败" };
                //    pipeClient.sendMessage(messageEntity.toJson());
                //}
                #endregion
                #region 新的只发图片路径，原始特征码由服务以反序列化获取的方式
                string imagePath = message.Message;
                float maxScore = 0;
                string userName = string.Empty;//最相似的人的用户名
                foreach (UserFaceInfo ufi in userFaceInfoList)
                {
                    byte[] currentFeatCodes = new byte[640];
                    loginUserFeatCodes = TranscateHelper.stringToBytes(ufi.faceCode, ".");
                    int feat2 = VyFaceDLL.face_sdk_extract(imagePath, ref currentFeatCodes[0]);
                    //Debugger.Launch();
                    if (feat2 == 0)
                    {
                        float score = VyFaceDLL.face_sdk_compare(ref loginUserFeatCodes[0], ref currentFeatCodes[0], 640);
                        if (score > maxScore)
                        {
                            maxScore = score;
                            userName = ufi.userName;
                        }
                    }
                }
                //Debugger.Launch();
                ProcessCommunicateMessage messageEntity = new ProcessCommunicateMessage
                {
                    ProcessName = processName,
                    MessageType = CommunicateMessageType.SCORE,
                    Message = userName + "@" + maxScore.ToString()
                };
                LogHelper.WriteLog("发送人脸识别结果"+messageEntity.toJson());
                pipeClient.sendMessage(messageEntity.toJson());
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("计算人脸对别得分时出现异常："+ex.Message);
            }
        }
        /// <summary>
        /// 按照远程调用的message内容执行操作，此处message为图片路径，返回图片特征码
        /// </summary>
        /// <param name="message"></param>
        private void dealFeatCommand(ProcessCommunicateMessage message)
        {
            try
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
            catch (Exception ex)
            {
                LogHelper.WriteLog("计算人脸特征码时出现异常：" + ex.Message);
            }
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
