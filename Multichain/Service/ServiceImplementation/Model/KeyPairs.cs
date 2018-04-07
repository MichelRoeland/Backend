using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Model
{
    public class KeyPairs
    {
        public KeyPair[] keypairs { get; set; }
    }

    public class KeyPair
    {
        public string address { get; set; }
        public string pubkey { get; set; }
        public string privkey { get; set; }
    }
}
