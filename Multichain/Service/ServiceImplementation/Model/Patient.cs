using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string BsnNumber { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public string Streetname { get; set; }
        [DataMember]
        public string Housenumber { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public DateTime DateLastMutation { get; set; }
        [DataMember]
        public ContentItems[] ItemsList { get; set; }
    }
}