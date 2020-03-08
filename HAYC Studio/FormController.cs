using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public class FormController
    {
        public static List<FormInfo> formInfoList = new List<FormInfo>();

        public static void addForm(int index, string pageName, ScreenForm form)
        {
            if (formInfoList.FindIndex(f => f.PageName == pageName) < 0)
            {
                formInfoList.Add(new FormInfo()
                {
                    Index = index,
                    PageName = pageName,
                    Form = form,
                    IsClosed = false
                });
            }
        }

        public static void removeForm(ScreenForm form)
        {
            if (formInfoList.FindIndex(f => f.Form == form) > -1)
            {
                int index = formInfoList.FindIndex(f => f.Form == form);
                formInfoList.RemoveAt(index);
            }
        }


        public static ScreenForm getFormIfExits(string pageName)
        {
            FormInfo fi = formInfoList.Find(f => f.PageName == pageName);
            if (fi == null)
            {
                return null;
            }
            else
            {
                return fi.Form;
            }
        }

        public static void CallFunction(string pageName, string funcName, params object[] paramList)
        {
            FormInfo formInfo = formInfoList.SingleOrDefault(f => f.PageName == pageName);
            if (formInfo == null)
            {
                LogHelper.WriteLog("未找到指定的屏幕，页面名称:" + pageName);
            }
            else
            {
                formInfo.Form.DoJavaScriptFuntion(funcName, paramList);
            }
        }

        public static void closeForm(ScreenForm closedForm)
        {
            FormInfo form = formInfoList.FirstOrDefault(f => f.Form == closedForm);
            if (form != null)
            {
                //MessageBox.Show("窗体关闭");
                form.IsClosed = true;
                if (formInfoList.Where(f => f.IsClosed == true).Count() == formInfoList.Count)//如果所有打开的窗体都关闭了，则退出程序
                {
                    //MessageBox.Show("所有窗体都已关闭，退出程序");
                    System.Environment.Exit(0);
                }
            }
        }
    }

    public class FormInfo
    {
        private int _index;
        private string _pageName;
        private ScreenForm _form;
        private bool _isClosed;

        public string PageName
        {
            get
            {
                return _pageName;
            }

            set
            {
                _pageName = value;
            }
        }

        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }
        public bool IsClosed
        {
            get
            {
                return _isClosed;
            }

            set
            {
                _isClosed = value;
            }
        }

        public ScreenForm Form
        {
            get
            {
                return _form;
            }

            set
            {
                _form = value;
            }
        }

    }
}
