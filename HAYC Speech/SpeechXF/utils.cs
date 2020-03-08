using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class utils
{
    public static void Inc(ref IntPtr p, int val)
    {
        p = (IntPtr)(((int)p) + val);
    }

    public static byte[] ReadFile(string fileName)
    {
        FileStream pFileStream = null;
        byte[] pReadByte = new byte[0];
        try
        {
            pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(pFileStream);
            r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
            pReadByte = r.ReadBytes((int)r.BaseStream.Length);
            return pReadByte;
        }
        catch
        {
            return pReadByte;
        }
        finally
        {
            if (pFileStream != null)
                pFileStream.Close();
        }
    }

}
