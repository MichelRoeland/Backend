using System;
using System.Collections.Generic;

namespace ServiceImplementation.Model
{
    public class Patient
    {
        public string Address { get; set; }

        public string BsnNumber { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Initials { get; set; }

        public string Streetname { get; set; }

        public string Housenumber { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime DateLastMutation { get; set; }

        public List<ContentItems> ItemsList { get; set; }
    }
}