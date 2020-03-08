using DevComponents.DotNetBar;
using HAYC_Studio.Browser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]//COM+组件可见
    public partial class ScreenForm : Office2007Form, IForm
    {
        BaseVideoForm videoForm;
        IHaycBrowser wb;

        public ScreenForm(BaseVideoForm _videoForm)
        {
            videoForm = _videoForm;
            InitializeComponent();
            this.FormClosed += ScreenForm_FormClosed;
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
        }

        public ScreenForm()
        {
            InitializeComponent();
            this.FormClosed += ScreenForm_FormClosed;
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
        }
        public void initWebBrowser(BrowserType browserType, string url)
        {
            wb = BrowserFactory.createBrowser(browserType, this, url);
        }
        private void ScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.wb.Dispose();
            FormController.removeForm(this);
        }

        /// <summary>
        /// 页面调用c#方法的入口，IE直接调用，Chrome通过ChromeJsHandler的callFunction调用本方法
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="funcName"></param>
        /// <param name="paramList"></param>
        public void CallFunction(string formName, string funcName, params object[] paramList)
        {
            LogHelper.WriteLog("接收到来自页面的调用。被调用窗体：" + formName + " 被调用函数：" + funcName + " 调用参数：" + string.Join(",", paramList));
            if (funcName == "oppenEventVideo")
            {
                if (videoForm != null)
                {
                    videoForm.showVideo(paramList);
                }
            }
            else
            {
                FormController.CallFunction(formName, funcName, paramList);
            }
        }


        public void DoJavaScriptFuntion(string funcName, params object[] paramList)
        {
            wb.CallJsFunction(funcName, paramList);
        }
        private void ScreenForm_Load(object sender, EventArgs e)
        {

        }

        public void Navigate(string url, string cookieKey = "", string cookieValue = "")
        {
            this.wb.Navigate(url, cookieKey, cookieValue);
        }

        private void ScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormController.closeForm(this);
        }

        private void ScreenForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F12)
            {
                wb.ShowDevTools();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wb.ShowDevTools();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CallFunction("视频", "", new object[] { 123 });
        }
        
    }
}
