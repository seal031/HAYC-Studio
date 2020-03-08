using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace HAYC_Studio.Message.MessageEntity
{
    public interface IMessageEntity
    {
        string toJson();

        IMessageEntity fromJson(string json);
    }
}
