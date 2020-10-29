using HAYC_ProcessCommunicate_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public class SpeechPipeCommunicateServerWorker
    {
        static PipeCommunicateServer server;
        public SpeechPipeCommunicateServerWorker(PipeCommunicateServer server)
        {
            SpeechPipeCommunicateServerWorker.server = server;
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

    public class FacePipeCommunicateServerWorker
    {
        static PipeCommunicateServer server;
        public FacePipeCommunicateServerWorker(PipeCommunicateServer server)
        {
            FacePipeCommunicateServerWorker.server = server;
        }

        public static void sendMessage(string message)
        {
            server.sendMessage(message);
        }
    }
}
