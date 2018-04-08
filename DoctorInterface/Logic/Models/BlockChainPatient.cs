using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class BlockChainPatient
    {
        public string Initials { get; set; }
        public string Lastname { get; set; }
        public string BsnNumber { get; set; }
        public string Address { get; set; }
        public string Streetname { get; set; }
        public string Housenumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public BlockChainApi.ContentItems[] ItemList { get; set; }
    }
}
