using ServiceImplementation.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceImplementation
{
    [ServiceContract]
    public interface IBlockChainApi
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/AddDataToAddress", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string AddDataToAddress(AddDataRequest request);
    }
}