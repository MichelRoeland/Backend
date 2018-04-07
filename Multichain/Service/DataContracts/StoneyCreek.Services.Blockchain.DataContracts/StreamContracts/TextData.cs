using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneyCreek.Services.Blockchain.DataContracts.StreamContracts
{
    public class TextData
    {
        public DateTime DateTimeLastMutation { get; set; }
        public string Addition { get; set; }
        public string AddressOtherPhysician { get; set; }
    }
}
