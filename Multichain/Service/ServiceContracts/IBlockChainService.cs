// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWcfPushRequestService.cs" company="suntech">
//   Copyright 2013 (c) suntech N.V.
// </copyright>
// <summary>
//   Defines the IWcfPushRequestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ServiceModel;
using System.ServiceModel.Channels;

using Stoneycreek.libraries.MultichainWrapper.Structs;

namespace Stoneycreek.Services.Blockchain.ServiceContracts
{
    [XmlSerializerFormat]
    [ServiceContract]
    public interface IBlockChainService
    {
        #region Public Methods and Operators

        //[OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "Register")]
        //RegisterSteckieUserResponse Register(RegisterSteckieUserRequest request);
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "RequestAddress")]
        KeyPair RequestAddress(string xsdrequest);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "*", ReplyAction = "*")]
        Message ExecuteRequest(Message request);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "ExecuteRequestMapData")]
        object ExecuteRequestMapData(string data);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "ExeQueuete")]
        bool ExeQueuete();

        #endregion
    }
}