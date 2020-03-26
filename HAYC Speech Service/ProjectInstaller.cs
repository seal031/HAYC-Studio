using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace HAYC_Speech_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
        }

        //public override void Commit(IDictionary savedState)
        //{
        //    base.Commit(savedState);
        //    //服务安装后自动启动
        //    ServiceController sc = new ServiceController("HAYC Speech Seivice");
        //    if (sc.Status.Equals(ServiceControllerStatus.Stopped))
        //    {
        //        sc.Start();
        //    }
        //}
    }
}
