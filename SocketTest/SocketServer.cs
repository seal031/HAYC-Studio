using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public class SocketServer : AppServer<SocketSession>
     {
         protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
         {
             Console.WriteLine("正在准备配置文件");
             return base.Setup(rootConfig, config);
         }
 
         protected override void OnStarted()
         {
             Console.WriteLine("服务已开始");
             base.OnStarted();
         }
 
         protected override void OnStopped()
         {
             Console.WriteLine("服务已停止");
             base.OnStopped();
         }
         protected override void OnNewSessionConnected(SocketSession session)
         {
             Console.WriteLine("新的连接地址为" + session.LocalEndPoint.Address.ToString() + ",时间为" + DateTime.Now);
             base.OnNewSessionConnected(session);
         }
     }
}
