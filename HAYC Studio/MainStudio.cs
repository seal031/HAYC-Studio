using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAYC_Studio
{
    public partial class MainStudio : Form
    {
        public MainStudio()
        {
            InitializeComponent();
        }
        NamedPipeServer<string> server = new NamedPipeServer<string>("MyServerPipe");

        private void MainStudio_Load(object sender, EventArgs e)

        {
            //PipeCommunicate pipe = new PipeCommunicate();
            //pipe.OnGetMessage += Pipe_OnGetMessage;
            //pipe.startGetMessage("testpipe");


            server.ClientMessage += Server_ClientMessage;
            server.Start();
        }

        private void Server_ClientMessage(NamedPipeConnection<string, string> connection, string message)
        {
            server.PushMessage(message+"   "+connection.Id);
        }

        private void Pipe_OnGetMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
