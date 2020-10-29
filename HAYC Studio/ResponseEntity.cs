using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAYC_Studio
{
    public class LoginResponseEntity
    {
        public List<string> datalist { get; set; }
        public string msg { get; set; }
        public string page { get; set; }
        public string resultCode { get; set; }
        public DataItem data { get; set; }

        public class DataItem
        {
            public string loginKey { get; set; }
            public string loginValue { get; set; }
            public UserVo userVo { get; set; }

            public class UserVo
            {
                public string createBy { get; set; }
                public string createDate { get; set; }
                public string deleteFlag { get; set; }
                public string departmentId { get; set; }
                public string departmentName { get; set; }
                public string email { get; set; }
                public string loginName { get; set; }
                public string mobile { get; set; }
                public string sexCode { get; set; }
                public string updateBy { get; set; }
                public string updateDate { get; set; }
                public string userLoginFlag { get; set; }
                public string userName { get; set; }
                public string userPicId { get; set; }
                public string userState { get; set; }
                public string userType { get; set; }
                public string uuid { get; set; }
            }
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static LoginResponseEntity fromJson(string json)
        {
            return JsonConvert.DeserializeObject<LoginResponseEntity>(json);
        }
    }

    public class UserInfoResposeEntity
    {
        public List<DataItem> datalist { get; set; }
        public string msg { get; set; }
        public string page { get; set; }
        public string resultCode { get; set; }
        public string data { get; set; }



        public class DataItem
        {
            public DataItem()
            {
                sceneInfo = new List<SceneItem>();
            }
            public string sceneName { get; set; }
            public List<SceneItem> sceneInfo { get; set; }
            public class SceneItem
            {
                public string browserTypeCode { get; set; }
                public string browserTypeName { get; set; }
                public string createBy { get; set; }
                public long? createDate { get; set; }
                public int displayIndex { get; set; }
                public string roleId { get; set; }
                public string roleName { get; set; }
                public string sceneName { get; set; }
                public string screenName { get; set; }
                public string screenNames { get; set; }
                public string screenRatio { get; set; }
                public string screenUrl { get; set; }
                public string uuid { get; set; }
            }
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static UserInfoResposeEntity fromJson(string json)
        {
            return JsonConvert.DeserializeObject<UserInfoResposeEntity>(json);
        }
    }
}
