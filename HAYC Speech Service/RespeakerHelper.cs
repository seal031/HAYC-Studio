using System;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Wave;

/// <summary>
/// 麦克风音量采集.
/// </summary>
public class MicVolumnPicker
{
    /// <summary>
    /// 最后一次音量超过指令结束阈值的时间
    /// </summary>
    public static DateTime lastLoudlyTime;
    /// <summary>
    /// 最后一条指令的识别时间
    /// </summary>
    public static DateTime lastCommandTime = DateTime.Now;

    private static MMDeviceEnumerator enumerator;
    private static MMDevice[] CaptureDevices;
    private static MMDevice selectedDevice;
    private static string voicePickerName;//需要获取的麦克风名字关键字，用于查找特定的麦克风

    /// <summary>
    /// 初始化
    /// </summary>
    public static bool init()
    {
        voicePickerName = ConfigWorker.GetConfigValue("voicePickerName");
        enumerator = new MMDeviceEnumerator();
        CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
        var respeakerIndex = getRespeakerIndexByDevice();
        if (CaptureDevices.Length > 0)
        {
            selectedDevice = CaptureDevices[respeakerIndex];
            return true;
        }
        else
        {
            LogHelper.WriteLog("当前机器上没有声音输入设备，服务无法启动。");
            return false;
        }
    }

    public static void dispose()
    {
        enumerator.Dispose();
        enumerator = null;
        CaptureDevices = null;
        selectedDevice.Dispose();
        selectedDevice = null;
    }

    private static int getRespeakerIndexByDevice()
    {
        foreach (var device in CaptureDevices)
        {
            LogHelper.WriteLog("找到一个麦克风，名字是"+device.DeviceFriendlyName);
        }
        for (int i = 0; i < CaptureDevices.Length; i++)
        {
            if (CaptureDevices[i].DeviceFriendlyName.ToLower().Contains(voicePickerName))
            {
                return i;
            }
        }
        LogHelper.WriteLog("没有找到适配的麦克风，开始使用系统默认的第一个麦克风");
        //return 0;//todo 为济南测试用，用后删除
        return 0;
    }/// <summary>
     /// 获取系统中respeaker麦克风的索引
     /// </summary>
     /// <returns></returns>
    public static int getRespeakerIndexByWaveInEvent()
    {
        int result = 0;
        for (int i = 0; i < WaveInEvent.DeviceCount; i++)
        {
            var device = WaveInEvent.GetCapabilities(i);
            if (device.ProductName.ToLower().Contains(voicePickerName))
            {
                return i;
            }
        }
        LogHelper.WriteLog("没有找到适配的麦克风，开始使用系统默认的第一个麦克风");
        //return 0;//todo 为济南测试用，用后删除
        return result;
    }

    /// <summary>
    /// 获取麦克风音量
    /// </summary>
    public static float getVolumn()
    {
        var volumn = selectedDevice.AudioMeterInformation.MasterPeakValue;
        //if (volumn > LocalParams.VolumnCommandThreshold)//如果听到大声说话
        //{
        //    lastLoudlyTime = DateTime.Now;//更新上次大声说话时间
        //}
        return volumn;
    }
}


