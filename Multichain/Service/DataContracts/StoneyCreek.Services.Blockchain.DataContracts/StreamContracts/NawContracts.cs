using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneyCreek.Services.Blockchain.DataContracts.StreamContracts
{
    [Serializable]
    public class NawContracts
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

        public List<Items> ItemsList { get; set; }
    }

    public class Items
    {
        public string PhysicianIdentification { get; set; }
        public DateTime DateTimeMutation { get; set; }
        public int DataBlocks { get; set; }

    }
}
