using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class LinkingPhysicianRequest
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PatientId { get; set; }

        [DataMember]
        public string BsnNumber { get; set; }

        [DataMember]
        public string SignMessage { get; set; }
    }
}