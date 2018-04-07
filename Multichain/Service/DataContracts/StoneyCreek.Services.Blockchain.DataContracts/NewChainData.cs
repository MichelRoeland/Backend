namespace StoneyCreek.Services.Blockchain.DataContracts
{
    public class NewChainData
    {
        public string ChainName { get; set; }

        public double AdminConsensus { get; set; }

        public double CreateConsensus { get; set; }

        public int FirstBlocks { get; set; }
    }
}
