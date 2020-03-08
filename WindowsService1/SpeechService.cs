using NAudio.CoreAudioApi;
using NAudio.Wave;
using SharpCapture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class SpeechService : ServiceBase
    {

        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
             string lpstrCommand,
             string lpstrReturnString,
             int uReturnLength,
             int hwndCallback);

        public SpeechService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
        WaveInEvent wie;
        WaveFileWriter writer;

        public void OnStart()
        {
            //VoicePicker picker = new WindowsService1.VoicePicker();
            //picker.init();
            //picker.microphoneCapturer.AudioDataCaptured += MicrophoneCapturer_AudioDataCaptured;
            //picker.start();

            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
            Directory.CreateDirectory(outputFolder);
            string outputFilePath = Path.Combine(outputFolder, "recorded.wav");
            wie = new WaveInEvent();
            writer = new WaveFileWriter(outputFilePath, wie.WaveFormat);
            wie.DataAvailable += Wie_DataAvailable;
            wie.RecordingStopped += Wie_RecordingStopped; ;
            wie.StartRecording();
            Thread.Sleep(3000);
            wie.StopRecording();

            //WaveInEvent wie = new WaveInEvent();
            //wie.DataAvailable += Wie_DataAvailable;
            //wie.DeviceNumber = 0;
            //wie.StartRecording();
            //Thread.Sleep(3000);
            //capture.StopRecording(); 

            //start();
            //Thread.Sleep(3000);
            //stop();

            //VoiceCapture vc = new global::VoiceCapture();
            //vc.InitVoice();
            //vc.StartVoiceCapture();
            //Thread.Sleep(3000);
            //vc.Stop();

            //SoundRecord sr = new SoundRecord();
            //sr.SetFileName("123.wav");
            //sr.RecStart();
            //Thread.Sleep(3000);
            //sr.RecStop();
        }


        private void Wie_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            if (writer.Position > wie.WaveFormat.AverageBytesPerSecond * 30)
            {
                wie.StopRecording();
            }
        }

        private void Wie_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
            writer = null;
            wie.Dispose();
            Debug.WriteLine("===============================================================");
        }

        private void Capture_DataAvailable(object sender, WaveInEventArgs e)
        {
            //Debug.WriteLine(string.Join("", e.Buffer));
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            if (writer.Position > wie.WaveFormat.AverageBytesPerSecond * 20)
            {
                wie.StopRecording();
            }
        }
        private void Capture_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
            writer = null;
            wie.Dispose();
            Debug.WriteLine("===============================================================");
        }

        private void MicrophoneCapturer_AudioDataCaptured(byte[] obj)
        {
            Debug.WriteLine(string.Join("", obj));
        }

        private void start()
        {
            mciSendString("set wave bitpersample 8", "", 0, 0);
            mciSendString("set wave samplespersec 20000", "", 0, 0);
            mciSendString("set wave channels 2", "", 0, 0);
            mciSendString("set wave format tag pcm", "", 0, 0);
            mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
            mciSendString("record movie", "", 0, 0);
        }
        private void stop()
        {
            mciSendString("stop movie", "", 0, 0);
            mciSendString("save movie 1.wav", "", 0, 0);
            mciSendString("close movie", "", 0, 0);
        }
    }
    public class VoicePicker
    {

        public SharpCapture.Interface.IMicrophoneCapture microphoneCapturer;


        public void init()
        {
            KeyMgr.SetKey("Test");
            microphoneCapturer = CaptureFactory.GetMicrophoneCapture(0);
        }
        public void start()
        {
            microphoneCapturer.Start();
        }

        public void stop()
        {
            microphoneCapturer.Stop();
        }
    }
}
