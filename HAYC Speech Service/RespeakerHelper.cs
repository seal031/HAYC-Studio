using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 麦克风音量采集
/// </summary>
public class MicVolumnPicker
{
    private static MicPickerState state = MicPickerState.ISSLEEP;

    /// <summary>
    /// 麦克风音量休眠阈值（用于判断语音何时休眠）
    /// </summary>
    public static readonly float volumnSleepThreshold = 0.03f;
    /// <summary>
    /// 麦克风音量指令结束输入阈值（用于判断用户是否说完一个指令）
    /// </summary>
    public static readonly float volumnCommandThreshold = 0.11f;
    /// <summary>
    /// 最后一次音量超过指令结束阈值的时间
    /// </summary>
    public static DateTime lastLoudlyTime;
    /// <summary>
    /// 最后一条指令的识别时间
    /// </summary>
    public static DateTime lastCommandTime = DateTime.Now;

    static MMDeviceEnumerator enumerator;
    static MMDevice[] CaptureDevices;
    static MMDevice selectedDevice;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="micIndex"></param>
    public static void init()
    {
        enumerator = new MMDeviceEnumerator();
        CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
        var respeakerIndex = getRespeakerIndex();
        selectedDevice = CaptureDevices[respeakerIndex];// enumerator.GetDevice(micIndex.ToString());
    }

    private static int getRespeakerIndex()
    {
        for (int i = 0; i < CaptureDevices.Length; i++)
        {
            if (CaptureDevices[i].DeviceFriendlyName.ToLower().Contains("respeaker"))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 获取麦克风音量
    /// </summary>
    public static float getVolumn()
    {
        //var volumn = selectedDevice.AudioMeterInformation.MasterPeakValue;
        var volumn = CaptureDevices[1].AudioMeterInformation.MasterPeakValue;
        //Console.WriteLine(volumn);
        if (volumn > volumnCommandThreshold)//如果听到大声说话
        {
            Console.WriteLine("更新大声说话时间");
            lastLoudlyTime = DateTime.Now;//更新上次大声说话时间
        }
        return volumn;
    }

    public static MicPickerState getState()
    {
        return state;
    }

    public static void setSleep()
    {
        Console.WriteLine("开始休眠");
        state = MicPickerState.ISSLEEP;
    }
    public static void setWaitingCommand()
    {
        Console.WriteLine("等待指令");
        state = MicPickerState.WAITINGCOMMAND;
    }
    public static void setListingCommand()
    {
        Console.WriteLine("正在聆听指令");
        state = MicPickerState.LISTINGCOMMAND;
    }
}

public class MicPicker
{

    /// <summary>
    /// 获取系统中respeaker麦克风的索引
    /// </summary>
    /// <returns></returns>
    public static int getRespeakerIndex()
    {
        int result = 0;
        for (int i = 0; i < WaveInEvent.DeviceCount; i++)
        {
            var device = WaveInEvent.GetCapabilities(i);
            if (device.ProductName.ToLower().Contains("respeaker"))
            {
                return i;
            }
        }
        return result;
    }
}

public enum MicPickerState
{
    /// <summary>
    /// 休眠
    /// </summary>
    ISSLEEP,
    /// <summary>
    /// 等待接收指令
    /// </summary>
    WAITINGCOMMAND,
    /// <summary>
    /// 正在接收指令
    /// </summary>
    LISTINGCOMMAND,
}
