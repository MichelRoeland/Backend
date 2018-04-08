using System;
using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class ContentItems
    {
        [DataMember]
        public string ContentId { get; set; }
        //[DataMember]
        // public DateTime DateTimeMutation { get; set; }
        [DataMember]
        public int DataBlockCount { get; set; }
    }
}