using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct LocalParams
{
    /// <summary>
    /// 麦克风音量休眠阈值（用于判断用户是否说完一句话）.
    /// </summary>
    public static float VolumnSleepThreshold = 0.05f;

    /// <summary>
    /// 麦克风音量指令结束输入阈值（用于判断用户是否开始说一句话）.
    /// </summary>
    public static float VolumnCommandThreshold = 0.18f;

    /// <summary>
    /// 采集声音data的毫秒间隔（即多久触发一次DataAvailable事件），SDK默认100，本程序默认25
    /// </summary>
    public static int BufferMilliseconds = 25;

    /// <summary>
    /// 输入声音变小后，继续采集DataAvailable事件的次数。用于解决声音尾音采集不全的问题，默认5
    /// </summary>
    public static int EndPickAdditional = 5;

    /// <summary>
    /// 距离上次识别出命令多少秒后进入休眠，默认30
    /// </summary>
    public static int MicVolumnPickerSleepSecond = 300;

    /// <summary>
    /// 当连续说出几次未识别命令时，转入休眠状态，默认3
    /// </summary>
    public static int UnKownTimes = 3;
}
