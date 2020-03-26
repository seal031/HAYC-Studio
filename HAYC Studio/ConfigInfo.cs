using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public class ConfigInfoMain
    {
        
    }

    /// <summary>
    /// 语音识别相关配置
    /// </summary>
    public class ConfigInfoSpeech
    {
        public int speechAutoStart { get; set; }
        public int micSensitivity { get; set; }

        public int BufferMilliseconds { get; set; }
        public int EndPickAdditional { get; set; }
        public int MicVolumnPickerSleepSecond { get; set; }
        public int UnKownTimes { get; set; }

        public static ConfigInfoSpeech LoadConfig()
        {
            try
            {
                ConfigInfoSpeech config = new HAYC_Studio.ConfigInfoSpeech();
                config.speechAutoStart = int.Parse(ConfigWorker.GetConfigValue("speechAutoStart"));
                config.micSensitivity = int.Parse(ConfigWorker.GetConfigValue("micSensitivity"));
                config.BufferMilliseconds = int.Parse(ConfigWorker.GetConfigValue("BufferMilliseconds"));
                config.EndPickAdditional = int.Parse(ConfigWorker.GetConfigValue("EndPickAdditional"));
                config.MicVolumnPickerSleepSecond = int.Parse(ConfigWorker.GetConfigValue("MicVolumnPickerSleepSecond"));
                config.UnKownTimes = int.Parse(ConfigWorker.GetConfigValue("UnKownTimes"));
                return config;
            }
            catch (Exception ex)
            {
                throw new Exception("加载配置出现异常：" + ex.Message);
            }
        }

        public static void SaveConfig(ConfigInfoSpeech config)
        {
            try
            {
                ConfigWorker.SetConfigValue("speechAutoStart", config.speechAutoStart.ToString());
                ConfigWorker.SetConfigValue("micSensitivity", config.micSensitivity.ToString());
                ConfigWorker.SetConfigValue("BufferMilliseconds", config.BufferMilliseconds.ToString());
                ConfigWorker.SetConfigValue("EndPickAdditional", config.EndPickAdditional.ToString());
                ConfigWorker.SetConfigValue("MicVolumnPickerSleepSecond", config.MicVolumnPickerSleepSecond.ToString());
                ConfigWorker.SetConfigValue("UnKownTimes", config.UnKownTimes.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("保持配置出现异常："+ex.Message);
            }
        }
    }

    public class ConfigInfoFace
    {

    }
}
