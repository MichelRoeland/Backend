using ServiceImplementation.Model;

namespace ServiceImplementation
{
    public class BlockChainApi : IBlockChainApi
    {
        public string AddDataToAddress(AddDataRequest request)
        {
            return "Ok";
        }
    }
}
