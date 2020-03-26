using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Speech_Service
{
    /// <summary>
    /// 声音输出类，用于语音交互的系统反馈
    /// </summary>
    //public class Speaker
    //{
    //    public static readonly string TextOnWake = "在呢，请问有什么需要";
    //    public static readonly string TextOnCommand = "收到，正在执行";
    //    public static readonly string TextOnUnkown = "未能识别指令";
    //    public static readonly string TextOnSleep = "小安正在休眠";

    //    private static SpeechSynthesizer speaker = new SpeechSynthesizer();
    //    private static SpeechWorker worker;

    //    // <summary>
    //    // 初始化
    //    // </summary>
    //    // <param name = "_worker" ></ param >
    //    public static void init(SpeechWorker _worker)
    //    {
    //        worker = _worker;
    //        speaker.SpeakCompleted += Speaker_SpeakCompleted;
    //    }

    //    public static void dispose()
    //    {
    //        speaker.Dispose();
    //        speaker = null;
    //    }

    //    // <summary>
    //    // 设置语速1-5
    //    // </summary>
    //    // <param name = "speed" ></ param >
    //    public static void setSpeed(int speed)
    //    {
    //        speaker.Rate = speed;
    //    }

    //    private static void Speaker_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
    //    {
    //        worker.startPick(); // 交互结束后，才可以让麦克风继续采集声音。因为交互会有声音输出，而这个输出不需要被识别.
    //    }

    //    // <summary>
    //    // 输出声音
    //    // </summary>
    //    // <param name = "text" > 要输出的文字 </ param >
    //    public static void speech(string text)
    //    {
    //        speaker.SpeakAsync(text);
    //    }
    //}

    public class Speaker
    {
        public static readonly string TextOnWake = @"\voices\OnWake.mp3";
        public static readonly string TextOnCommand = @"\voices\OnCommand.mp3";
        public static readonly string TextOnUnkown = @"D:\voices\OnUnkown.wav";
        public static readonly string TextOnSleep = @"\voices\OnSleep.mp3";

        private static SoundPlayer speaker;
        private static SpeechWorker worker;

        public static void init(SpeechWorker _worker)
        {
            //worker = _worker;
            //speaker = new SoundPlayer();
        }


        public static void dispose()
        {
            //speaker.Dispose();
            //speaker = null;
        }

        public static void speech(string filePath)
        {
            //speaker.SoundLocation = filePath;
            //speaker.PlaySync();
        }
    }
}
