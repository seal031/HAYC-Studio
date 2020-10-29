using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    /// <summary>
    /// 报警灯控制类（串口）
    /// </summary>
    public class BeepWorker
    {
        static CountdownEvent cd = new CountdownEvent(1);
        static SerialPortStream SerialPort;
        static int alarmLampSecond = 5;
        static bool alarmBeepEnable = true;

        static BeepWorker()
        {
            try
            {
                alarmLampSecond = int.Parse(ConfigWorker.GetConfigValue("alarmLampSecond"));
                alarmBeepEnable = (ConfigWorker.GetConfigValue("alarmBeepEnable") == "1" ? true : false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("报警灯闪烁时间设置格式不正确，闪烁时间将默认为5秒");
            }
        }

        public static void init(string port)
        {
            SerialPort = new SerialPortStream(port, 9600);
        }

        public static void open()
        {
            if (SerialPort == null)
            {
                throw new Exception("串口连接对象尚未初始化。请先调用init方法进行初始化");
            }
            else if (SerialPort.IsOpen == false)
            {
                try
                {
                    SerialPort.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("打开串口失败，原因是：" + ex.Message);
                }
            }
        }
        public static void close()
        {
            if (SerialPort == null)
            {
                throw new Exception("串口连接对象尚未初始化。请先调用init方法进行初始化");
            }
            else if (SerialPort.IsOpen)
            {
                try
                {
                    SerialPort.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("关闭串口失败，原因是：" + ex.Message);
                }
            }
        }

        public static void sendDate(int data)
        {
            try
            {
                SerialPort.WriteByte(Convert.ToByte(data));
            }
            catch (Exception ex)
            {
                throw new Exception("向串口发送数据" + data.ToString() + "失败，原因是：" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">报警灯颜色</param>
        /// <param name="needBeep">是否有报警声音</param>
        /// <param name="alternateBeep">是否是间隔报警声音</param>
        public static void beep(AlarmLampColor color, bool needBeep = false, bool alternateBeep = false)
        {
            switch (color)
            {
                case AlarmLampColor.red:
                    sendDate(0x41);
                    break;
                case AlarmLampColor.yellow:
                    sendDate(0x42);
                    break;
                case AlarmLampColor.green:
                    sendDate(0x44);
                    break;
                default:
                    break;
            }
            if (needBeep && alarmBeepEnable)
            {
                if (alternateBeep)
                {
                    sendDate(0x18);//报警声持续响
                }
                else
                {
                    sendDate(0x48);//报警声间隔响
                }
            }
            //LogHelper.WriteLog("+1");
            cd.AddCount();
            //LogHelper.WriteLog(cd.CurrentCount.ToString());
            Thread.Sleep(1000 * alarmLampSecond);
            cd.Signal();
            //LogHelper.WriteLog("减少");
            //LogHelper.WriteLog(cd.CurrentCount.ToString());
            if (cd.CurrentCount == 1)
            {
                //LogHelper.WriteLog("停止");
                switch (color)
                {
                    case AlarmLampColor.red:
                        sendDate(0x21);
                        break;
                    case AlarmLampColor.yellow:
                        sendDate(0x22);
                        break;
                    case AlarmLampColor.green:
                        sendDate(0x24);
                        break;
                    default:
                        break;
                }
                if (needBeep && alarmBeepEnable)
                {
                    sendDate(0x28);//报警声关闭
                }
            }
            else
            {
                //LogHelper.WriteLog("还在闪，不能停");
            }
        }
    }

    public enum AlarmLampColor
    {
        red,
        yellow,
        green
    }
}
