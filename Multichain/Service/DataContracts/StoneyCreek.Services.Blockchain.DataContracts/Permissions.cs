using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper.Structs
{
    public class Permissions
    {
        public Permission[] permissions { get; set; }
    }
    public class Permission
    {
        public string address { get; set; }

        public string @for { get; set; }
        public string @type { get; set; }
        public string startblock { get; set; }
        public string endblock { get; set; }

    }
}
