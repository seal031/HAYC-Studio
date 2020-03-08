using System;
using System.Runtime.InteropServices;

public static class msp_cmn
{
    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int MSPLogin(string usr, string pwd, string param);
    [DllImport("msc.dll")]
    public static extern int MSPLogout();
    [DllImport("msc.dll")]
    public static extern int MSPUpload(string dataName, string param, string dataID);
	public delegate int DownloadStatusCB(int errorCode, int param1, IntPtr param2, IntPtr userData);
	public delegate int DownloadResultCB(IntPtr data, int dataLen, IntPtr userData);
    [DllImport("msc.dll")]
    public static extern int MSPDownload(string dataName, string param, DownloadStatusCB statusCb, DownloadResultCB resultCb, IntPtr userData);
    [DllImport("msc.dll")]
    public static extern int MSPAppendData(IntPtr data, uint dataLen, uint dataStatus);
    [DllImport("msc.dll")]
    public static extern string MSPGetResult(ref uint rsltLen, ref int rsltStatus, ref int errorCode);
    [DllImport("msc.dll")]
    public static extern int MSPSetParam(string paramName, string paramValue);
    [DllImport("msc.dll")]
    public static extern int MSPGetParam(string paramName, ref string paramValue, ref uint valueLen);
    [DllImport("msc.dll")]
    public static extern string MSPUploadData(string dataName, IntPtr data, uint dataLen, string param, ref int errorCode);
    [DllImport("msc.dll")]
    public static extern IntPtr MSPDownloadData(string param, ref uint dataLen, ref int errorCode);
    [DllImport("msc.dll")]
    public static extern IntPtr MSPDownloadDataW(char param, ref uint dataLen, ref int errorCode);
    [DllImport("msc.dll")]
    public static extern string MSPSearch(string param, string text, ref uint dataLen, ref int errorCode);

    public delegate int NLPSearchCB(string sessionID, int errorCode, int status, IntPtr result, int rsltLen, IntPtr userData);
    [DllImport("msc.dll")]
    public static extern string MSPNlpSearch(string param, string text, uint textLen, ref int errorCode, NLPSearchCB callback, IntPtr userData);
	public delegate void msp_status_ntf_handler(int type, int status, int param1, IntPtr param2, IntPtr userData);
    [DllImport("msc.dll")]
    public static extern string MSPRegisterNotify(msp_status_ntf_handler statusCb, IntPtr userData);
    [DllImport("msc.dll")]
    public static extern string MSPGetVersion(string verName, ref int errorCode);

}
