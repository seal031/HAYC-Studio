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
            //server.OnClientMessage += PipeCommunicateServer_ServerMessage;
            //server.startServer();
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
            client.sendMessage("I am client");
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
