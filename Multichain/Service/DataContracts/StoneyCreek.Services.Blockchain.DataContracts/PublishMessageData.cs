namespace StoneyCreek.Services.Blockchain.DataContracts
{
    public class PublishMessageData
    {
        public string Key { get; set; }

        public string FromAddress { get; set; }

        public string HexString { get; set; }

        public string StreamName { get; set; }
    }
}
