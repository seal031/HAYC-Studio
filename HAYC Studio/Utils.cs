using DevComponents.DotNetBar;
using HAYC_Studio;
using HAYC_Studio.Browser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ConfigWorker
{
    public static string GetConfigValue(string key)
    {
        if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
            return System.Configuration.ConfigurationManager.AppSettings[key];
        else
            return string.Empty;
    }

    public static void SetConfigValue(string key, string value)
    {
        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (cfa.AppSettings.Settings.AllKeys.Contains(key))
        {
            cfa.AppSettings.Settings[key].Value = value;
        }
        else
        {
            cfa.AppSettings.Settings.Add(key, value);
        }
        cfa.Save();
        ConfigurationManager.RefreshSection("appSettings");
    }
}

public class HttpWorker
{
    [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);


    public static string PostJson(string Url, string postDataStr)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
        //request.CookieContainer = cookie;
        Stream myRequestStream = request.GetRequestStream();
        StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
        myStreamWriter.Write(postDataStr);
        myStreamWriter.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //response.Cookies = cookie.GetCookies(response.ResponseUri);
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        string retString = myStreamReader.ReadToEnd();
        myStreamReader.Close();
        myResponseStream.Close();

        return retString;
    }

    /// <summary>
    /// 指定Post地址使用Get 方式获取全部字符串
    /// </summary>
    /// <param name="url">请求后台地址</param>
    /// <param name="content">Post提交数据内容(utf-8编码的)</param>
    /// <returns></returns>
    public static string PostStr(string url, string content)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";

        #region 添加Post 参数
        byte[] data = Encoding.UTF8.GetBytes(content);
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        #endregion

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    /// <summary>
    /// 指定Post地址使用Get 方式获取全部字符串
    /// </summary>
    /// <param name="url">请求后台地址</param>
    /// <returns></returns>
    public static string PostDic(string url, Dictionary<string, string> dic)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        //req.ContentType = "application/x-www-form-urlencoded";
        req.ContentType = "application/json";
        #region 添加Post 参数
        StringBuilder builder = new StringBuilder();
        int i = 0;
        foreach (var item in dic)
        {
            if (i > 0)
                builder.Append("&");
            builder.AppendFormat("{0}={1}", item.Key, item.Value);
            i++;
        }
        byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        #endregion
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    public static string PostByHttpClient(string url, string head)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        HttpContent content = new StringContent(head, Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = client.PostAsync(url, content).Result;
        var responseString = response.Content.ReadAsStringAsync().Result;
        return responseString;
    }

    public static string PostByHttpClientWithCookie(string url,string cookieKey,string cookieValue)
    {
        var handler = new HttpClientHandler() { UseCookies = false };
        var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        var message = new HttpRequestMessage(HttpMethod.Post, url);
        message.Headers.Add("Cookie", cookieKey + "=" + cookieValue);
        var result = client.SendAsync(message).Result;
        //result.EnsureSuccessStatusCode();
        return result.Content.ReadAsStringAsync().Result;
    }

    public static bool SetCookie(string url, string cookieKey, string cookieValue)
    {
        return InternetSetCookie(url, cookieKey, cookieValue);
    }
}

public class MessageWorker
{
    public static void showMessage(string message)
    {
        MessageBoxEx.Show(message);
    }
}

public class LogHelper
{
    public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
    public static void WriteLog(string info)
    {
        if (loginfo.IsInfoEnabled)
        {
            loginfo.Info(info);
        }
    }
}

public class TransDicToUserInfo
{
    public static LoginInfo translate(Dictionary<string, object> dic)
    {
        LoginInfo loginInfo = new LoginInfo();

        loginInfo.SceneList = new List<SceneInfo>();
        //转换sceneList
        foreach (object sceneModel in dic[""] as List<object>)
        {
            SceneInfo scene = new SceneInfo();
            scene.ScreenList = new List<ScreenInfo>();
            scene.SceneName = "";
            //转换screenList
            foreach (object screenModel in dic["models"] as List<object>)
            {
                ScreenInfo screen = new ScreenInfo();
                Dictionary<string, object> screenModelDic = screenModel as Dictionary<string, object>;
                screen.ModelName = screenModelDic["modelName"].ToString();
                screen.ModelIndex = int.Parse(screenModelDic["index"].ToString());
                screen.ModelURL = screenModelDic["modelUrl"].ToString();
                scene.ScreenList.Add(screen);
            }
            loginInfo.SceneList.Add(scene);
        }
        //转换userInfo
        foreach (object userModel in dic["userInfo"] as List<object>)
        {
            UserInfo userInfo = new UserInfo();
            Dictionary<string, object> userModelDic = userModel as Dictionary<string, object>;
            userInfo.UserAccount = userModelDic["userAccount"].ToString();
            userInfo.UserAddress = userModelDic["address"].ToString();
            userInfo.UserDepName = userModelDic["deptName"].ToString();
            userInfo.UserEmail = userModelDic["email"].ToString();
            userInfo.UserIdCard = userModelDic["idCard"].ToString();
            userInfo.UserPhone = userModelDic["phone"].ToString();
            userInfo.UserRealName = userModelDic["userName"].ToString();
            userInfo.UserWxCode = userModelDic["wxCode"].ToString();
            userInfo.UserToken = userModelDic["userLoginKey"].ToString();
            loginInfo.User = userInfo;
        }
        return loginInfo;
    }

    public static LoginInfo translate(LoginInfo info, LoginResponseEntity entity)
    {
        if (entity == null) return info;
        if (info == null)
        {
            info = new LoginInfo();
        }
        info.User = new UserInfo();
        info.User.UserAccount = entity.data.loginKey;
        info.User.UserToken = entity.data.loginValue;
        info.User.UserRealName = entity.data.userVo.userName;
        return info;
    }

    public static LoginInfo translate(LoginInfo info, UserInfoResposeEntity entity)
    {
        if (entity == null) return info;
        LoginInfo loginInfo = new LoginInfo();
        loginInfo.SceneList = new List<SceneInfo>();
        //转换sceneList
        foreach (var sceneModel in entity.datalist)
        {
            SceneInfo scene = new SceneInfo();
            scene.ScreenList = new List<ScreenInfo>();
            scene.SceneName = sceneModel.sceneName;
            //转换screenList
            foreach (var screenModel in sceneModel.sceneInfo)
            {
                ScreenInfo screen = new ScreenInfo();
                screen.ModelName = screenModel.screenName;
                screen.ModelIndex = screenModel.displayIndex;
                screen.ModelURL = screenModel.screenUrl;
                screen.browserType = (BrowserType)Enum.Parse(typeof(BrowserType), screenModel.browserTypeName);
                double ratio;
                if (double.TryParse(screenModel.screenRatio.Replace("%",""), out ratio))
                {
                    screen.ratio = ratio * 0.01;
                }
                scene.ScreenList.Add(screen);
            }
            loginInfo.SceneList.Add(scene);
        }
        loginInfo.User = info.User;
        return loginInfo;
    }
}

public class ScreenWorker
{
    /// <summary>
    /// 获取当前设备的屏幕数量
    /// </summary>
    /// <returns></returns>
    public static int getScreenCount()
    {
        return Screen.AllScreens.Count();
    }
}

public class FormBuilder
{
    public static ScreenForm createForm(int left, int top, int width, int height, string pageName, BaseVideoForm videoForm)
    {
        ScreenForm form = FormController.getFormIfExits(pageName);
        if (form == null)
        {
            form = new ScreenForm(videoForm);
        }
        form.StartPosition = FormStartPosition.Manual;
        //设置Form位置和大小
        form.Location = new System.Drawing.Point(left, top);
        //form.Top = top;
        form.Size = new System.Drawing.Size(width, height);
        //form.MaximizeBox = false;
        form.WindowState = FormWindowState.Maximized;
        //form.ControlBox = false;//不显示自带按钮
        return form;
    }
    public static ScreenForm createForm(int left, int top, int width, int height, string pageName)
    {
        ScreenForm form = FormController.getFormIfExits(pageName);
        if (form == null)
        {
            form = new ScreenForm();
        }
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new System.Drawing.Point(left, top);
        //form.Top = top;
        form.Size = new System.Drawing.Size(width, height);
        form.MaximizeBox = false;
        return form;
    }

    public static SingleMainStudio createForm(int left, int top, int width, int height, string pageName, LoginInfo loginInfo, string remoteUr)
    {
        SingleMainStudio form = FormController.singleMainStudio;
        if (form == null)
        {
            form = new SingleMainStudio(loginInfo, remoteUr);
            form.isMaster = true;
            FormController.singleMainStudio = form;
        }
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new System.Drawing.Point(left, top);
        form.Size = new System.Drawing.Size(width, height);
        form.MaximizeBox = false;
        return form;
    }
}