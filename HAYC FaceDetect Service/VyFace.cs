using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_FaceDetect_Service
{
    public class VyFaceDLL
    {
        [DllImport("E:\\FaceSDK_CSharp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "?face_sdk_init@@YAHPBD@Z")]
        public static extern int face_sdk_init(string filePath);

        [DllImport("E:\\FaceSDK_CSharp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "?face_sdk_extract@@YAHPBDPAD@Z")]
        public static extern int face_sdk_extract(string imagePath, ref byte featcode);
        //public static extern int face_sdk_extract(string imagePath, [MarshalAs(UnmanagedType.LPStr)]StringBuilder featcode);

        [DllImport("E:\\FaceSDK_CSharp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "?face_sdk_compare@@YAMPAD0H@Z")]
        public static extern float face_sdk_compare(ref byte f1, ref byte f2, int codelen);


        public enum FLAG_EXTRACT
        {
            FLAG_EXTRACT_WITH_DETECTION = 1,
            FLAG_EXTRACT_WITHOUT_DETECTION = 2
        };
        public enum FLAG_CALL_METHOD
        {
            FLAG_CALL_FROM_FILE = 1,
            FLAG_CALL_FROM_MEM = 2,
            FLAG_CALL_FROM_SUPPRSION_MEM = 3
        };
        public enum FLAG_DETECT_METHOD
        {
            FLAG_DETECT_DL = 1,
            FLAG_DETECT_OV = 2,
            FLAG_DETECT_DL_SIM = 3

        };
        public enum SDK_INIT_METHOD
        {
            SDK_INIT_SIMPLE_SSD = 1,
            SDK_INIT_SIMPLE_MTCNN = 2,
            SDK_INIT_COMPOSITE_SSD = 3,
            SDK_INIT_COMPOSITE_MTCNN = 4
        };

        public struct VYFeature
        {
            int _nFeatureSize;
            string _pFeatureData;
        }
        public struct VYImage
        {
            int flag;
            int _nWidth;
            int _nHeight;
            int _nDataSize;
            string _pData;
        }
        public struct VYRECT
        {
            float _x;
            float _y;
            float _width;
            float _height;

        }
        public struct VYCONFRECT
        {
            VYRECT _rc;
            float _confidence;
            float _rot;
        }
        public struct VYPOINT
        {
            int x;
            int y;
        }
        public struct VYPOINTF
        {
            float x;
            float y;
        }
        public struct VYLANDMARKSF
        {
            VYPOINTF le;
            VYPOINTF re;
            VYPOINTF nose;
            VYPOINTF lm;
            VYPOINTF rm;
        }
        public struct VYFACELAND
        {
            VYCONFRECT face;
            VYLANDMARKSF landmarks;

        }
        public struct VYRECTGENDER
        {
            VYRECT _rc;
            float _confidence;
        }

        public struct VYFACEPOSE
        {
            VYFACELAND _rc;
            float roll;
            float yaw;
            float pitch;

        }

        public struct VYLANDMARKS
        {
            VYPOINT le;
            VYPOINT re;
            VYPOINT nose;
            VYPOINT lm;
            VYPOINT rm;
        }

        public struct VYFACES
        {
            VYFACEPOSE _rc;
            float _gender;

        }

        public struct BBOX
        {
            int x0;
            int y0;
            int x1;
            int y1;
            float score;
            float reg0;
            float reg1;
            float reg2;
            float reg3;
        }
        public struct FBBOX
        {
            float x0;
            float y0;
            float x1;
            float y1;
            float score;
        }
        public struct PAD_PARAMS
        {
            int dy;
            int edy;
            int dx;
            int edx;
            int y;
            int ey;
            int x;
            int ex;
            int tmpw;
            int tmph;
        }
        public struct cutface_params
        {
            int ec_mc_y;             //在截取后的照片中,两眼中点与嘴中点的距离，用于求缩放比例
            int ec_y;                //在截取后的照片中,两眼中点与上边界的距离
            int crop_face_size_x;     //最终截取的人脸大小
            int crop_face_size_y;     //最终截取的人脸大小
        }

    }
}
