using System.Threading;

namespace Stoneycreek.Libraries.Blockchain.SignallR
{
    public class StateObjClass
    {
        // Used to hold parameters for calls to TimerTask. 
        public int SomeValue;
        public Timer TimerReference;
        public bool TimerCanceled;
    }
}
