using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class ContentData
    {
        [DataMember]
        public List<Transaction> Content { get; set; }
    }
}