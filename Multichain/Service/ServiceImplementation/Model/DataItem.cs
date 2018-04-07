using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class DataItem
    {
        [DataMember]
        public string StreamName { get; set; }

        [DataMember]
        public string ContentId { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PatientId { get; set; }
    }
}