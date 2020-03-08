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

namespace HAYC_Speech_Service.Communicate
{
    #region 程序句柄交互
    public static class ProcessCommunicate
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
    public class PipeCommunicate
    {
        public delegate void getMessageHandle(string message);
        public event getMessageHandle OnGetMessage;
        NamedPipeClientStream pipeClient;
        Thread thread;

        public void sendMessage(string pipeName, string message)
        {
            pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
            pipeClient.Connect();
            using (StreamReader sr = new StreamReader(pipeClient))
            {
                var data = new byte[10240];
                data = System.Text.Encoding.Default.GetBytes(message);
                pipeClient.Write(data, 0, data.Length);
            }
        }

        public void startGetMessage(string pipeName)
        {
            if (thread == null)
            {
                thread = new Thread(new ParameterizedThreadStart(getMessage));

            }
            thread.Start(pipeName);
        }
        private void getMessage(object pipeName)
        {
            while (true)
            {
                using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName.ToString(), PipeDirection.InOut))
                {
                    pipeServer.ReadMode = PipeTransmissionMode.Byte;
                    pipeServer.WaitForConnection();
                    var data = new byte[10240];
                    var count = pipeServer.Read(data, 0, 10240);
                    string message = Encoding.Default.GetString(data);
                    OnGetMessage(message);
                }
            }
        }
    }
    #endregion


    #region 消息体
    public class ProcessCommunicateMessage
    {
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string ProcessName { get; set; }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static ProcessCommunicateMessage toObj(string json)
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
    #endregion
}
