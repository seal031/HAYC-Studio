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

    /// <summary>
    /// 初始化
    /// </summary>
    public static void init()
    {
        enumerator = new MMDeviceEnumerator();
        CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
        var respeakerIndex = getRespeakerIndexByDevice();
        selectedDevice = CaptureDevices[respeakerIndex];
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
            if (CaptureDevices[i].DeviceFriendlyName.ToLower().Contains("respeaker"))
            {
                return i;
            }
        }
        LogHelper.WriteLog("没有找到适配的麦克风");
        return -1;
    }/// <summary>
     /// 获取系统中respeaker麦克风的索引
     /// </summary>
     /// <returns></returns>
    public static int getRespeakerIndexByWaveInEvent()
    {
        int result = -1;
        for (int i = 0; i < WaveInEvent.DeviceCount; i++)
        {
            var device = WaveInEvent.GetCapabilities(i);
            if (device.ProductName.ToLower().Contains("respeaker"))
            {
                return i;
            }
        }
        LogHelper.WriteLog("没有找到适配的麦克风");
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


