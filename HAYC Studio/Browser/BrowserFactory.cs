using CefSharp;
using CefSharp.WinForms;
using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio.Browser
{
    public enum BrowserType
    {
        IE,
        Chrome
    }

    public class BrowserFactory
    {
        public static IHaycBrowser createBrowser(BrowserType browserType, ScreenForm form, string url)
        {
            switch (browserType)
            {
                case BrowserType.IE:
                    IEBrowser ie = new IEBrowser();
                    form.Controls.Add(ie);
                    ie.Dock = DockStyle.Fill;
                    return ie;
                case BrowserType.Chrome:
                    ChromeBrowser chrome = new ChromeBrowser(url);
                    chrome.Dock = DockStyle.Fill;
                    CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                    chrome.RegisterJsObject("chromeObj", new ChromeJsHandler(form));
                    form.Controls.Add(chrome);
                    return chrome;
                default:
                    return null;
            }
        }

    }

    public class ChromeJsHandler
    {
        private ScreenForm form;
        public ChromeJsHandler(ScreenForm form)
        {
            this.form = form;
        }
        public void callFunction(string formName, string functionName, object paramList)
        {
            string paramStr = string.Join(",", paramList);
            LogHelper.WriteLog("跨页面调用,formName:" + formName + "，functionName:" + functionName + "，参数:" + paramStr);
            form.CallFunction(formName, functionName, paramList);
        }
    }

    public interface IHaycBrowser
    {
        void Navigate(string url, string cookieKey = "", string cookieValue = "");
        void CallJsFunction(string functionName, object[] paramList);
        void ShowDevTools();
        void Dispose();
    }

    /// <summary>
    /// ie浏览器
    /// </summary>
    public class IEBrowser : WebBrowser, IHaycBrowser
    {
        public void Navigate(string url, string cookieKey = "", string cookieValue = "")
        {
            if (cookieKey != string.Empty && cookieValue != string.Empty)
            {
                HttpWorker.SetCookie(url, cookieKey, cookieValue);
                //string hostUrl = "http://" + new Uri(url).Host + ":" + new Uri(url).Port + "/";
                //this.Navigate(hostUrl);
                //this.Refresh();
                //Thread.Sleep(1000);
                this.Navigate(url);
            }
            else
            {
                ((WebBrowser)this).Navigate(url);
            }
        }

        public void CallJsFunction(string functionName, object[] paramList)
        {
            if (functionName == "AlarmLamp")
            {
                BeepWorker.open();
                switch (functionName)
                {
                    case "1"://一级，红灯闪烁+蜂鸣器响
                        Task.Run(() =>
                        {
                            BeepWorker.beep(AlarmLampColor.red, needBeep: true);
                        });
                        break;
                    case "2"://二级，黄灯闪烁+蜂鸣器间断响
                        Task.Run(() =>
                        {
                            BeepWorker.beep(AlarmLampColor.yellow, needBeep: true, alternateBeep: true);
                        });
                        break;
                    case "3"://三级，黄灯闪烁
                        Task.Run(() =>
                        {
                            BeepWorker.beep(AlarmLampColor.yellow);
                        });
                        break;
                    default:
                        break;
                }
            }
            else if (functionName == string.Empty)//北京新机场，新框架页面无法直接调用js，需要执行下面语句
            {
                string jsCode = "window.angularComponent.zone.run(function() { window.angularComponent.component.calledFromOutside(JSON.stringify(" + paramList[0] + "))})";
                DoJavaScriptCode(jsCode);
            }
            else
            {
                this.Document.InvokeScript(functionName, paramList);
            }
        }

        private void DoJavaScriptCode(string js)
        {
            try
            {
                var doc = this.Document.DomDocument as IHTMLDocument2;
                var win = doc.parentWindow as IHTMLWindow2;
                string jscode = js;
                win.execScript(jscode, "javascript");
            }
            catch (Exception)
            {

            }
        }
        public void ShowDevTools() { }
        public new void Dispose()
        {
            ((WebBrowser)this).Dispose();
        }

    }

    public class ChromeBrowser : ChromiumWebBrowser, IHaycBrowser
    {
        public ChromeBrowser(string url) : base(url)
        {
        }

        public void CallJsFunction(string functionName, object[] paramList)
        {
            if (functionName == string.Empty)
            {
                string jsCode = "window.angularComponent.zone.run(function() { window.angularComponent.component.calledFromOutside(JSON.stringify(" + paramList[0] + "))})";
                this.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsCode);
            }
            else
            {
                this.GetBrowser().MainFrame.ExecuteJavaScriptAsync(functionName + "(" + paramList[0].ToString() + ")");
            }
        }

        public void Navigate(string url, string cookieKey = "", string cookieValue = "")
        {
            if (cookieKey != string.Empty && cookieValue != string.Empty)
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                bool a = cookieManager.SetCookie(url, new Cookie()
                {
                    Name = cookieKey,
                    Value = cookieValue,
                    HttpOnly = false,
                    Domain = null,
                    Expires = null
                });
            }
            this.Load(url);
        }

        public void ShowDevTools()
        {
            ((ChromiumWebBrowser)this).ShowDevTools();
        }

        public new void Dispose()
        {
            ((ChromiumWebBrowser)this).Dispose();
        }
    }
}
