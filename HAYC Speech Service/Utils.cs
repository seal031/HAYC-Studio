using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class LogHelper
{
    public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
    private static int byConsole = 0;
    static LogHelper()
    {
        string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
        string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
        string configFilePath = assemblyDirPath + "\\log4net.config";
        XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
    }

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
