using HAYC_ProcessCommunicate_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public class FaceScoreHelper
    {
        private static int currentFaildTimes = 0;//当前连续低分次数
        private static int faildTimes = 10;//连续N次低分后判定为失败
        private static double Standard = 0.65;//得分判定标准

        /// <summary>
        /// 判断人脸得分
        /// </summary>
        /// <param name="score"></param>
        /// <returns>1成功；2失败；3继续</returns>
        public static int checkScore(double score)
        {
            if (score >= Standard)
            {
                currentFaildTimes = 0;//重置
                return 1;
            }
            else
            {
                if (currentFaildTimes >= faildTimes)
                {
                    currentFaildTimes = 0;//重置
                    return 2;
                }
                else
                {
                    currentFaildTimes += 1;
                    return 3;
                }
            }
        }
        /// <summary>
        /// 序列化用户和特征码关联关系
        /// </summary>
        /// <param name="userFaceInfoList"></param>
        public static void faceInfoSerialize(List<UserFaceInfo> userFaceInfoList)
        {
            using (FileStream fs = new FileStream("DataFile.faceinfo", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (UserFaceInfo ufi in userFaceInfoList)
                {
                    formatter.Serialize(fs, ufi);
                }
            }
        }
        /// <summary>
        /// 反序列化用户和特征码关联关系
        /// </summary>
        /// <returns></returns>
        public static List<UserFaceInfo> faceInfoDeserialize()
        {
            List<UserFaceInfo> list = new List<UserFaceInfo>();
            using (FileStream fs = new FileStream("DataFile.faceinfo", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    UserFaceInfo ufi = (UserFaceInfo)formatter.Deserialize(fs);
                    list.Add(ufi);
                }
                catch (Exception)
                {
                    
                }
            }
            return list;
        }
    }

    
}
