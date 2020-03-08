using Newtonsoft.Json;

namespace HAYC_Message
{
    public class TestMessage : BaseMessageEntity
    {
        public override IMessageEntity fromJson(string json)
        {
            return JsonConvert.DeserializeObject<TestMessage>(json);
        }

        public override string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public new class Meta
        {
            public string sender { get; set; }
        }
        public new class Body
        {
            public string equ { get; set; }
        }
    }
}
