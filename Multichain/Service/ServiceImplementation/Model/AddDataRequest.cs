using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class AddDataRequest
    {
        [DataMember]
        public string SourceAddress { get; set; }

        [DataMember]
        public string DestinationAddress { get; set; }

        [DataMember]
        public string Data { get; set; }
    }
}
