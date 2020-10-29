using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.SocketService;
using SuperSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SocketTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //#region 初始化Socket
            //IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
            //if (!bootstrap.Initialize())
            //{
            //    Console.WriteLine(DateTime.Now + ":Socket初始化失败\r\n");
            //    return;
            //}

            //var result = bootstrap.Start();
            //foreach (var server in bootstrap.AppServers)
            //{
            //    if (server.State == ServerState.Running)
            //    {
            //        Console.WriteLine(DateTime.Now + ":serverName为:" + server.Name + "Socket运行中\r\n");
            //        Console.Read();
            //    }
            //    else
            //    {
            //        Console.WriteLine(DateTime.Now + ":serverName为:" + server.Name + "Socket启动失败\r\n");
            //    }
            //}
            //#endregion

            var app = new AppServer();
            var config = new SuperSocket.SocketBase.Config.ServerConfig()
            {
                Name = "SSServer",
                ServerTypeName = "SServer",
                ClearIdleSession = false, 
                MaxRequestLength = 2048, //最大包长度
                Ip = "Any",
                Port = 18888,
                MaxConnectionNumber = 10,
                TextEncoding="gb2312"
            };
            app.Setup(config);
            app.NewSessionConnected += App_NewSessionConnected;
            app.NewRequestReceived += App_NewRequestReceived;
            app.SessionClosed += App_SessionClosed;
            app.Start();
        }

        private void App_SessionClosed(AppSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Debug.WriteLine("客户端断开");
        }

        private void App_NewRequestReceived(AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            Debug.WriteLine("客户端发来数据"+requestInfo.Key+" "+requestInfo.Body);
            session.Send("客户端发来数据" + requestInfo.Key + " " + requestInfo.Body);
        }

        private void App_NewSessionConnected(AppSession session)
        {
            Debug.WriteLine("客户端连接");
        }
    }
}
