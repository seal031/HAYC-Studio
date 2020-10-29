using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngularCallWinformTest
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]//COM+组件可见
    public partial class Form1 : DevComponents.DotNetBar.Office2007Form,IQuartz,IForm
    {
        public Form1()
        {
            InitializeComponent();
            ((WebBrowser)wb1).ObjectForScripting = this;
            ((WebBrowser)wb2).ObjectForScripting = this;
            ((WebBrowser)wb3).ObjectForScripting = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wb1.Navigate(textBox1.Text.Trim());
            wb2.Navigate(textBox1.Text.Trim());
            wb3.Navigate(textBox1.Text.Trim());
        }

        public void Test(string input, params object[] paramList)
        {
            MessageBox.Show("接收到参数" + input +";"+ string.Join(",", paramList));
        }

        public void cyclicWork()
        {
            
        }

        public void DoJavaScriptFuntion(string funcName, params object[] paramList)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr pHandle = IEWebbrowserMemoryReleaser.GetCurrentProcess();
            IEWebbrowserMemoryReleaser.SetProcessWorkingSetSize(pHandle, -1, -1);
        }
    }

    public interface IQuartz
    {
        void cyclicWork();
    }
    public interface IForm
    {
        void DoJavaScriptFuntion(string funcName, params object[] paramList);
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
            ((WebBrowser)this).Navigate(url);
        }

        public void CallJsFunction(string functionName, object[] paramList)
        {

        }

        private void DoJavaScriptCode(string js)
        {
        }
        public void ShowDevTools() { }
        public new void Dispose()
        {
            ((WebBrowser)this).Dispose();
        }

    }

    public class IEWebbrowserMemoryReleaser
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCurrentProcess();
    }
}
