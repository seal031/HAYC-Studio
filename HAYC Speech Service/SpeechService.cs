using HAYC_ProcessCommunicate_Library;
using HAYC_Speech_Service.Voice;
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
using System.Windows.Forms;

namespace HAYC_Speech_Service
{
    public partial class SpeechService : ServiceBase, IdoJobWorker
    {
        string processName = "Speech";
        string pipeName = "SpeechPipe";
        PipeCommunicateClient pipeClient;
        SpeechWorker speechWorker;
        Thread threadforwork;

        IScheduler scheduler;
        IJobDetail job;

        public SpeechService()
        {
            InitializeComponent();
        }

        private void workFunction()
        {
            LogHelper.setConsole(0);
            //LogHelper.WriteLog("开始执行workFunction");
            //LogHelper.WriteLog(System.Windows.Forms.Application.StartupPath);
            pipeClient = new PipeCommunicateClient(pipeName);
            pipeClient.OnServerMessage += PipeClient_OnServerMessage;
            speechWorker = new SpeechWorker(pipeClient);

            pipeClient.startClient();
            startJob();
            speechWorker.startPick();
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
            LogHelper.WriteLog("开始执行OnStop");
            //执行清理工作
            if (threadforwork != null)
            {
                if (threadforwork.ThreadState == System.Threading.ThreadState.Running)
                {
                    threadforwork.Abort();
                }
            }
            stopJob();
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
