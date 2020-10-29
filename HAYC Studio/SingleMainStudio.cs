using DevComponents.DotNetBar;
using HAYC_ProcessCommunicate_Library;
using HAYC_Studio.Browser;
using NAudio.CoreAudioApi;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenNIWrapper;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]//COM+组件可见
    public partial class SingleMainStudio : ScreenForm, IForm, IQuartz
    {
        private const string speechServiceName = "HAYC Speech Service";
        private const string faceServiceName = "HAYC FaceDetect Service";

        public string remoteUrl = string.Empty;
        string cookieKey = "HA_LINK_LOGIN_INFO";
        public static string cookieValue = string.Empty;
        /// <summary>
        /// 是否是多屏中的主窗体，默认false。如果是主窗体，则切换场景时，触发其他窗体的切换事件
        /// </summary>
        public bool isMaster = false;
        bool speech_running = false;
        bool face_running = false;
        public WindowsServiceManager windowsServerManager;
        public LoginInfo loginInfo;
        private static List<PanelEx> wbList = new List<PanelEx>();

        //定时任务相关变量
        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;
        private Mat image2;
        
        public SingleMainStudio(LoginInfo loginInfo, string remoteUrl)
        {
            InitializeComponent();
            this.loginInfo = loginInfo;
            this.remoteUrl = remoteUrl;
            string logoImage = ConfigWorker.GetConfigValue("logoImage");
            this.pictureBox1.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(logoImage, null);
        }
        private void SingleMainStudio_Shown(object sender, EventArgs e)
        {
            init(loginInfo);
        }
        public void init(LoginInfo loginInfo, bool resume = false)
        {
            this.lbl_userName.Text = loginInfo.User.UserRealName;
            this.loginInfo = loginInfo;
            if (resume == false)//如果首次打开。如果是恢复的重新登录，则不需要执行全部方法
            {
                initSpeaker();
                initPanelWidth();
                initBBBtn_tool();
            }
            initBBBtn_scene();
            timer_checkServiceState.Start();
            OpenCamera();
            if (resume == false)//如果首次打开。初始化定时任务；否则恢复任务
            {
                initJob();
            }
            else
            {
                resumeJob();
            }
        }
        ///定时任务
        public void initJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("connectJob", "jobs").Build();
                    //每五分钟检测一次人脸
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(30))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever()).Build();
                    //string time = ConfigWorker.GetConfigValue("timeTask");
                    //ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                    //    .WithCronSchedule(time).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                    jobKey = job.Key;
                    //scheduler.DeleteJob
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }
        public void stopJob()
        {
            if (scheduler != null)
            {
                Debug.WriteLine("人脸检测定时任务停止,timer停止");
                timer_saveImage.Stop();
                scheduler.PauseJob(jobKey);
                //CloseCamera();
            }
        }
        public void pauseJob()
        {
            Debug.WriteLine("人脸检测定时timer停止");
            timer_saveImage.Stop();
        }
        public void resumeJob()
        {
            try
            {
                if (scheduler != null)
                {
                    Debug.WriteLine("人脸检测定时任务恢复");
                    scheduler.ResumeJob(jobKey);
                }
            }
            catch (Exception)
            {

            }
        }
        public delegate void initWebBrowserHandler(SceneInfo sceneInfo);
        public void initWebBrowser(SceneInfo sceneInfo)
        {
            if (this.InvokeRequired)
            {
                initWebBrowserHandler handler = new initWebBrowserHandler(initWebBrowser);
                this.Invoke(handler, sceneInfo);
            }
            else
            {
                if (isMaster)//如果是多屏，显示该场景的第一个页面
                {
                    var screenInfo = sceneInfo.ScreenList[0];//第一个页面
                    if (wbList.Count == 0)//是否第一次执行init。如果是，不需要负责副屏幕；如果不是，则说明是在切换场景状态，需要负责副屏幕
                    {
                        PanelEx panel = new PanelEx();
                        //目前先只生成IE
                        IHaycBrowser wb = new IEBrowser();
                        panel.Controls.Add((WebBrowser)wb);
                        ((WebBrowser)wb).Dock = DockStyle.Fill;
                        ((WebBrowser)wb).ObjectForScripting = this;
                        wbList.Add(panel);
                        panel_main.Controls.Add(panel);
                        panel.Dock = DockStyle.Fill;
                        wb.Navigate(remoteUrl + screenInfo.ModelURL, cookieKey, cookieValue);
                    }
                    else
                    {
                        IHaycBrowser wb;
                        PanelEx panel = wbList[0];
                        switch (screenInfo.browserType)
                        {
                            case BrowserType.IE:
                                wb = panel.Controls[0] as IHaycBrowser;
                                wb.Navigate(remoteUrl + screenInfo.ModelURL, cookieKey, cookieValue);
                                break;
                            case BrowserType.Chrome:
                                wb = new ChromeBrowser(remoteUrl + screenInfo.ModelURL);
                                break;
                            default:
                                return;
                        }
                        //为副屏幕指定url
                        for (int i = 1; i < sceneInfo.ScreenList.Count; i++)
                        {
                            screenInfo = sceneInfo.ScreenList[i];
                            try
                            {
                                var form = FormController.formInfoList[i - 1];//尝试从副屏幕列表中取出一个副屏幕
                                if (form.Form != null)
                                {
                                    form.Form.Navigate(remoteUrl + screenInfo.ModelURL, cookieKey, cookieValue);
                                }
                            }
                            catch (Exception)
                            {
                                LogHelper.WriteLog("副屏幕打开页面失败。准备打开场景"+sceneInfo.SceneName+"的第"+i+"个页面"+screenInfo.ModelName+"，但副屏幕列表中只包含"+FormController.formInfoList.Count+"个副屏幕");
                            }
                        }
                    }
                }
                else//如果是单宽屏，显示该场景下所有页面
                {
                    //创建3个wb循环使用
                    if (wbList.Count == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            PanelEx panel = new PanelEx();
                            //目前先只生成IE
                            IHaycBrowser wb;
                            wb = new IEBrowser();
                            panel.Controls.Add((WebBrowser)wb);
                            ((WebBrowser)wb).Dock = DockStyle.Fill;
                            ((WebBrowser)wb).ObjectForScripting = this;
                            wbList.Add(panel);
                            panel_main.Controls.Add(panel);
                        }
                    }

                    //panel_main.Controls.Clear();
                    int currentPanelLocationX = 0;
                    for (int i = 0; i < sceneInfo.ScreenList.Count; i++)
                    {
                        var screenInfo = sceneInfo.ScreenList[i];
                        PanelEx panel = wbList[i];
                        panel.Show();
                        panel.Location = new System.Drawing.Point(currentPanelLocationX, 0);
                        panel.Height = panel_main.Height;
                        panel.Width = (int)(panel_main.Width * screenInfo.ratio);
                        currentPanelLocationX += panel.Width + 1;
                        IHaycBrowser wb;
                        switch (screenInfo.browserType)
                        {
                            case BrowserType.IE:
                                //wb = new IEBrowser();
                                wb = panel.Controls[0] as IHaycBrowser;
                                //panel.Controls.Add((WebBrowser)wb);
                                //((WebBrowser)wb).Dock = DockStyle.Fill;
                                IntPtr pHandle = IEWebbrowserMemoryReleaser.GetCurrentProcess();
                                IEWebbrowserMemoryReleaser.SetProcessWorkingSetSize(pHandle, -1, -1);
                                wb.Navigate(remoteUrl + screenInfo.ModelURL, cookieKey, cookieValue);
                                break;
                            case BrowserType.Chrome:
                                wb = new ChromeBrowser(remoteUrl + screenInfo.ModelURL);
                                panel.Controls.Add((Control)wb);
                                ((Control)wb).Dock = DockStyle.Fill;
                                break;
                            default:
                                return;
                        }
                        //panel_main.Controls.Add(panel);
                    }
                    //隐藏多余的panel
                    for (int i = sceneInfo.ScreenList.Count; i < wbList.Count; i++)
                    {
                        PanelEx panel = wbList[i];
                        panel.Hide();
                    }
                }
            }
        }

        private void bbbtn_speech_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            if (speech_running)//启动->停止
            {
                this.bbbtn_speech.Image = global::HAYC_Studio.Properties.Resources.speech32_off;
                this.bbbtn_speech.ImageLarge = global::HAYC_Studio.Properties.Resources.speech32_off;
                bubbleBar.Refresh();
                setServiceState("HAYC Speech Service", false);
            }
            else//停止->启动
            {
                this.bbbtn_speech.Image = global::HAYC_Studio.Properties.Resources.speech32_on;
                this.bbbtn_speech.ImageLarge = global::HAYC_Studio.Properties.Resources.speech32_on;
                bubbleBar.Refresh();
                setServiceState("HAYC Speech Service", true);
            }
            speech_running = !speech_running;
        }
        private void bbbtn_face_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            if (face_running)
            {
                this.bbbtn_face.Image = global::HAYC_Studio.Properties.Resources.face32_off;
                this.bbbtn_face.ImageLarge = global::HAYC_Studio.Properties.Resources.face32_off;
                bubbleBar.Refresh();
                setServiceState("HAYC FaceDetect Service", false);
            }
            else
            {
                this.bbbtn_face.Image = global::HAYC_Studio.Properties.Resources.face32_on;
                this.bbbtn_face.ImageLarge = global::HAYC_Studio.Properties.Resources.face32_on;
                bubbleBar.Refresh();
                setServiceState("HAYC FaceDetect Service", true);
            }
            face_running = !face_running;
        }
        private void setServiceState(string serviceName, bool state)
        {
            if (windowsServerManager != null)
            {
                switch (state)
                {
                    case true:
                        windowsServerManager.StartService(serviceName);
                        break;
                    case false:
                        windowsServerManager.StopService(serviceName);
                        break;
                }
            }
        }
        private void bbbtn_setup_Click(object sender, ClickEventArgs e)
        {
            Setup setup = new Setup(windowsServerManager);
            setup.Show(this);
        }
        private void bbbtn_faceLocalInfo_Click(object sender, ClickEventArgs e)
        {
            FaceRegist fr = new HAYC_Studio.FaceRegist();
            fr.ShowDialog(this);
        }
        private void bbbtn_logout_Click(object sender, ClickEventArgs e)
        {
            if (MessageBox.Show("您确定要退出系统吗？", "退出系统", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    stopJob();
                    BeepWorker.close();
                    windowsServerManager.StopService("");
                    //CloseCamera(); //在loginFomr的formclosed事件中已经关了
                }
                catch (Exception)
                {

                }
                finally
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    System.Environment.Exit(0);
                }
            }
        }

        public void DoJavaScriptFuntion(string funcName, params object[] paramList)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        public void cyclicWork()
        {
            //OpenCamera();
            Debug.WriteLine("定时任务执行，启动timer");
            if (OpenNI.EnumerateDevices().Length == 0)//如果没有检测到深度摄像头，则不执启动人脸检测图片截取的timer
            {
                //LogHelper.WriteLog("定时任务执行，由于没有检测到深度摄像头，所以不启动timer");
            }
            else
            {
                panelEx1.Invoke((MethodInvoker)delegate { timer_saveImage.Start(); });
            }
        }

        #region 摄像头图像控制
        private Bitmap bitmap = new Bitmap(1, 1);
        private string tempJpgPath = Application.StartupPath + "\\" + ConfigWorker.GetConfigValue("tempJpnPath");
        private Device currentDevice;
        private VideoMode videoMode;
        private VideoStream currentSensor;
        private void initCamera()
        {
            HandleError(OpenNI.Initialize());
            var cameraCount = OpenNI.EnumerateDevices().Length;
            if (cameraCount > 0)
            {
                currentDevice = OpenNI.EnumerateDevices()[0].OpenDevice();
                currentSensor = currentDevice.CreateVideoStream(Device.SensorType.Color);
                videoMode = currentSensor.SensorInfo.GetSupportedVideoModes().First(m => m.DataPixelFormat == VideoMode.PixelFormat.Rgb888 && m.Resolution.Height == 480 && m.Resolution.Width == 640 && m.Fps == 30);
            }
            else
            {
                bool showBoxOnCheckHardwareFaild = ConfigWorker.GetConfigValue("showBoxOnCheckHardwareFaild") == "1" ? true : false;
                if (showBoxOnCheckHardwareFaild)
                {
                    MessageBox.Show("未能在本机上找到视频传感器设备。请确认视频传感器设备已连接到本机，并已正确安装驱动程序", "未能找到设备", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                stopJob();//如果连接不上摄像头，则不再执行定时检测人脸任务.
            }
        }
        private void HandleError(OpenNI.Status status)
        {
            if (status == OpenNI.Status.Ok)
            {
                return;
            }
            MessageBox.Show(string.Format(@"Error: {0} - {1}", status, OpenNI.LastError), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 开启摄像头
        /// </summary>
        public void OpenCamera()
        {
            initCamera();
            if (this.currentSensor != null && this.currentSensor.IsValid)
            {
                this.currentSensor.Stop();
                this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                this.currentSensor.VideoMode = videoMode;

                if (this.currentSensor.Start() == OpenNI.Status.Ok)
                {
                    this.currentSensor.OnNewFrame += this.CurrentSensorOnNewFrame;
                }
                else
                {
                    MessageBox.Show(@"Failed to start stream.");
                }
            }
        }
        public void CloseCamera()
        {
            if (this.currentSensor != null && this.currentSensor.IsValid)
            {
                this.currentSensor.Stop();
                this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                OpenNI.Shutdown();
            }
        }
        private void CurrentSensorOnNewFrame(VideoStream videoStream)
        {
            if (videoStream.IsValid && videoStream.IsFrameAvailable())
            {
                using (VideoFrameRef frame = videoStream.ReadFrame())
                {
                    if (frame.IsValid)
                    {
                        VideoFrameRef.CopyBitmapOptions options = VideoFrameRef.CopyBitmapOptions.Force24BitRgb | VideoFrameRef.CopyBitmapOptions.DepthFillShadow;
                        lock (bitmap)
                        {
                            try
                            {
                                frame.UpdateBitmap(bitmap, options);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("sms bitmap update" + ex.Message);
                                bitmap = frame.ToBitmap(options);
                            }
                            finally
                            {

                            }
                        }
                        //if (!this.pb_camera.Visible)
                        {
                            this.Invalidate();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 截图及保存
        /// </summary>
        /// <returns></returns>
        public string pickAndSaveFaceImage()
        {
            string imagePath = string.Empty;
            try
            {
                imagePath = tempJpgPath + "\\" + DateTime.Now.ToString("HHmmss") + ".jpg";
                lock (bitmap)
                {
                    bitmap.Save(imagePath, ImageFormat.Jpeg);
                }
            }
            catch (Exception)
            {
                imagePath = string.Empty;
            }
            return imagePath;
        }
        /// <summary>
        /// 定时，用于保存人脸图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_saveImage_Tick(object sender, EventArgs e)
        {
            try
            {
                //Bitmap currentBitmap = bitmap.Clone() as Bitmap;
                //pb_camera.Image = currentBitmap;
            }
            catch (Exception)
            {

            }
            string imagePath = pickAndSaveFaceImage();
            ProcessCommunicateMessage message = new ProcessCommunicateMessage();
            message.ProcessName = "";
            message.MessageType = CommunicateMessageType.SCORE;
            //message.Message = imagePath + "#" + LoginForm.userConditionCode;
            message.Message = imagePath;
            Debug.WriteLine("timer执行中，发送人脸识别请求 "+message.toJson());
            FacePipeCommunicateServerWorker.sendMessage(message.toJson());
        }
        /// <summary>
        /// 定时，用于检测服务状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_checkServiceState_Tick(object sender, EventArgs e)
        {
            initBBBtn_tool();
        }
        #endregion

        #region 场景切换
        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="sceneName"></param>
        public void switchScene(string sceneName)
        {
            var scene = loginInfo.SceneList.FirstOrDefault(s => s.SceneName == sceneName);
            if (scene == null)
            {
                MessageBox.Show("没有名为" + sceneName + "的场景");
            }
            else
            {
                initWebBrowser(scene);//方法中已经处理了作为isMaster的情况。下面只需显示副窗体即可.
                if (isMaster)
                {
                    if (scene.ScreenList.Count > ScreenWorker.getScreenCount())
                    {
                        MessageBox.Show("实际的屏幕数量少于此场景需要的屏幕数量，部分屏幕将无法显示。");
                    }
                    //取实际屏幕数和场景页面数，两者的最小值
                    int formCountToShow = scene.ScreenList.Count > ScreenWorker.getScreenCount() ? ScreenWorker.getScreenCount() : scene.ScreenList.Count;
                    for (int i = 1; i < formCountToShow; i++)
                    {
                        var screenInfo = scene.ScreenList[i];
                        ScreenForm form;
                        if (i - 1 < FormController.formInfoList.Count)
                        {
                            form = FormController.formInfoList[i - 1].Form;
                            form.Show();
                        }
                        else
                        {
                            int left = Screen.AllScreens[i].Bounds.Left;
                            form = FormBuilder.createForm(left, 0, Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height, screenInfo.ModelName);
                            FormController.addForm(screenInfo.ModelIndex, screenInfo.ModelName, form);
                            form.Visible = true;
                            string url = remoteUrl + screenInfo.ModelURL;
                            form.Text = screenInfo.ModelName + "屏";
                            form.initWebBrowser(screenInfo.browserType, url);
                            form.Navigate(url, loginInfo.User.UserAccount, loginInfo.User.UserToken);
                            form.Show();
                        }
                    }
                    //如果实际屏幕数大于要显示的页面数，那么多余的实际屏幕中的原有页面（来自上一个场景）要关闭。
                    if (ScreenWorker.getScreenCount() > formCountToShow)
                    {
                        try
                        {
                            for (int i = formCountToShow; i < ScreenWorker.getScreenCount(); i++)
                            {
                                ScreenForm form = FormController.formInfoList[i - 1].Form;
                                form.Hide(); 
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 构建场景按钮
        /// </summary>
        private void initBBBtn_scene()
        {
            bubbleBarTab_scene.Buttons.Clear();
            for (int i = 1; i <= loginInfo.SceneList.Count; i++)
            {
                SceneInfo scene = loginInfo.SceneList[i - 1];
                BubbleButton bbbtn = new BubbleButton();
                bbbtn.Name = scene.SceneName;
                bbbtn.TooltipText = scene.SceneName;
                switch (i)
                {
                    case 1:
                        bbbtn.Image = Properties.Resources._01;
                        bbbtn.ImageLarge = Properties.Resources._01;
                        break;
                    case 2:
                        bbbtn.Image = Properties.Resources._02;
                        bbbtn.ImageLarge = Properties.Resources._02;
                        break;
                    case 3:
                        bbbtn.Image = Properties.Resources._03;
                        bbbtn.ImageLarge = Properties.Resources._03;
                        break;
                    case 4:
                        bbbtn.Image = Properties.Resources._04;
                        bbbtn.ImageLarge = Properties.Resources._04;
                        break;
                    case 5:
                        bbbtn.Image = Properties.Resources._05;
                        bbbtn.ImageLarge = Properties.Resources._05;
                        break;
                    case 6:
                        bbbtn.Image = Properties.Resources._06;
                        bbbtn.ImageLarge = Properties.Resources._06;
                        break;
                    case 7:
                        bbbtn.Image = Properties.Resources._07;
                        bbbtn.ImageLarge = Properties.Resources._07;
                        break;
                    case 8:
                        bbbtn.Image = Properties.Resources._08;
                        bbbtn.ImageLarge = Properties.Resources._08;
                        break;
                    case 9:
                        bbbtn.Image = Properties.Resources._09;
                        bbbtn.ImageLarge = Properties.Resources._09;
                        break;
                    case 10:
                        bbbtn.Image = Properties.Resources._10;
                        bbbtn.ImageLarge = Properties.Resources._10;
                        break;
                    default:
                        bbbtn.Image = Properties.Resources._00;
                        bbbtn.ImageLarge = Properties.Resources._00;
                        break;
                }
                bbbtn.Click += Bbbtn_Click;
                bubbleBarTab_scene.Buttons.Add(bbbtn);
            }
        }

        private void Bbbtn_Click(object sender, ClickEventArgs e)
        {
            BubbleButton bbbtn = sender as BubbleButton;
            switchScene(bbbtn.Name);
        }
        /// <summary>
        /// 设置头部panel宽度
        /// </summary>
        private void initPanelWidth()
        {
            panel_user.Width = 180;
            int screenWidth = Screen.AllScreens[0].Bounds.Width;
        }
        /// <summary>
        /// 设置工具栏中语音识别、人脸识别按钮状态
        /// </summary>
        private void initBBBtn_tool()
        {
            if (getRespeakerIndexByDevice() == -1)//如果没有检查到指定的声音输入设备
            {
                this.bbbtn_speech.Image = global::HAYC_Studio.Properties.Resources.speech32_off;
                this.bbbtn_speech.ImageLarge = global::HAYC_Studio.Properties.Resources.speech32_off;
                bbbtn_speech.Enabled = false;
                bbbtn_speech.TooltipText = "未检测到指定语音输入设备，无法使用语音识别功能。" + Environment.NewLine + "请检查设备是否正常连接。";
            }
            else
            {
                try
                {
                    if (windowsServerManager.ServiceIsRunning(speechServiceName))
                    {
                        this.bbbtn_speech.Image = global::HAYC_Studio.Properties.Resources.speech32_on;
                        this.bbbtn_speech.ImageLarge = global::HAYC_Studio.Properties.Resources.speech32_on;
                    }
                    else
                    {
                        this.bbbtn_speech.Image = global::HAYC_Studio.Properties.Resources.speech32_off;
                        this.bbbtn_speech.ImageLarge = global::HAYC_Studio.Properties.Resources.speech32_off;
                    }
                    bbbtn_speech.Enabled = true;
                    bbbtn_speech.TooltipText = "语音识别服务";
                }
                catch (Exception ex)
                {
                    bbbtn_speech.Enabled = false;
                    bbbtn_speech.TooltipText = ex.Message;
                }
            }
            if (OpenNI.EnumerateDevices().Length == 0)//如果没有检测到深度摄像头
            {
                this.bbbtn_face.Image = global::HAYC_Studio.Properties.Resources.face32_off;
                this.bbbtn_face.ImageLarge = global::HAYC_Studio.Properties.Resources.face32_off;
                bbbtn_face.Enabled = false;
                bbbtn_face.TooltipText = "未检测到深度体感摄像头，无法使用人脸识别功能。"+Environment.NewLine+"请检查设备是否正常连接并已安装驱动。";
            }
            else
            {
                try
                {
                    if (windowsServerManager.ServiceIsRunning(faceServiceName))
                    {
                        this.bbbtn_face.Image = global::HAYC_Studio.Properties.Resources.face32_on;
                        this.bbbtn_face.ImageLarge = global::HAYC_Studio.Properties.Resources.face32_on;
                    }
                    else
                    {
                        this.bbbtn_face.Image = global::HAYC_Studio.Properties.Resources.face32_off;
                        this.bbbtn_face.ImageLarge = global::HAYC_Studio.Properties.Resources.face32_off;
                    }
                    bbbtn_face.Enabled = true;
                    bbbtn_face.TooltipText = "人脸识别服务";
                }
                catch (Exception ex)
                {
                    bbbtn_face.Enabled = false;
                    bbbtn_face.TooltipText = ex.Message;
                }
            }
            bubbleBar.Refresh();
        }
        private static MMDeviceEnumerator enumerator;
        private static MMDevice[] CaptureDevices;
        private static MMDevice selectedDevice;
        private static string voicePickerName;//需要获取的麦克风名字关键字，用于查找特定的麦克风
        private void initSpeaker()
        {
            voicePickerName = ConfigWorker.GetConfigValue("voicePickerName");
            enumerator = new MMDeviceEnumerator();
        }
        private int getRespeakerIndexByDevice()
        {
            CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            foreach (var device in CaptureDevices)
            {
                LogHelper.WriteLog("找到一个麦克风，名字是" + device.DeviceFriendlyName);
            }
            for (int i = 0; i < CaptureDevices.Length; i++)
            {
                if (CaptureDevices[i].DeviceFriendlyName.ToLower().Contains(voicePickerName))
                {
                    return i;
                }
            }
            //LogHelper.WriteLog("没有找到适配的麦克风");
            return -1;
        }
        #endregion

    }
}
