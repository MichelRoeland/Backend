using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BlockchainTest;

namespace TestApplication
{
    public class Program
    {
        public static FirstClient FirstClient;
        public static SecondClient SecondClient;

        public static string streamname;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
