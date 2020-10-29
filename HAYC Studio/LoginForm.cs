using DevComponents.DotNetBar;
using fastJSON;
using HAYC_ProcessCommunicate_Library;
using HAYC_Studio.Browser;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenNIWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]//COM+组件可见
    public partial class LoginForm : Form
    {
        string remoteURL;
        string loginURL;
        string loginAction;
        string getUserURL;
        string getUserAction;

        string userName, password;
        bool showBoxOnCheckHardwareFaild;

        LoginInfo loginInfo = null;
        const string cookieKey = "HA_LINK_LOGIN_INFO";
        string loginResponse = string.Empty;
        BrowserType browserType;
        //FormBuilder fb = new FormBuilder();

        public int recordAheadInt = 10;//录像播放提前量，秒

        Speaker speaker;
        PipeCommunicateServer SpeechPipeServer;
        SpeechPipeCommunicateServerWorker SpeechPipeServerWorker;
        PipeCommunicateServer FacePipeServer;
        FacePipeCommunicateServerWorker facePipeServerWorker;

        private const string speechServiceName = "HAYC Speech Service";
        private const string faceServiceName = "HAYC FaceDetect Service";
        WindowsServiceManager windowsServerManager = new WindowsServiceManager(new List<string>() { speechServiceName, faceServiceName });

        private static Bitmap bitmap = new Bitmap(1, 1);
        private Mat image2;
        private string tempJpgPath = Application.StartupPath + "\\" + ConfigWorker.GetConfigValue("tempJpnPath");
        //public static string userConditionCode = "28.24.179.62.51.201.183.191.66.134.81.191.44.52.101.192.40.245.229.190.36.199.240.189.68.190.186.191.228.39.193.63.22.135.216.191.213.92.18.192.64.85.255.189.121.5.52.64.247.111.208.61.200.198.71.64.0.199.38.192.208.1.105.191.224.198.153.63.122.155.49.192.19.123.185.63.196.120.26.191.152.151.147.63.33.69.186.191.143.76.164.62.169.29.49.63.249.238.232.63.109.222.45.64.172.156.45.192.68.59.233.191.156.239.195.191.148.172.103.63.175.115.72.192.198.248.43.192.245.196.180.63.14.95.35.63.153.244.227.190.10.152.26.64.60.32.126.191.126.79.0.192.22.111.57.64.100.36.185.191.34.15.60.192.48.49.24.63.59.165.34.192.92.19.226.61.160.204.92.63.187.237.187.62.192.150.206.63.113.29.209.190.118.145.119.63.193.8.231.191.2.76.211.191.110.113.28.191.11.82.30.63.24.243.231.61.145.17.193.63.160.209.48.61.120.157.32.63.146.95.248.191.138.21.154.190.89.254.194.63.176.53.119.191.0.172.28.63.40.68.139.63.55.12.184.62.165.203.20.62.239.230.164.63.7.18.226.62.17.63.15.192.196.168.93.64.84.12.161.190.144.238.33.191.192.92.119.64.152.169.160.190.68.3.17.191.135.22.207.63.42.68.4.190.154.41.147.63.43.239.0.64.212.154.73.61.206.140.18.64.24.109.198.190.208.112.229.190.244.127.24.63.80.40.149.61.54.183.227.191.0.211.151.189.39.86.146.191.52.86.197.63.193.32.12.64.71.0.45.64.84.197.125.62.19.119.148.63.213.221.97.64.202.69.230.63.12.171.24.64.152.127.109.63.189.165.67.64.0.247.165.63.44.170.38.64.140.107.223.189.180.74.98.63.76.195.138.63.84.126.13.192.218.125.228.62.178.99.208.191.14.51.65.62.168.50.243.62.199.20.187.62.197.130.20.64.78.112.64.192.185.40.243.190.77.36.170.190.47.221.242.191.208.133.244.62.106.90.151.64.104.182.16.190.242.165.10.192.68.17.207.61.50.99.195.191.122.141.149.62.162.2.217.191.117.242.94.64.248.66.161.191.143.39.11.191.0.95.102.61.94.18.220.63.98.192.170.191.107.212.156.61.12.1.56.49.102.101.36.86.29.45.65.46.32.68.82.51.96.54.103.17.8.47.22.34.104.7.110.94.120.115.112.52.42.126.113.58.123.3.107.16.70.99.14.93.24.71.83.88.20.72.63.33.21.28.41.91.109.77.85.122.114.78.66.108.5.18.23.106.76.73.74.125.119.111.92.38.40.81.55.64.11.39.62.53.9.59.127.44.19.79.48.60.75.13.80.67.26.30.57.118.31.116.0.90.97.124.50.69.10.84.6.43.2.95.121.117.27.4.37.105.89.98.61.25.87.15.35.100";
        SingleMainStudio sms;
        public static List<UserFaceInfo> userFaceInfoList = new List<UserFaceInfo>();

        object cameraMonitorLock = new object();
        private string serialPortName = string.Empty;

        //public LevitatedBall.LevitateBall levitateBall = new LevitatedBall.LevitateBall();
        public LoginForm()
        {
            InitializeComponent();
            string logoImage = ConfigWorker.GetConfigValue("logoImage");
            this.pictureBox1.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(logoImage, null);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig() == false)
            {
                MessageWorker.showMessage("读取本地配置出现错误，请检查配置文件及程序日志");
                Application.Exit();
            }
            //levitateBall.Show();
            initPipeServer();
            initBeepWorker();
            mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.Controls.Add(mediaPlayer);
            mediaPlayer.Visible = false;
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            speaker = new Speaker(mediaPlayer, SpeechPipeServer);
            try
            {
                windowsServerManager.StartService(speechServiceName, needRestart: true);
                windowsServerManager.StartService(faceServiceName, needRestart: true);
            }
            catch (Exception ex)
            {
                if (showBoxOnCheckHardwareFaild)
                {
                    MessageBox.Show(ex.Message + "。在服务恢复正常之前，您将无法使用相关的功能。", "服务状态异常", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            //levitateBall.windowsServerManager = windowsServerManager;
            userFaceInfoList = FaceScoreHelper.faceInfoDeserialize();//读取用户和人脸特征码关联关系
            txt_username.BackColor = getHtmlColor("#1d273e");
            txt_password.BackColor = getHtmlColor("#1d273e");
        }

        private void initPipeServer()
        {
            initSpeechPipeServer();
            initFacePipeServer();
        }
        private void initSpeechPipeServer()
        {
            SpeechPipeServer = new PipeCommunicateServer("SpeechPipe");
            SpeechPipeServer.OnClientMessage += Server_OnClientMessage;
            SpeechPipeServer.OnConnection += PipeServer_OnConnection;
            SpeechPipeServer.OnDisConnection += PipeServer_OnDisConnection;
            SpeechPipeServer.OnPipeError += PipeServer_OnPipeError;
            SpeechPipeServer.startServer();
            SpeechPipeServerWorker = new SpeechPipeCommunicateServerWorker(SpeechPipeServer);
        }

        private void initFacePipeServer()
        {
            FacePipeServer = new PipeCommunicateServer("FaceDetectPipe");
            FacePipeServer.OnClientMessage += Server_OnClientMessage;
            FacePipeServer.OnConnection += PipeServer_OnConnection;
            FacePipeServer.OnDisConnection += PipeServer_OnDisConnection;
            FacePipeServer.OnPipeError += PipeServer_OnPipeError;
            FacePipeServer.startServer();
            facePipeServerWorker = new FacePipeCommunicateServerWorker(FacePipeServer);
        }
        private void initBeepWorker()
        {
            serialPortName = ConfigWorker.GetConfigValue("serialPortName");
            BeepWorker.init(serialPortName);
        }

        private bool loadLocalConfig()
        {
            try
            {
                remoteURL = ConfigWorker.GetConfigValue("hostUrl");
                loginAction = ConfigWorker.GetConfigValue("loginApi");
                loginURL = remoteURL + loginAction;
                getUserAction = ConfigWorker.GetConfigValue("infoApi");
                getUserURL = remoteURL + getUserAction;
                recordAheadInt = int.Parse(ConfigWorker.GetConfigValue("recordAhead"));
                showBoxOnCheckHardwareFaild = ConfigWorker.GetConfigValue("showBoxOnCheckHardwareFaild") == "1" ? true : false;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("读取本地配置出现错误：" + ex.Message);
                return false;
            }
        }

        private Color getHtmlColor(string htmlColor)
        {
            return ColorTranslator.FromHtml(htmlColor);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text.Trim() == string.Empty || txt_password.Text.Trim() == string.Empty)
            {
                MessageWorker.showMessage("用户名、密码不能为空");
            }
            else
            {
                loginInfo = new LoginInfo();
                //startWork();
                var loginResult = login(txt_username.Text.Trim(), txt_password.Text.Trim());
                if (loginResult)
                {
                    userName = txt_username.Text.Trim();
                    password = txt_password.Text.Trim();
                    var getUserInfoResult = getUserInfo();
                    if (getUserInfoResult)
                    {
                        startWork();
                    }
                }
            }
        }

        /// <summary>
        /// 实际登录方法
        /// </summary>
        private bool login(string username, string password)
        {
            Dictionary<string, string> postDataDic = new Dictionary<string, string>();
            /////////////////用户名、密码转码，解决含特殊字符的问题//////////////////////
            username = System.Web.HttpUtility.UrlEncode(username, System.Text.Encoding.UTF8);
            password = System.Web.HttpUtility.UrlEncode(password, System.Text.Encoding.UTF8);
            string loginHead = "{\"loginName\": \"" + username + "\",\"loginPass\": \"" + password + "\"}";
            try
            {
                string strResponse = HttpWorker.PostByHttpClient(loginURL, loginHead);
                loginResponse = strResponse;
                //MessageWorker.showMessage("返回值是"+strResponse);
                Dictionary<string, object> responseDic = JSON.Parse(strResponse) as Dictionary<string, object>;
                if (responseDic.Keys.Contains("resultCode") && responseDic.Keys.Contains("msg"))
                {
                    if (responseDic["resultCode"].ToString() != "200") //old value:-1
                    {
                        MessageWorker.showMessage(responseDic["msg"].ToString());
                        return false;
                    }
                }
                else
                {
                    LogHelper.WriteLog("登录返回信息不完整。" + strResponse);
                    return false;
                }
                LoginResponseEntity loginResponseEntity = LoginResponseEntity.fromJson(strResponse);
                loginInfo = TransDicToUserInfo.translate(loginInfo, loginResponseEntity);
                return true;
            }
            catch (WebException we)
            {
                loginInfo = null;
                LogHelper.WriteLog(we.Message);
                throw new Exception(we.Message);
            }

            catch (Exception e)
            {
                loginInfo = null;
                LogHelper.WriteLog(e.Message);
                MessageBox.Show("连接平台出现异常，请确认是否平台是否可以正常访问", "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;   
            }
            finally
            {
                //FormController.formInfoList.Clear();//清除已保存的窗体对象列表
            }
        }

        /// <summary>
        /// 获取用户信息方法
        /// </summary>
        /// <returns></returns>
        private bool getUserInfo()
        {
            Dictionary<string, string> postDataDic = new Dictionary<string, string>();
            string loginHead = loginInfo.User.UserAccount + ": " + loginInfo.User.UserToken + "\r\n";
            try
            {
                string strResponse = HttpWorker.PostByHttpClientWithCookie(getUserURL, loginInfo.User.UserAccount, loginInfo.User.UserToken);
                UserInfoResposeEntity userInfoResponseEntity = UserInfoResposeEntity.fromJson(strResponse);
                loginInfo = TransDicToUserInfo.translate(loginInfo, userInfoResponseEntity);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("获取用户信息失败:" + ex.Message);
                //return false;
                //todo 临时************************************************************************
                //*************************************************************************************
                return true;
                //*************************************************************************************
            }

            return true;
        }

        //人脸识别登录时，由非UI线程调用start Work方法，必须使用invoke方式调用，否则创建webbrowser时报“当前线程不在单线程单元中，因此无法实例化 ActiveX 控件”
        public delegate void startWorkHandler();
        private void startWork()
        {
            if (this.InvokeRequired)
            {
                startWorkHandler handler = new startWorkHandler(startWork);
                this.Invoke(handler);
            }
            else
            {
                if (loginInfo == null)
                {
                    MessageWorker.showMessage("您尚未登陆系统，请先登陆");
                }
                else
                {
                    int userHopeScreenCount = loginInfo.SceneList.Count;//用户期望的屏幕数
                    int realScreenCount = ScreenWorker.getScreenCount();//机器上实际的屏幕数
                    string screenType = ConfigWorker.GetConfigValue("screenType");
                    if (screenType == "s" || screenType == "o")//如果单屏
                    {
                        //正常显示
                        ShowForm(loginInfo, screenType);
                    }
                    else if (screenType == "m")//如果多屏
                    {
                        //20200902修改，realScreenCount现在是场景数，不再需要跟实际屏幕数比较。当打开场景时再用该场景下的屏幕数和实际屏幕数比较
                        //if (realScreenCount < userHopeScreenCount)
                        //{
                        //    //如果期望屏幕数大于实际屏幕数，提示
                        //    MessageWorker.showMessage("实际的屏幕数量少于用户要打开的屏幕数量，部分屏幕将无法显示。");
                        //}
                        ShowForm(loginInfo, screenType);
                    }
                    else if (screenType == "w")//单宽屏
                    {
                        CloseCamera();
                        SingleMainStudio.cookieValue = loginResponse;
                        if (sms == null)//如果首次打开
                        {
                            sms = new SingleMainStudio(loginInfo, remoteURL);
                            sms.windowsServerManager = windowsServerManager;
                            sms.Show(this);
                            if (loginInfo.SceneList.Count > 0)
                            {
                                sms.initWebBrowser(loginInfo.SceneList[0]);//起始默认显示第一个场景
                            }
                            else
                            {
                                sms.panel_main.Text = "您的账号当前尚未配置任何场景，请在SODB平台中进行场景配置";
                            }
                            sms.resumeJob();
                        }
                        else//如果锁定后再次人脸登录的
                        {
                            sms.loginInfo = loginInfo;
                            sms.remoteUrl = remoteURL;
                            sms.Show(this);
                            if (loginInfo.SceneList.Count > 0)
                            {
                                sms.initWebBrowser(loginInfo.SceneList[0]);//起始默认显示第一个场景
                            }
                            else
                            {
                                sms.panel_main.Text = "您的账号当前尚未配置任何场景，请在SODB平台中进行场景配置";
                            }
                            sms.resumeJob();
                        }
                        this.Hide();
                    }
                }
            }
        }

        /// <summary>
        /// 展示多屏窗口
        /// </summary>
        /// <param name="screenInfo">web接口得到的屏幕信息列表</param>
        /// <param name="model">多屏模式：m真正的多屏 s\w单个宽屏</param>
        /// <param name="formCount">真正要显示的屏幕数</param>
        private void ShowForm(LoginInfo loginInfo, string model, int formCount = -1)
        {
            List<ScreenInfo> screenInfoList = loginInfo.SceneList[0].ScreenList;//todo 先看第一个场景
            if (model == "m")
            {
                int left = 0;
                //取实际屏幕数和场景页面数，两者的最小值
                int formCountToShow = screenInfoList.Count > ScreenWorker.getScreenCount() ? ScreenWorker.getScreenCount() : screenInfoList.Count;
                for (int i = 0; i < formCountToShow; i++)
                {
                    if (i == 0)//主屏幕，带菜单栏
                    {
                        left = Screen.AllScreens[i].Bounds.Left;
                        ScreenInfo screenInfo = screenInfoList[i];
                        sms = FormBuilder.createForm(left, 0, Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height, screenInfo.ModelName, loginInfo, remoteURL);
                        //FormController.addForm(screenInfo.ModelIndex, screenInfo.ModelName, sms);//主屏幕不加入FormController
                        sms.Visible = true;
                        CloseCamera();
                        SingleMainStudio.cookieValue = loginResponse;
                        sms.windowsServerManager = windowsServerManager;
                        sms.Visible = false;
                        sms.Show(this);
                        if (loginInfo.SceneList.Count > 0)
                        {
                            sms.initWebBrowser(loginInfo.SceneList[0]);//起始默认显示第一个场景
                        }
                        else
                        {
                            sms.panel_main.Text = "您的账号当前尚未配置任何场景，请在SODB平台中进行场景配置";
                        }
                        sms.resumeJob();
                    }
                    else//副屏幕，不带菜单栏
                    {
                        left = Screen.AllScreens[i].Bounds.Left;
                        ScreenInfo screenInfo = screenInfoList[i];
                        ScreenForm form = FormBuilder.createForm(left, 0, Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height, screenInfo.ModelName);
                        FormController.addForm(screenInfo.ModelIndex, screenInfo.ModelName, form);
                        form.Visible = true;
                        string url = remoteURL + screenInfo.ModelURL;
                        form.Text = screenInfo.ModelName + "屏";
                        form.initWebBrowser(browserType, url);
                        form.Navigate(url, loginInfo.User.UserAccount, loginInfo.User.UserToken);
                        form.Show();
                    }
                }
            }
            else if (model == "s")
            {
                int formCountToShow;
                if (formCount != -1)
                {
                    formCountToShow = formCount;
                }
                else//以formCount为准
                {
                    formCountToShow = screenInfoList.Count;
                }
                for (int i = 0; i < formCountToShow; i++)
                {
                    int leftPosition = Screen.AllScreens[0].Bounds.Width * i / formCountToShow;
                    int formWidth = Screen.AllScreens[0].Bounds.Width / formCountToShow;
                    LogHelper.WriteLog("窗体" + i + "的left:" + leftPosition + "，width:" + formWidth);

                    ScreenInfo screenInfo = screenInfoList[i];
                    string modelName = string.Empty;
                    bool extralForm = false;
                    if (i < screenInfoList.Count)
                    {
                        modelName = screenInfo.ModelName;
                    }
                    else
                    {
                        modelName = "屏幕" + i;
                        extralForm = true;
                    }
                    ScreenForm form = FormBuilder.createForm(leftPosition, 0, formWidth, Screen.AllScreens[0].Bounds.Height - 30, modelName);
                    if (extralForm)
                    {
                        FormController.addForm(10 * i, modelName, form);
                        form.Text = modelName;
                        form.Left = leftPosition;
                        form.Visible = true;
                        form.Show();
                    }
                    else
                    {
                        FormController.addForm(screenInfo.ModelIndex, modelName, form);
                        form.Left = leftPosition;
                        form.Visible = true;
                        string url = remoteURL + screenInfo.ModelURL;
                        form.Text = screenInfo.ModelName + "屏";
                        url = "http://192.168.111.70:2000/#/ams/alarmMonitor";
                        form.initWebBrowser(browserType, url);
                        form.Navigate(url, loginInfo.User.UserAccount, loginInfo.User.UserToken);
                        form.Show();
                    }
                }
            }
            else if (model == "o")//单屏单窗体，用于临时演示
            {
                screenInfoList = new List<ScreenInfo>();
                screenInfoList.Add(new ScreenInfo() { browserType = BrowserType.IE, Left = 0, Top = 0, ModelIndex = 1, ModelName = "信息", ModelURL = "/ms/alarmMonitor" });
                screenInfoList.Add(new ScreenInfo() { browserType = BrowserType.IE, Left = 0, Top = 0, ModelIndex = 2, ModelName = "视频", ModelURL = "/ms/alarmentry" });
                tempList.Add("GIS屏", remoteURL + "/ms/alarmMonitor");
                tempList.Add("信息屏", remoteURL + "/ms/alarmentry");

                ScreenInfo screenInfo = screenInfoList[0];
                string modelName = screenInfo.ModelName;
                ScreenForm form = FormBuilder.createForm(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.AllScreens[0].Bounds.Height - 30, modelName);
                FormController.addForm(screenInfo.ModelIndex, modelName, form);
                form.Left = 0;
                form.Visible = true;
                string url = remoteURL + screenInfo.ModelURL;
                form.Text = screenInfo.ModelName + "屏";
                form.initWebBrowser(browserType, url);
                form.Navigate(url, "HA_LINK_LOGIN_INFO", loginResponse);
                tempForm = form;
                form.Show();
            }
            this.Hide();
        }
        public Dictionary<string, string> tempList = new Dictionary<string, string>();
        public ScreenForm tempForm;

        #region 进程间交互
        private void Server_OnClientMessage(NamedPipeWrapper.NamedPipeConnection<string, string> connection, string message)
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
                        Task.Factory.StartNew(() =>
                        {
                            dealScoreCommand(messageEntity);
                        });
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
        string scoreFromFaceDetect = string.Empty;
        string userNameFromFaceDetect = string.Empty;
        bool isStartWorking = false;
        bool isDealingSocre = false;
        private void dealScoreCommand(ProcessCommunicateMessage message)
        {
            Debug.WriteLine("人脸识别结果为"+message.Message);
            if (isDealingSocre)
            {
                Debug.WriteLine("前一个人脸识别结果正在处理中，跳过本次结果处理");
                LogHelper.WriteLog("前一个人脸识别结果正在处理中，跳过本次结果处理");
                return;
            }
            isDealingSocre = true;
            //如果登录窗体已隐藏，说明是在进行定时人脸检测
            if (this.Visible == false)
            {
                var faceCode = message.Message;
                double score = 0;
                var paramsList = message.Message.Split(new string[] { "@" }, StringSplitOptions.None);
                if (paramsList.Count() == 2)
                {
                    scoreFromFaceDetect = paramsList[1];
                }
                else
                {
                    isDealingSocre = false;
                    return;
                }
                if (double.TryParse(scoreFromFaceDetect, out score))
                {
                    int checkScoreResult = FaceScoreHelper.checkScore(score);
                    Debug.WriteLine(checkScoreResult);
                    switch (checkScoreResult)
                    {
                        case 1://定时人脸检测成功，暂停timer，其余什么都不做
                            //CloseCamera();
                            Thread.Sleep(500);
                            //验证通过
                            Debug.WriteLine("定时验证成功");
                            sms.pauseJob();
                            //currentFaceDetectFaild = 0;
                            //sms.init(loginInfo, true);
                            isDealingSocre = false;
                            //sms.Show();
                            break;
                        case 2:
                            //验证失败
                            isStartWorking = false;
                            sms.stopJob();
                            sms.CloseCamera();
                            txt_username.Text = "";
                            txt_password.Text = "";
                            lbl_faceDetectResult.Visible = false;
                            lbl_faceDetectResult.Text = "验证中";
                            lbl_faceDetectResult.ForeColor = Color.Yellow;
                            Debug.WriteLine("定时验证失败，锁定程序");
                            sms.Hide();
                            this.Show();
                            isDealingSocre = false;
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {
                    LogHelper.WriteLog("人脸识别得分格式不正确:" + message.Message);
                }
            }
            else
            {
                if (isStartWorking == false)
                {
                    double score = 0;
                    var paramsList = message.Message.Split(new string[] { "@" }, StringSplitOptions.None);
                    if (paramsList.Count() == 2)
                    {
                        userNameFromFaceDetect = paramsList[0];
                        scoreFromFaceDetect = paramsList[1];
                    }
                    else
                    {
                        isDealingSocre = false;
                        return;
                    }
                    if (double.TryParse(scoreFromFaceDetect, out score))
                    {
                        int checkScoreResult = FaceScoreHelper.checkScore(score);
                        lbl_faceDetectResult.Visible = true;
                        switch (checkScoreResult)
                        {
                            case 1:
                                isStartWorking = true;
                                lbl_faceDetectResult.Text = "验证通过";
                                lbl_faceDetectResult.ForeColor = Color.Green;
                                CloseCamera();
                                //Thread.Sleep(500);
                                //验证通过，打开主窗体
                                timer_drawBox.Stop();
                                timer_saveImage.Stop();
                                pb_camera.Visible = false;
                                pb_cameraWithBox.Visible = false;
                                loginInfo = new LoginInfo();
                                var ufi = userFaceInfoList.FirstOrDefault(u => u.userName == userNameFromFaceDetect);
                                if (ufi == null)
                                {
                                    MessageBox.Show("没有在本地找到以下用户的信息" + userNameFromFaceDetect);
                                }
                                else
                                {
                                    var loginResult = login(userNameFromFaceDetect, ufi.password);
                                    if (loginResult)
                                    {
                                        var getUserInfoResult = getUserInfo();
                                        if (getUserInfoResult)
                                        {
                                            startWork();
                                        }
                                    }
                                    //txt_username.Text = ufi.userName;
                                    //txt_password.Text = ufi.password;
                                    //btn_login_Click(null, new EventArgs());
                                }
                                isDealingSocre = false;
                                break;
                            case 2:
                                lbl_faceDetectResult.Text = "验证未通过！";
                                lbl_faceDetectResult.ForeColor = Color.Red;
                                //验证失败
                                txt_username.Visible = true;
                                txt_password.Visible = true;
                                btn_login.Visible = true;
                                timer_drawBox.Stop();
                                timer_saveImage.Stop();
                                pb_camera.Visible = false;
                                pb_cameraWithBox.Visible = false;
                                isDealingSocre = false;
                                CloseCamera();
                                break;
                            default:
                                isDealingSocre = false;
                                break;
                        }
                    }
                    else
                    {
                        LogHelper.WriteLog("人脸识别得分格式不正确:" + message.Message);
                    }
                }
            }
            isDealingSocre = false;
        }
        private void dealFeatCommand(ProcessCommunicateMessage message)
        {
            var faceCode = message.Message;
            //将特征码和用户关联，并保存关联关系
            var currentUfi = userFaceInfoList.FirstOrDefault(u => u.userName == userNameFromFaceDetect);
            if (currentUfi == null)//增加
            {
                UserFaceInfo ufi = new UserFaceInfo()
                {
                    userName = userName,
                    password = password,
                    faceCode = faceCode
                };
                userFaceInfoList.Add(ufi);
            }
            else//更新
            {
                currentUfi.password = password;
                currentUfi.faceCode = faceCode;
            }
            FaceScoreHelper.faceInfoSerialize(userFaceInfoList);
        }
        private void dealStartCommand(ProcessCommunicateMessage message)
        {

        }
        private void dealStopCommand(ProcessCommunicateMessage message)
        {

        }
        private void dealSpeechResult(ProcessCommunicateMessage message)
        {
            CountdownEvent ce = new CountdownEvent(1);
            speaker.ce = ce;
            speaker.play(message.Message);
            if (ce.Wait(3000))//不能小于2500，要大于等于mp3播放的时长
            {
                if (message.Message != string.Empty)
                {
                    //levitateBall.bigForm.addSpeechMessage(message.Message);
                    //levitateBall.showMessage(message.Message);
                }
                LogHelper.WriteLog("语音识别结果为"+message.Message);
                //FormCommand command = new FormCommand(message.Message);
                //command.executeCommand();
                switch (message.Message)
                {
                    //case "切换到信息屏":
                    //    var url = tempList["信息屏"];
                    //    tempForm.Navigate(url, "HA_LINK_LOGIN_INFO", loginResponse);
                    //    break;
                    //case "切换到GIS屏":
                    //    var url1 = tempList["GIS屏"];
                    //    tempForm.Navigate(url1, "HA_LINK_LOGIN_INFO", loginResponse);
                    //    break;
                    case "关闭系统":
                        Application.Exit();
                        return;
                }
                if (message.Message.Contains("切换到"))
                {
                    string sceneName = message.Message.Replace("切换到", "").Replace("场景", ""); ;
                    sms.switchScene(sceneName);
                }
            }
        }
        private void dealMessage(ProcessCommunicateMessage message)
        {

        }
        private void dealSetting(ProcessCommunicateMessage messageEntity)
        {

        }

        private void PipeServer_OnPipeError(Exception exception)
        {
            Debug.WriteLine("Pipe连接错误：" + exception.Message);
            LogHelper.WriteLog("Pipe连接错误：" + exception.Message);
        }

        private void PipeServer_OnDisConnection(NamedPipeWrapper.NamedPipeConnection<string, string> connection)
        {
            Debug.WriteLine("Pipe断开连接" + connection.Name);
            LogHelper.WriteLog("Pipe断开连接" + connection.Name);
        }

        private void PipeServer_OnConnection(NamedPipeWrapper.NamedPipeConnection<string, string> connection)
        {
            Debug.WriteLine("Pipe建立连接" + connection.Name);
            LogHelper.WriteLog("Pipe建立连接" + connection.Name);
        }
        #endregion

        #region 摄像头图像控制
        private Device currentDevice;
        private VideoMode videoMode;
        private VideoStream currentSensor;
        CascadeClassifier face_cascade = new CascadeClassifier(@"haarcascade_frontalface_alt.xml");
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
                if (showBoxOnCheckHardwareFaild)
                {
                    MessageBox.Show("未能在本机上找到视频传感器设备。请确认视频传感器设备已连接到本机，并已正确安装驱动程序", "未能找到设备", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
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
                currentSensor.VideoMode = videoMode;
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
        /// <summary>
        /// 关闭摄像头
        /// </summary>
        public void CloseCamera()
        {
            if (this.currentSensor != null && this.currentSensor.IsValid)
            {
                this.currentSensor.Stop();
                this.currentSensor.OnNewFrame -= this.CurrentSensorOnNewFrame;
                OpenNI.Shutdown();
                GC.Collect();
            }
            Monitor.TryEnter(cameraMonitorLock,3000);
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
                                Debug.WriteLine("login bitmap update" + ex.Message);
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
        public Mat DetectFace(Mat src)
        {
            Mat result;
            using (var gray = new Mat())
            {
                result = src.Clone();
                try
                {
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                    Rect[] faces = face_cascade.DetectMultiScale(gray, 1.08, 2, HaarDetectionType.ScaleImage, new OpenCvSharp.Size(30, 30));
                    foreach (Rect face in faces)
                    {
                        var center = new OpenCvSharp.Point
                        {
                            X = (int)(face.X + face.Width * 0.5),
                            Y = (int)(face.Y + face.Height * 0.5)
                        };
                        var axes = new OpenCvSharp.Size
                        {
                            Width = (int)(face.Width * 0.7),
                            Height = (int)(face.Height * 0.7)
                        };
                        Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(0, 255, 0), 2, LineTypes.AntiAlias);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return result;
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
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.pb_camera.Visible)
            {
                this.pb_camera.Visible = false;
            }

            if (bitmap == null)
            {
                return;
            }

            lock (bitmap)
            {
                System.Drawing.Size canvasSize = this.pb_camera.Size;
                System.Drawing.Point canvasPosition = pb_camera.Location;

                double ratioX = canvasSize.Width / (double)bitmap.Width;
                double ratioY = canvasSize.Height / (double)bitmap.Height;
                double ratio = Math.Min(ratioX, ratioY);

                int drawWidth = Convert.ToInt32(bitmap.Width * ratio);
                int drawHeight = Convert.ToInt32(bitmap.Height * ratio);

                int drawX = canvasPosition.X + Convert.ToInt32((canvasSize.Width - drawWidth) / 2);
                int drawY = canvasPosition.Y + Convert.ToInt32((canvasSize.Height - drawHeight) / 2);

                e.Graphics.DrawImage(bitmap, drawX, drawY, drawWidth, drawHeight);
                //pb_image.BringToFront();
            }
        }
        #endregion

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //关闭服务
                windowsServerManager.StopService(speechServiceName);
                windowsServerManager.StopService(faceServiceName);
            }
            catch (Exception ex){}
            clearProgram();
        }

        private void clearProgram()
        {
            CloseCamera();
            timer_drawBox.Stop();
            timer_saveImage.Stop();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(tempJpgPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("清理临时图片目录出现异常"+ex.Message);
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            SetDouble(this);
            SetDouble(pb_cameraWithBox);
        }

        /// <summary>
        /// 双缓存解决窗体闪屏问题
        /// </summary>
        /// <param name="cc"></param>
        public static void SetDouble(Control cc)
        {
            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);
        }

        private void txt_username_MouseEnter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.BackColor = getHtmlColor("#0c8db0");
        }

        private void txt_username_MouseLeave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text == string.Empty)
            {
                txt.BackColor= getHtmlColor("#1d273e");
            }
        }

        private void txt_username_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.BackColor = getHtmlColor("#0c8db0");
        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text == string.Empty)
            {
                txt.BackColor = getHtmlColor("#0c8db0");
            }
        }

        private void lbl_faceDetect_Click(object sender, EventArgs e)
        {
            if (Monitor.IsEntered(cameraMonitorLock))
            {
                Monitor.Wait(cameraMonitorLock, 3000, false);
            }
            try
            {
                if (windowsServerManager.ServiceIsRunning(faceServiceName))
                {
                    lbl_faceDetectResult.Text = "验证中......";
                    lbl_faceDetectResult.ForeColor = Color.Yellow;
                    txt_username.Visible = false;
                    txt_password.Visible = false;
                    btn_login.Visible = false;
                    //pb_camera.Visible = true;
                    pb_camera.Size = new System.Drawing.Size(0, 0);
                    pb_camera.Visible = false;
                    pb_cameraWithBox.Visible = true;
                    pb_cameraWithBox.Size = new System.Drawing.Size(416, 312);
                    pb_camera.Location = pb_cameraWithBox.Location;
                    pb_cameraWithBox.BringToFront();
                    OpenCamera();
                    timer_drawBox.Start();
                    timer_saveImage.Start();
                }
                else
                {
                    windowsServerManager.StartService(faceServiceName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"服务状态异常",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);   
            }
        }
        #region timer定时器任务
        /// <summary>
        /// 定时，用于人脸画框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_drawBox_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap currentBitmap = bitmap.Clone() as Bitmap;
                image2 = BitmapConverter.ToMat(currentBitmap);
                Mat mat = DetectFace(image2);
                pb_cameraWithBox.Image = mat.ToBitmap();
            }
            catch (Exception)
            {

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            BeepWorker.open();
            Task.Run(() =>
            {
                BeepWorker.beep(AlarmLampColor.red);
            });
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            BeepWorker.open();
            Task.Run(() =>
            {
                BeepWorker.beep(AlarmLampColor.yellow);
            });
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            BeepWorker.open();
            Task.Run(() =>
            {
                BeepWorker.beep(AlarmLampColor.green);
            });
        }

        /// <summary>
        /// 定时，用于保存人脸图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_saveImage_Tick(object sender, EventArgs e)
        {
            string imagePath = pickAndSaveFaceImage();
            ProcessCommunicateMessage message = new ProcessCommunicateMessage();
            message.ProcessName = "";
            message.MessageType = CommunicateMessageType.SCORE;
            //message.Message = imagePath +"#"+ userConditionCode;
            message.Message = imagePath;
            FacePipeCommunicateServerWorker.sendMessage(message.toJson());
        }
        #endregion
    }
}
