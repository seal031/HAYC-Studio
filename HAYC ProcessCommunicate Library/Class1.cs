using NamedPipeWrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 跨进程交互类库
/// </summary>
namespace HAYC_ProcessCommunicate_Library
{
    #region 应用程序句柄交互
    /// <summary>
    /// 使用应用程序句柄进行跨进程交互
    /// </summary>
    public static class HandlerCommunicate
    {
        [DllImport("User32.dll")]
        private static extern int SendMessage(int hwnd, int msg, int wParam, ref COPYDATASTRUCT IParam);


        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        public const int WM_COPYDATA = 0x004A;

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        public static void sendMessage(ProcessCommunicateMessage message)
        {
            int WINDOW_HANDLE = FindWindow(null, message.ProcessName);
            if (WINDOW_HANDLE != 0)
            {
                string jsonStr = message.toJson();
                byte[] arr = System.Text.Encoding.Default.GetBytes(jsonStr);
                int len = arr.Length;
                COPYDATASTRUCT cdata;
                cdata.dwData = (IntPtr)100;
                cdata.lpData = jsonStr;
                cdata.cData = len + 1;
                SendMessage(WINDOW_HANDLE, WM_COPYDATA, 0, ref cdata);
            }
        }
    }
    #endregion

    #region 管道交互
    /// <summary>
    /// 使用命名管道交互
    /// </summary>
    public class PipeCommunicateClient
    {
        /// <summary>
        /// 从管道获取消息的事件，配合startGetMessage使用
        /// </summary>
        public event ConnectionMessageEventHandler<string, string> OnServerMessage;
        NamedPipeClient<string> pipeClient;
        Thread thread;
        bool isConnected = false;

        public PipeCommunicateClient(string pipeName)
        {
            if (pipeClient == null)
            {
                pipeClient = new NamedPipeClient<string>(pipeName);
                pipeClient.AutoReconnect = true;
                pipeClient.ServerMessage += PipeClient_ServerMessage;
                pipeClient.Disconnected += PipeClient_Disconnected;
            }
        }

        public void startClient()
        {
            if (isConnected == false)
            {
                pipeClient.Start(); 
                isConnected = true;
            }
        }

        public void stopClient()
        {
            if (isConnected)
            {
                pipeClient.Stop();
                pipeClient = null;
            }
        }

        /// <summary>
        /// 向指定name的管道发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void sendMessage(string message)
        {
            if (pipeClient == null)
            {
                throw new Exception("pipeClient未实例化");
            }
            if (isConnected == false)
            {
                throw new Exception("pipeClient未连接");
            }
            pipeClient.PushMessage(message);
        }

        private void PipeClient_Disconnected(NamedPipeConnection<string, string> connection)
        {
            //isConnected = false;
        }

        private void PipeClient_ServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            OnServerMessage(connection, message);
        }
    }

    public class PipeCommunicateServer
    {
        NamedPipeServer<string> pipeServer;
        /// <summary>
        /// 从管道获取消息的事件，配合startGetMessage使用
        /// </summary>
        public event ConnectionMessageEventHandler<string, string> OnClientMessage;
        bool isConnected = false;

        public PipeCommunicateServer(string pipeName)
        {
            if (pipeServer == null)
            {
                pipeServer = new NamedPipeServer<string>(pipeName);
                pipeServer.ClientConnected += PipeServer_ClientConnected;
                pipeServer.ClientMessage += PipeServer_ClientMessage;
                pipeServer.Error += PipeServer_Error;
            }
        }

        public void startServer()
        {
            if (isConnected == false)
            {
                pipeServer.Start();
                isConnected = true;
            }
        }

        private void PipeServer_Error(Exception exception)
        {
           
        }

        private void PipeServer_ClientConnected(NamedPipeConnection<string, string> connection)
        {
            
        }

        private void PipeServer_ClientMessage(NamedPipeConnection<string, string> connection, string message)
        {
            OnClientMessage(connection, message);
        }

        public void sendMessage(string message)
        {
            if (pipeServer == null)
            {
                throw new Exception("pipeServer未实例化");
            }
            if (isConnected == false)
            {
                throw new Exception("pipeServer未连接");
            }
            pipeServer.PushMessage(message);
        }
    }
    #endregion

    /// <summary>
    /// 跨进程交互消息对象
    /// </summary>
    public class ProcessCommunicateMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public CommunicateMessageType MessageType { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 接收消息的进程名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 序列化消息对象
        /// </summary>
        /// <returns>序列化后的消息对象</returns>
        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// 反序列化消息对象
        /// </summary>
        /// <param name="json">反序列化的字符串
        /// </param>
        /// <returns>反序列化后的消息对象</returns>
        public static ProcessCommunicateMessage fromJson(string json)
        {
            try
            {
                ProcessCommunicateMessage message = JsonConvert.DeserializeObject<ProcessCommunicateMessage>(json) as ProcessCommunicateMessage;
                return message;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 跨进程交互消息类型
    /// </summary>
    public enum CommunicateMessageType
    {
        /// <summary>
        /// 命令，指令
        /// </summary>
        COMMAND,
        /// <summary>
        /// 启动
        /// </summary>
        START,
        /// <summary>
        /// 停止
        /// </summary>
        STOP,
        /// <summary>
        /// 获取人脸识别得分
        /// </summary>
        SCORE,
        /// <summary>
        /// 获取人脸特征码
        /// </summary>
        FEAT,
        /// <summary>
        /// 语音识别结果
        /// </summary>
        SPEECHRESULT,
        /// <summary>
        /// 普通文本消息
        /// </summary>
        MESSAGE,
        /// <summary>
        /// 主程序对服务的设置
        /// </summary>
        SETTING
    }
    
    /// <summary>
    /// 跨进程交互的语音识别设置对象
    /// </summary>
    public class SpeechSetting
    {
        public float VolumnSleepThreshold { get; set; }
        public float VolumnCommandThreshold { get; set; }
        public int BufferMilliseconds { get; set; }
        //public int SpeechSpeed { get; set; }
        public int EndPickAdditional { get; set; }
        public int MicVolumnPickerSleepSecond { get; set; }
        public int UnKownTimes { get; set; }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static SpeechSetting fromJson(string json)
        {
            return JsonConvert.DeserializeObject<SpeechSetting>(json);
        }
    }

    /// <summary>
    /// 跨进程交互的人脸识别设置对象
    /// </summary>
    public class FaceSetting
    {
        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FaceSetting fromJson(string json)
        {
            return JsonConvert.DeserializeObject<FaceSetting>(json);
        }

    }
}
