using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class LogHelper
{
    static LogHelper()
    {
        string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
        string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
        string configFilePath = assemblyDirPath + "\\log4net.config";
        XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
    }
    public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
    public static void WriteLog(string info)
    {
        if (loginfo.IsInfoEnabled)
        {
            loginfo.Info(info);
        }
    }
}

public class TranscateHelper
{
    public static byte[] stringToBytes(string inputStr, string splitStr)
    {
        string[] inputArray = inputStr.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);
        byte[] returnByteArray = new byte[inputArray.Length];
        for (int i=0;i<inputArray.Length;i++)
        {
            string s = inputArray[i];
            byte b;
            if (byte.TryParse(s, out b))
            {
                returnByteArray[i] = b;
            }
            else
            {
                throw new Exception("输入的字符串无法转为数字");
            }
        }
        return returnByteArray;
    }
}
