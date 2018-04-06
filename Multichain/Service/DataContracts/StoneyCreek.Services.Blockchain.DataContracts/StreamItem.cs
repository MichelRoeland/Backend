using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper.Structs
{
    public class StreamItems
    {
        public StreamItem[] streamitems { get; set; }
    }

    public class StreamItem
    {
        public string[] publishers { get; set; }
        public string key { get; set; }
        public string data { get; set; }
        public int confirmations { get; set; }
        public int blocktime { get; set; }
        public string txid { get; set; }
    }
}
