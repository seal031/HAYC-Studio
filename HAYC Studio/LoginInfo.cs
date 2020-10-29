using HAYC_Studio.Browser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Studio
{
    public class LoginInfo
    {
        private List<SceneInfo> _sceneList = new List<SceneInfo>();
        private UserInfo _user = new UserInfo();

        public List<SceneInfo> SceneList
        {
            get
            {
                return _sceneList;
            }

            set
            {
                _sceneList = value;
            }
        }

        public UserInfo User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }
    }

    public class UserInfo
    {
        private string _userRealName = string.Empty;
        private string _userAccount = string.Empty;
        private string _userAddress = string.Empty;
        private string _userIdCard = string.Empty;
        private string _userPhone = string.Empty;
        private string _userEmail = string.Empty;
        private string _userWxCode = string.Empty;
        private string _userDepName = string.Empty;
        private string _userToken = string.Empty;
        
        public string UserRealName
        {
            get
            {
                return _userRealName;
            }

            set
            {
                _userRealName = value;
            }
        }

        public string UserAccount
        {
            get
            {
                return _userAccount;
            }

            set
            {
                _userAccount = value;
            }
        }

        public string UserAddress
        {
            get
            {
                return _userAddress;
            }

            set
            {
                _userAddress = value;
            }
        }

        public string UserIdCard
        {
            get
            {
                return _userIdCard;
            }

            set
            {
                _userIdCard = value;
            }
        }

        public string UserPhone
        {
            get
            {
                return _userPhone;
            }

            set
            {
                _userPhone = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return _userEmail;
            }

            set
            {
                _userEmail = value;
            }
        }

        public string UserWxCode
        {
            get
            {
                return _userWxCode;
            }

            set
            {
                _userWxCode = value;
            }
        }

        public string UserDepName
        {
            get
            {
                return _userDepName;
            }

            set
            {
                _userDepName = value;
            }
        }

        public string UserToken
        {
            get
            {
                return _userToken;
            }

            set
            {
                _userToken = value;
            }
        }
    }

    public class SceneInfo
    {
        private string _sceneName = string.Empty;
        private List<ScreenInfo> _screenList = new List<ScreenInfo>();

        public string SceneName
        {
            get { return _sceneName; }
            set { _sceneName = value; }
        }
        public List<ScreenInfo> ScreenList
        {
            get
            {
                return _screenList;
            }

            set
            {
                _screenList = value;
            }
        }
    }

    public class ScreenInfo
    {
        private string modelName = string.Empty;
        private int modelIndex = -1;
        private string modelURL = string.Empty;
        private int _left = 0;
        private int _top = 0;
        private System.Drawing.Size _size = new System.Drawing.Size();
        public BrowserType browserType { get; set; }
        public double ratio { get; set; } = 0;//默认0

        public string ModelName
        {
            get
            {
                return modelName;
            }

            set
            {
                modelName = value;
            }
        }

        public int ModelIndex
        {
            get
            {
                return modelIndex;
            }

            set
            {
                modelIndex = value;
            }
        }

        public string ModelURL
        {
            get
            {
                return modelURL;
            }

            set
            {
                modelURL = value;
            }
        }

        public int Left
        {
            get
            {
                return _left;
            }

            set
            {
                _left = value;
            }
        }

        public int Top
        {
            get
            {
                return _top;
            }

            set
            {
                _top = value;
            }
        }

        public Size Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

    }
}
