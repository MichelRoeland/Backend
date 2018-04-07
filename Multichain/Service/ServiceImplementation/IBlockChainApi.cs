using ServiceImplementation.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceImplementation
{
    [ServiceContract]
    public interface IBlockChainApi
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/SignPhysician", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string SignPhysician(SignRequest request);

        [OperationContract]
        [WebGet(UriTemplate = "/AllPatients", ResponseFormat = WebMessageFormat.Json)]
        PatientsResponse GetAllPatients();

        [OperationContract]
        [WebInvoke(UriTemplate = "/LinkPhysicianToPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string LinkPhysicianToPatient(LinkingPhysicianRequest request);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetContentData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        ContentData GetContentData(DataItem request);

        [OperationContract]
        [WebInvoke(UriTemplate = "/SetContentData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string SetContentData(DataItemToStore request);
    }
}