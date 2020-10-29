using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_FaceDetect_Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ///属性：AnyCpu，首选32位
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FaceDetectService()
            };
            ServiceBase.Run(ServicesToRun);

            //FaceDetectService service = new FaceDetectService();
            //service.OnStart();
        }
    }
}
