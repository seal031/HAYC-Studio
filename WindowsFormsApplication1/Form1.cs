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

        PipeCommunicateServer server1 = new PipeCommunicateServer("SpeechPipe");
        PipeCommunicateClient client1 = new PipeCommunicateClient("test1");

        private void Form1_Load(object sender, EventArgs e)
        {
            server.OnClientMessage += PipeCommunicateServer_ServerMessage;
            server.startServer();
            client.OnServerMessage += PipeCommunicateClient_ServerMessage;
            client.startClient();
            server1.OnClientMessage += PipeCommunicateServer_ServerMessage;
            server1.startServer();
            client1.OnServerMessage += PipeCommunicateClient_ServerMessage;
            client1.startClient();
        }

        private void PipeCommunicateServer_ServerMessage(NamedPipeWrapper.NamedPipeConnection<string, string> connection, string message)
        {
            MessageBox.Show("服务端收到消息:"+message);
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
    }
}
