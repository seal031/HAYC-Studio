using HAYC_ProcessCommunicate_Library;
using NamedPipeWrapper;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HAYC_Speech_Service
{
    public partial class SpeechService : ServiceBase, IdoJobWorker
    {
        string processName = "Speech";
        string pipeName = "SpeechPipe";
        PipeCommunicateClient pipeClient;
        SpeechWorker speechWorker;

        IScheduler scheduler;
        IJobDetail job;

        public SpeechService()
        {
            InitializeComponent();
            pipeClient = new PipeCommunicateClient(pipeName);
            pipeClient.OnServerMessage += PipeClient_OnServerMessage;
            speechWorker = new SpeechWorker(pipeClient);
        }

        protected override void OnStart(string[] args)
        {
            
        }
        
        protected override void OnStop()
        {
        }
        public void OnStart()
        {
            pipeClient.startClient();
            startJob();
            speechWorker.startPick();
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
            speechWorker.startPick();
        }
        private void dealStopCommand(ProcessCommunicateMessage message)
        {
            speechWorker.endPick();
        }

        private void dealSpeechResult(ProcessCommunicateMessage message)
        {

        }

        private void dealMessage(ProcessCommunicateMessage message)
        {

        }

        private void startJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.worker = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("fireJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("fireTrigger", "triggers").StartAt(DateTimeOffset.Now)
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                //FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        public void doJobWork()
        {
            if ((DateTime.Now - MicVolumnPicker.lastCommandTime).TotalSeconds > 30 && speechWorker.isWakeUp == true)
            {
                speechWorker.isWakeUp = false;
                Console.WriteLine("进入睡眠状态");
            }
        }
    }
}
