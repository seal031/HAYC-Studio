using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public static class qivw
{
    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr QIVWSessionBegin(string grammarList, IntPtr param, ref int result);


    [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int QIVWSessionEnd(string sessionID, string hints);


}
