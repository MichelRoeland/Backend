using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper.Structs
{
    public class StreamKey
    {

        public StreamKeys[] streamkeys { get; set; }

    }

    public class StreamKeys
    {
        public string key { get; set; }
        public int items { get; set; }
        public int confirmed { get; set; }

    }
}
