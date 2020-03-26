using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LogHelper
{
    public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
    private static int byConsole = 0;

    /// <summary>
    /// 0：输出到日志；非0：输出到console
    /// </summary>
    public static void setConsole(int value)
    {
        LogHelper.byConsole = value;
    }

    public static void WriteLog(string info)
    {
        if (byConsole != 0)
        {
            Console.WriteLine(info);
        }
        else
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
    }
}
