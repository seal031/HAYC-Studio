using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_ProcessCommunicate_Library
{
    [Serializable]
    public class UserFaceInfo
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string faceCode { get; set; }
    }
}
