using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class Utils
{
    public static bool isIp(string ip)
    {
        //如果为空，认为验证不合格
        if (string.IsNullOrEmpty(ip))
        {
            return false;
        }
        //清除要验证字符传中的空格
        ip = ip.Trim();
        //模式字符串，正则表达式
        string patten = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
        //验证
        return Regex.IsMatch(ip, patten);
    }

    public static bool isMac(string mac)
    {
        //如果为空，认为验证不合格
        if (string.IsNullOrEmpty(mac))
        {
            return false;
        }
        //清除要验证字符传中的空格
        mac = mac.Trim();
        //模式字符串，正则表达式
        string patten = @"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$";
        //验证
        return Regex.IsMatch(mac, patten);
    }


    public string GetCPUSerialNumber()
    {
        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Processor");
            string sCPUSerialNumber = "";
            foreach (ManagementObject mo in searcher.Get())
            {
                sCPUSerialNumber = mo["ProcessorId"].ToString().Trim();
            }
            return sCPUSerialNumber;
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
    /// </summary>
    /// <param name="password">待加密的字符串</param>
    /// <param name="bit">位数，一般取值16 或 32</param>
    /// <returns>返回的加密后的字符串</returns>
    public static string MD5Encrypt(string password, int bit)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] hashedDataBytes;
        hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
        StringBuilder tmp = new StringBuilder();
        foreach (byte i in hashedDataBytes)
        {
            tmp.Append(i.ToString("x2"));
        }
        if (bit == 16)
            return tmp.ToString().Substring(8, 16);
        else
        if (bit == 32) return tmp.ToString();//默认情况
        else return string.Empty;
    }
}
