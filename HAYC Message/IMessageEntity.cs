using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAYC_Message
{
    public interface IMessageEntity
    {
        string toJson();

        IMessageEntity fromJson(string json);
    }

    public abstract class BaseMessageEntity : IMessageEntity
    {
        public Meta meta;
        public Body body;

        public BaseMessageEntity()
        {
            meta = new Meta();
            body = new Body();
        }
        public abstract IMessageEntity fromJson(string json);
        public abstract string toJson();

        public class Meta
        {
            public string name { get; set; }
        }

        public class Body
        {
            public string filed { get; set; }
        }
    }
}
