using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_ProcessCommunicate_Library
{
    public class SocketServer
    {
        public static AppServer server;
        public static AppSession session;

        public static void init()
        {
            server = new AppServer();
            var config = new SuperSocket.SocketBase.Config.ServerConfig()
            {
                Name = "SSServer",
                ServerTypeName = "SServer",
                ClearIdleSession = false,
                MaxRequestLength = 2048,
                Ip = "Any",
                Port = 20020,
                MaxConnectionNumber = 10,
                TextEncoding = "gb2312"
            };
            server.Setup(config);
        }

        public static void start()
        {
            if (server == null) { throw new Exception("server尚未初始化"); }
            if (server.State == ServerState.NotInitialized) { throw new Exception("server尚未初始化"); }
            if (server.State == ServerState.Initializing) { throw new Exception("server正在初始化，请稍后再试"); }
            if (server.State == ServerState.Starting) { throw new Exception("server正在启动中"); }
            if (server.State == ServerState.Stopping) { throw new Exception("server正在停止"); }
            if (server.State != ServerState.Running )
            {
                server.Start();
            }
        }

        public static void stop()
        {
            if (server == null) { throw new Exception("server尚未初始化"); }
            if (server.State == ServerState.NotInitialized) { throw new Exception("server尚未初始化"); }
            if (server.State == ServerState.Initializing) { throw new Exception("server正在初始化，请稍后再试"); }
            if (server.State == ServerState.Starting) { throw new Exception("server正在启动中"); }
            if (server.State == ServerState.Stopping) { throw new Exception("server正在停止"); }
            if (server.State == ServerState.Running)
            {
                server.Stop();
            }
        }

        public static void send(string message)
        {
            if (session == null) { throw new Exception("sesseion尚未初始化"); }
            if (session.Connected)
            {
                try
                {
                    session.Send(message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    throw new Exception("发送数据失败，异常为"+ex.Message);
                }
            }
            else
            {
                throw new Exception("session已断开连接，无法发送数据");
            }
        }
    }
}
