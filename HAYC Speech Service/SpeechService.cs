using HAYC_ProcessCommunicate_Library;
using NamedPipeWrapper;
using Quartz;
using Quartz.Impl;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace HAYC_Speech_Service
{
    public partial class SpeechService : ServiceBase, IdoJobWorker
    {
        string processName = "Speech";
        string pipeName = "SpeechPipe";
        PipeCommunicateClient pipeClient;
        SpeechWorker speechWorker;
        Thread threadforwork;
        Thread threadforSocket;

        IScheduler scheduler;
        IJobDetail job;

        public SpeechService()
        {
            InitializeComponent();
        }
        private void workFunction()
        {
            //Debugger.Launch();
            LogHelper.setConsole(0);
            //LogHelper.WriteLog("开始执行workFunction");
            //LogHelper.WriteLog(System.Windows.Forms.Application.StartupPath);
            pipeClient = new PipeCommunicateClient(pipeName);
            pipeClient.OnServerMessage += PipeClient_OnServerMessage;
            speechWorker = new SpeechWorker(pipeClient,this);

            pipeClient.startClient();
            startJob();
            speechWorker.startPick(); 
        }
        private void socketFunction()
        {
            try
            {
                SocketServer.init();
                SocketServer.server.NewSessionConnected += Server_NewSessionConnected;
                SocketServer.server.NewRequestReceived += Server_NewRequestReceived;
                SocketServer.server.SessionClosed += Server_SessionClosed;
                SocketServer.start();
                LogHelper.WriteLog("socket初始化及启动成功");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("socket初始化及启动异常：" + ex.Message);
            }
        }

        private void Server_SessionClosed(SuperSocket.SocketBase.AppSession session, SuperSocket.SocketBase.CloseReason value)
        {
            LogHelper.WriteLog("由"+session.RemoteEndPoint.Port+ "端口连入的socket客户端已经断开连接：");
        }

        private void Server_NewRequestReceived(SuperSocket.SocketBase.AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            LogHelper.WriteLog("收到由"+session.RemoteEndPoint.Port+"端口连入的socket客户端发来的消息"+requestInfo.Key);
            dealSocketCommand(requestInfo.Key);
        }

        private void Server_NewSessionConnected(SuperSocket.SocketBase.AppSession session)
        {
            LogHelper.WriteLog("由" + session.RemoteEndPoint.Port + "端口连入的socket客户端已经连接成功：");
            SocketServer.session = session;//连接后，将session对象赋给SocketServer的静态session对象
        }

        private void stopSocket()
        {
            try
            {
                SocketServer.stop();
                SocketServer.session = null;
                SocketServer.server.Dispose();
                SocketServer.server = null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("清理socket发生异常："+ex.Message);
            }
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
            //if (threadforSocket == null)
            //{
            //    threadforSocket = new Thread(socketFunction);
            //}
            //threadforSocket.IsBackground = true;
            //threadforSocket.Start();
        }

        protected override void OnStop()
        {
            LogHelper.WriteLog("开始执行OnStop");
            //执行清理工作
            if (threadforwork != null)
            {
                if (threadforwork.ThreadState == System.Threading.ThreadState.Running)
                {
                    threadforwork.Abort();
                }
            }
            if (threadforSocket != null)
            {
                if (threadforSocket.ThreadState == System.Threading.ThreadState.Running)
                {
                    threadforSocket.Abort();
                }
            }
            stopJob();
            stopSocket();
            speechWorker.dispose();
            speechWorker = null;
            pipeClient.stopClient();
            pipeClient = null;

            //System.Environment.Exit(1);
        }
        public void OnStart()
        {
            workFunction();
            while (true)
            { }
        }

        private void PipeClient_OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            ProcessCommunicateMessage messageEntity = ProcessCommunicateMessage.fromJson(message);
            if (messageEntity == null)
            {
                LogHelper.WriteLog("收到pipe服务端消息：" + message + "。反序列化失败");
            }
            else
            {
                switch (messageEntity.MessageType)
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
                    case CommunicateMessageType.SETTING://设置信息
                        dealSetting(messageEntity);
                        break;
                    default:
                        LogHelper.WriteLog("未指定的消息类型");
                        break;
                }
            }
        }

        private void dealCommand(ProcessCommunicateMessage message)
        {

        }
        private void dealScoreCommand(ProcessCommunicateMessage message)
        {

        }
        private void dealFeatCommand(ProcessCommunicateMessage message)
        {

        }
        private void dealStartCommand(ProcessCommunicateMessage message)
        {
            LogHelper.WriteLog("接收到pipe指令，开始startPick");
            speechWorker.startPick();
        }
        private void dealStopCommand(ProcessCommunicateMessage message)
        {
            //speechWorker.endPick();
        }

        private void dealSpeechResult(ProcessCommunicateMessage message)
        {

        }

        private void dealMessage(ProcessCommunicateMessage message)
        {

        }

        private void dealSetting(ProcessCommunicateMessage messageEntity)
        {
            var settingString = messageEntity.Message;
            SpeechSetting setting = SpeechSetting.fromJson(settingString);
            if (settingString != null)
            {
                LocalParams.BufferMilliseconds = setting.BufferMilliseconds;
                LocalParams.EndPickAdditional = setting.EndPickAdditional;
                LocalParams.MicVolumnPickerSleepSecond = setting.MicVolumnPickerSleepSecond;
                LocalParams.VolumnCommandThreshold = setting.VolumnCommandThreshold;
                LocalParams.VolumnSleepThreshold = setting.VolumnSleepThreshold;
                LocalParams.UnKownTimes = setting.UnKownTimes;
                LogHelper.WriteLog("设置内容生效。设置内容为："+messageEntity.Message);
                //设置更新到局部变量后，让局部变量重新赋值给程序。
                speechWorker.useLocalSetting();
            }
            else
            {
                LogHelper.WriteLog("设置内容无效。设置消息内容为：" + messageEntity.Message);
            }
        }
        /// <summary>
        /// 处理socket消息
        /// </summary>
        /// <param name="body"></param>
        private void dealSocketCommand(string body)
        {
            ProcessCommunicateMessage messageEntity = ProcessCommunicateMessage.fromJson(body);
            if (messageEntity == null)
            {
                LogHelper.WriteLog("收到socket消息：" + body + "。反序列化失败");
            }
            else
            {
                switch (messageEntity.MessageType)
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
                    case CommunicateMessageType.SETTING://设置信息
                        dealSetting(messageEntity);
                        break;
                    default:
                        LogHelper.WriteLog("未指定的消息类型");
                        break;
                }
            }
        }

        private void startJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.worker = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("SpeechJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("SpeechTrigger", "triggers").StartAt(DateTimeOffset.Now)
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        private void stopJob()
        {
            if (job != null)
            {
                job = null;
            }
            if (scheduler != null)
            {
                scheduler.Shutdown(false);
                scheduler = null;
            }
        }

        public void doJobWork()
        {
            if ((DateTime.Now - MicVolumnPicker.lastCommandTime).TotalSeconds >= LocalParams.MicVolumnPickerSleepSecond && speechWorker.IsWakeUp == true)
            {
                speechWorker.IsWakeUp = false;
            }
        }
    }
}
