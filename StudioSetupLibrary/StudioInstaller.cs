using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudioSetupLibrary
{
    [RunInstaller(true)]
    public partial class StudioInstaller : System.Configuration.Install.Installer
    {
        string setupPath;

        public StudioInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            //LogWrite("OnAfterInstall！");
            //string setupPath= this.Context.Parameters["targetdir"];
            setupPath = this.Context.Parameters["targetdir"];
            try
            {
                string path = setupPath + @"SpeechService\install.bat";
                path = path.Replace(@"\\", @"\");
                LogWrite(path);
                //System.Diagnostics.Process.Start(path);
                using (Process proc = new Process())
                {
                    string command = path;
                    proc.StartInfo.FileName = command;
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(command);

                    //run as admin
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                    while (!proc.HasExited)
                    {
                        proc.WaitForExit(3000);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.Message);
            }
            base.OnAfterInstall(savedState);
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            //LogWrite("OnAfterInstall！");
            //string setupPath = this.Context.Parameters["targetdir"];
            setupPath = this.Context.Parameters["targetdir"];
            try
            {
                string path = setupPath + @"SpeechService\uninstall.bat";
                path = path.Replace(@"\\", @"\");
                //System.Diagnostics.Process.Start(path);
                using (Process proc = new Process())
                {
                    string command = path;
                    proc.StartInfo.FileName = command;
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(command);

                    //run as admin
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                    while (!proc.HasExited)
                    {
                        proc.WaitForExit(3000);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.Message);
            }
            base.OnAfterUninstall(savedState);
        }

        public void LogWrite(string str)
        {
            string LogPath = setupPath.Replace(@"\\", @"\"); ;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"SetUpLog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");

            }
        }
    }
}
