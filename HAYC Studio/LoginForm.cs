using DevComponents.DotNetBar;
using fastJSON;
using HAYC_Studio.Browser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public partial class LoginForm : Form

    {
        string remoteURL;
        string loginURL;
        string loginAction;
        string getUserURL;
        string getUserAction;

        LoginInfo loginInfo = null;
        const string cookieKey = "HA_LINK_LOGIN_INFO";
        string loginResponse = string.Empty;
        BrowserType browserType;

        public int recordAheadInt = 10;//录像播放提前量，秒

        public LoginForm()
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
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("读取本地配置出现错误："+ex.Message);
                return false;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text.Trim() == string.Empty || txt_password.Text.Trim() == string.Empty)
            {
                MessageWorker.showMessage("用户名、密码不能为空");
            }
            else
            {
               var loginResult = login(txt_username.Text.Trim(), txt_password.Text.Trim());
                if (loginResult)
                {
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
                throw new Exception(e.Message);
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
            string loginHead = loginInfo.User.UserAccount + ": " + loginInfo.User.UserToken+"\r\n";
            try
            {
                string strResponse = HttpWorker.PostByHttpClientWithCookie(getUserURL, loginInfo.User.UserAccount, loginInfo.User.UserToken);
                UserInfoResposeEntity userInfoResponseEntity = UserInfoResposeEntity.fromJson(strResponse);
                loginInfo = TransDicToUserInfo.translate(loginInfo, userInfoResponseEntity);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("获取用户信息失败:" + ex.Message);   
            }

            return true;
        }

        private void startWork()
        {
            if (loginInfo == null)
            {
                MessageWorker.showMessage("您尚未登陆系统，请先登陆");
            }
            else
            {
                int userHopeScreenCount = loginInfo.ScreenList.Count;//用户期望的屏幕数
                int realScreenCount = ScreenWorker.getScreenCount();//机器上实际的屏幕数
                string screenType = ConfigWorker.GetConfigValue("screenType");
                if (screenType == "s")//如果单宽屏
                {
                    //正常显示
                    ShowForm(loginInfo, "s");
                }
                else if (screenType == "m")//如果多屏
                {
                    if (realScreenCount < userHopeScreenCount)
                    {
                        //如果期望屏幕数大于实际屏幕数，提示
                        MessageWorker.showMessage("实际的屏幕数量少于用户要打开的屏幕数量，部分屏幕将无法显示。");
                    }
                    ShowForm(loginInfo, "m");
                }
            }
        }

        /// <summary>
        /// 展示多屏窗口
        /// </summary>
        /// <param name="screenInfo">web接口得到的屏幕信息列表</param>
        /// <param name="model">多屏模式：m真正的多屏 s单个宽屏</param>
        /// <param name="formCount">真正要显示的屏幕数</param>
        private void ShowForm(LoginInfo loginInfo, string model, int formCount = -1)
        {
            List<ScreenInfo> screenInfoList = loginInfo.ScreenList;
            FormBuilder fb = new FormBuilder();
            if (model == "m")
            {
                int left = 0;
                for (int i = 0; i < ScreenWorker.getScreenCount(); i++)
                {
                    left = Screen.AllScreens[i].Bounds.Left;
                    ScreenInfo screenInfo = screenInfoList[i];
                    ScreenForm form = fb.createForm(left, 0, Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height, screenInfo.ModelName);
                    FormController.addForm(screenInfo.ModelIndex, screenInfo.ModelName, form);
                    form.Visible = true;
                    string url = remoteURL + screenInfo.ModelURL;
                    form.Text = screenInfo.ModelName + "屏";
                    form.initWebBrowser(browserType, url);
                    form.Navigate(url, cookieKey, loginResponse);
                    form.Show();
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
                    ScreenForm form = fb.createForm(leftPosition, 0, formWidth, Screen.AllScreens[0].Bounds.Height - 30, modelName);
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
                        form.Navigate(url, cookieKey, loginResponse);
                        form.Show();
                    }
                }
            }
            this.Hide();
        }
    }
}
