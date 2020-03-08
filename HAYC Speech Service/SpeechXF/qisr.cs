using System;
using System.Runtime.InteropServices;

public static class qisr
{
    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr QISRSessionBegin(string grammarList, IntPtr param, ref int result);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QISRAudioWrite(string sessionID, IntPtr waveData, uint waveLen, int audioStatus, ref int epStatus, ref int recogStatus);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr QISRGetResult(string sessionID, ref int rsltStatus, int waitTime, ref int errorCode);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern string QISRGetBinaryResult(string sessionID, ref uint rsltLen, ref int rsltStatus, int waitTime, ref int errorCode);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QISRSessionEnd(string sessionID, string hints);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QISRGetParam(string sessionID, string paramName, ref string paramValue, ref uint valueLen);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QISRSetParam(string sessionID, string paramName, string paramValue);

    public delegate void recog_result_ntf_handler(string sessionID, string result, int resultLen, int resultStatus, IntPtr userData);
    public delegate void recog_status_ntf_handler(string sessionID, int type, int status, int param1, IntPtr param2, IntPtr userData);
    public delegate void recog_error_ntf_handler(string sessionID, int errorCode, string detail, IntPtr userData);

    public delegate int UserCallBack(int NamelessParameter1, IntPtr NamelessParameter2, IntPtr NamelessParameter3);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GrammarCallBack(int NamelessParameter1, string NamelessParameter2, IntPtr NamelessParameter3);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LexiconCallBack(int NamelessParameter1, string NamelessParameter2, IntPtr NamelessParameter3);

    [DllImport("msc.dll")]
    public static extern int QISRBuildGrammar(IntPtr grammarType, IntPtr grammarContent, uint grammarLength, IntPtr param, GrammarCallBack callback, IntPtr userData);

    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QISRUpdateLexicon(string lexiconName, string lexiconContent, uint lexiconLength, string param, LexiconCallBack callback, IntPtr userData);
}