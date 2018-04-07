using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class SignRequest
    {
        [DataMember]
        public string HashPrivateKey { get; set; }

        [DataMember]
        public string PhysicianId { get; set; }
    }
}