using HAYC_ProcessCommunicate_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public class PipeCommunicateServerWorker
    {
        static PipeCommunicateServer server;
        public PipeCommunicateServerWorker(PipeCommunicateServer server)
        {
            PipeCommunicateServerWorker.server = server;
        }

        public static void sendMessage(string message)
        {
            server.sendMessage(message);
        }

        public static void sendSetting(ConfigInfoSpeech config)
        {
            SpeechSetting setting = new HAYC_ProcessCommunicate_Library.SpeechSetting();
            setting.BufferMilliseconds = config.BufferMilliseconds;
            setting.EndPickAdditional = config.EndPickAdditional;
            setting.MicVolumnPickerSleepSecond = config.MicVolumnPickerSleepSecond;
            setting.VolumnCommandThreshold = 0.002f * (100 - config.micSensitivity);
            setting.VolumnSleepThreshold = 0.005f * (100 - config.micSensitivity);
            setting.UnKownTimes = config.UnKownTimes;
            ProcessCommunicateMessage pcm = new ProcessCommunicateMessage();
            pcm.MessageType = CommunicateMessageType.SETTING;
            pcm.ProcessName = "";
            pcm.Message = setting.toJson();
            var message = pcm.toJson();
            sendMessage(message);
        }
    }
}
