using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper.Structs
{
    public class Streams
    {
        public Stream[] streams;
    }

    public class Stream
    {
        public string name { get; set; }
        public string createtxid { get; set; }
        public bool open { get; set; }
        public object details { get; set; }
        public bool subscribed { get; set; }
        public bool synchronized { get; set; }
        public int items { get; set; }
        public int confirmed { get; set; }
        public int keys { get; set; }
        public int publishers { get; set; }
        public string streamref { get; set; }
    }
}
