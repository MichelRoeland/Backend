using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.Services.Blockchain.SignalRService.BlockCommunication
{
    public class SendMessage
    {
        public enum TransactionType
        {
            Coin,
            Message,
            Object,
            
        }

        public string Signature { get; set; }
        public bool IsPrivate { get; set; }
        public string TransactionId { get; set; }
        
        public TimeSpan SendTime { get; set; }
        public DateTime SendDate { get; set; }

    }
}
