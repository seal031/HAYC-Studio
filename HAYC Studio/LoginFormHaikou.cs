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
    public partial class LoginFormHaikou : Office2007Form
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
        
        PipeCommunicateServer SpeechPipeServer;
        SpeechPipeCommunicateServerWorker SpeechPipeServerWorker;
        PipeCommunicateServer FacePipeServer;
        FacePipeCommunicateServerWorker facePipeServerWorker;

        private const string speechServiceName = "HAYC Speech Service";
        private const string faceServiceName = "HAYC FaceDetect Service";
        WindowsServiceManager windowsServerManager = new WindowsServiceManager(new List<string>() { speechServiceName, faceServiceName });
        
        private string tempJpgPath = Application.StartupPath + "\\" + ConfigWorker.GetConfigValue("tempJpnPath");
        SingleMainStudio sms;
        
        private string serialPortName = string.Empty;

        //public LevitatedBall.LevitateBall levitateBall = new LevitatedBall.LevitateBall();
        public LoginFormHaikou()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig() == false)
            {
                MessageWorker.showMessage("读取本地配置出现错误，请检查配置文件及程序日志");
                Application.Exit();
            }
            initPipeServer();
            initBeepWorker();
            try
            {
                //windowsServerManager.StartService(speechServiceName, needRestart: true);
                //windowsServerManager.StartService(faceServiceName, needRestart: true);
            }
            catch (Exception ex)
            {
                if (showBoxOnCheckHardwareFaild)
                {
                    MessageBox.Show(ex.Message + "。在服务恢复正常之前，您将无法使用相关的功能。", "服务状态异常", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            wb.ObjectForScripting = this;
        }
        /// <summary>
        /// 前后端交互方法
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="funcName"></param>
        /// <param name="paramList"></param>
        public void CallFunction(string formName, string funcName, params object[] paramList)
        {
            string strResponse = paramList[0].ToString();
            UserInfoResposeEntity userInfoResponseEntity = UserInfoResposeEntity.fromJson(strResponse);
            loginInfo = TransDicToUserInfo.translate(loginInfo, userInfoResponseEntity);
            startWork();
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
            
        }
        private void dealFeatCommand(ProcessCommunicateMessage message)
        {
            
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
    }
}
