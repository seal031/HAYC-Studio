using AxWMPLib;
using WMPLib;
using HAYC_ProcessCommunicate_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PipeCommunicateServer server = new PipeCommunicateServer("FaceDetectPipe");
        PipeCommunicateClient client = new PipeCommunicateClient("test");

        PipeCommunicateServer server1 = new PipeCommunicateServer("1");
        PipeCommunicateClient client1 = new PipeCommunicateClient("SpeechPipe");

        Speaker speaker;

        private void Form1_Load(object sender, EventArgs e)
        {
            server.OnClientMessage += PipeCommunicateServer_ServerMessage;
            server.startServer();
            //client.OnServerMessage += PipeCommunicateClient_ServerMessage;
            //client.startClient();
            server1.OnClientMessage += PipeCommunicateServer_ServerMessage;
            server1.startServer();
            //speaker = new Speaker(axWindowsMediaPlayer1, server1);
            client1.OnServerMessage += Client1_OnServerMessage;
            client1.startClient();
        }

        private void Client1_OnServerMessage(NamedPipeWrapper.NamedPipeConnection<string, string> connection, string message)
        {
            MessageBox.Show(message);   
        }

        private void PipeCommunicateServer_ServerMessage(NamedPipeWrapper.NamedPipeConnection<string, string> connection, string message)
        {
            MessageBox.Show("服务端收到消息:" + message);
            //Console.WriteLine(message);
            //var messageObj = ProcessCommunicateMessage.fromJson(message);
            //speaker.play(messageObj.Message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage message = new ProcessCommunicateMessage();
            message.ProcessName = "";
            message.MessageType = CommunicateMessageType.FEAT;
            message.Message = @"E:\vyface\TestImgSource\seal1.jpg";
            server.sendMessage(message.toJson());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //client.sendMessage("I am client");
            ProcessCommunicateMessage message = new ProcessCommunicateMessage();
            message.ProcessName = "";
            message.MessageType = CommunicateMessageType.SCORE;
            message.Message = @"E:\vyface\TestImgSource\seal2.jpg#28.24.179.62.51.201.183.191.66.134.81.191.44.52.101.192.40.245.229.190.36.199.240.189.68.190.186.191.228.39.193.63.22.135.216.191.213.92.18.192.64.85.255.189.121.5.52.64.247.111.208.61.200.198.71.64.0.199.38.192.208.1.105.191.224.198.153.63.122.155.49.192.19.123.185.63.196.120.26.191.152.151.147.63.33.69.186.191.143.76.164.62.169.29.49.63.249.238.232.63.109.222.45.64.172.156.45.192.68.59.233.191.156.239.195.191.148.172.103.63.175.115.72.192.198.248.43.192.245.196.180.63.14.95.35.63.153.244.227.190.10.152.26.64.60.32.126.191.126.79.0.192.22.111.57.64.100.36.185.191.34.15.60.192.48.49.24.63.59.165.34.192.92.19.226.61.160.204.92.63.187.237.187.62.192.150.206.63.113.29.209.190.118.145.119.63.193.8.231.191.2.76.211.191.110.113.28.191.11.82.30.63.24.243.231.61.145.17.193.63.160.209.48.61.120.157.32.63.146.95.248.191.138.21.154.190.89.254.194.63.176.53.119.191.0.172.28.63.40.68.139.63.55.12.184.62.165.203.20.62.239.230.164.63.7.18.226.62.17.63.15.192.196.168.93.64.84.12.161.190.144.238.33.191.192.92.119.64.152.169.160.190.68.3.17.191.135.22.207.63.42.68.4.190.154.41.147.63.43.239.0.64.212.154.73.61.206.140.18.64.24.109.198.190.208.112.229.190.244.127.24.63.80.40.149.61.54.183.227.191.0.211.151.189.39.86.146.191.52.86.197.63.193.32.12.64.71.0.45.64.84.197.125.62.19.119.148.63.213.221.97.64.202.69.230.63.12.171.24.64.152.127.109.63.189.165.67.64.0.247.165.63.44.170.38.64.140.107.223.189.180.74.98.63.76.195.138.63.84.126.13.192.218.125.228.62.178.99.208.191.14.51.65.62.168.50.243.62.199.20.187.62.197.130.20.64.78.112.64.192.185.40.243.190.77.36.170.190.47.221.242.191.208.133.244.62.106.90.151.64.104.182.16.190.242.165.10.192.68.17.207.61.50.99.195.191.122.141.149.62.162.2.217.191.117.242.94.64.248.66.161.191.143.39.11.191.0.95.102.61.94.18.220.63.98.192.170.191.107.212.156.61.12.1.56.49.102.101.36.86.29.45.65.46.32.68.82.51.96.54.103.17.8.47.22.34.104.7.110.94.120.115.112.52.42.126.113.58.123.3.107.16.70.99.14.93.24.71.83.88.20.72.63.33.21.28.41.91.109.77.85.122.114.78.66.108.5.18.23.106.76.73.74.125.119.111.92.38.40.81.55.64.11.39.62.53.9.59.127.44.19.79.48.60.75.13.80.67.26.30.57.118.31.116.0.90.97.124.50.69.10.84.6.43.2.95.121.117.27.4.37.105.89.98.61.25.87.15.35.100";
            server.sendMessage(message.toJson());
        }

        private void PipeCommunicateClient_ServerMessage(NamedPipeWrapper.NamedPipeConnection<string, string> connection, string message)
        {
            MessageBox.Show("客户端收到消息:" + message);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            server1.sendMessage("I am server1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client1.sendMessage("I am client1");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage m = new ProcessCommunicateMessage();
            m.MessageType = CommunicateMessageType.START;
            m.Message = "";
            m.ProcessName = "";
            var message = m.toJson();
            server1.sendMessage(message);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage m = new ProcessCommunicateMessage();
            m.MessageType = CommunicateMessageType.STOP;
            m.Message = "";
            m.ProcessName = "";
            var message = m.toJson();
            server1.sendMessage(message);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage m = new ProcessCommunicateMessage();
            m.MessageType = CommunicateMessageType.SETTING;
            m.ProcessName = "";
            SpeechSetting setting = new SpeechSetting();
            setting.BufferMilliseconds = 25;
            setting.EndPickAdditional = 5;
            setting.MicVolumnPickerSleepSecond = 30;
            setting.VolumnSleepThreshold = 0.05f;
            setting.VolumnCommandThreshold = 0.20f;
            m.Message = setting.toJson();
            var message = m.toJson();
            server1.sendMessage(message);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage m = new ProcessCommunicateMessage();
            m.MessageType = CommunicateMessageType.SETTING;
            m.ProcessName = "";
            SpeechSetting setting = new SpeechSetting();
            setting.BufferMilliseconds = 25;
            setting.EndPickAdditional = 5;
            setting.MicVolumnPickerSleepSecond = 30;
            setting.VolumnSleepThreshold = 0.05f;
            setting.VolumnCommandThreshold = 0.20f;
            m.Message = setting.toJson();
            var message = m.toJson();
            server1.sendMessage(message);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"D:\voices\OnCommand.mp3";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ProcessCommunicateMessage m = new ProcessCommunicateMessage();
            m.MessageType = CommunicateMessageType.SPEECHRESULT;
            m.ProcessName = "";
            m.Message = "小安小安";

            var message = m.toJson();
            client1.sendMessage(message);
        }
    }
}
