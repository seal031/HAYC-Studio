using NAudio.Wave;
using SharpCapture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Speech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SharpCapture.Interface.IMicrophoneCapture microphoneCapturer;


        WaveInEvent waveIn;
        WaveFileWriter writer;

        public void init()
        {
            KeyMgr.SetKey("Test");
            microphoneCapturer = CaptureFactory.GetMicrophoneCapture(0);
            microphoneCapturer.AudioDataCaptured += MicrophoneCapturer_AudioDataCaptured;
        }

        public void initNaudio()
        {
            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
            Directory.CreateDirectory(outputFolder);
            string outputFilePath = Path.Combine(outputFolder, "recorded.wav");
            waveIn = new WaveInEvent();
            waveIn.DataAvailable += Capture_DataAvailable;
            waveIn.RecordingStopped += Capture_RecordingStopped;
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
        }

        private void Capture_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
            writer = null;
            waveIn.Dispose();
        }

        private void MicrophoneCapturer_AudioDataCaptured(byte[] obj)
        {
            Debug.WriteLine(string.Join("",obj));
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            //microphoneCapturer.Start();
            waveIn.StartRecording();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            //microphoneCapturer.Stop();
            waveIn.StopRecording();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //init();
            initNaudio();
        }
        private void Wie_DataAvailable(object sender, WaveInEventArgs e)
        {
            Debug.WriteLine(string.Join("", e.Buffer));
        }

        private void Capture_DataAvailable(object sender, WaveInEventArgs e)
        {
            //Debug.WriteLine(string.Join("", e.Buffer));
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 20)
            {
                waveIn.StopRecording();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
