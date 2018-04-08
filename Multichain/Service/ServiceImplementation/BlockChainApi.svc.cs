using ServiceImplementation.Model;
using Stoneycreek.libraries.MultichainWrapper;
using System.Collections.Generic;

namespace ServiceImplementation
{
    public class BlockChainApi : IBlockChainApi
    {
        public string SignPhysician(SignRequest request)
        {
            PatientChain patientChain = new PatientChain();
            var Signature = patientChain.SignMessage(request.HashPrivateKey, request.PhysicianId);

            return "{ \"Signature\" : \"" + Signature + "\" }";
        }

        public PatientsResponse GetAllPatients()
        {
            var myPatients = new List<Patient>();

            PatientChain patientChain = new PatientChain();
            var patients = patientChain.GetPatients();

            foreach(var patient in patients)
            {
                var p = new Patient
                {
                    Address = patient.Address,
                    BsnNumber = patient.BsnNumber,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Initials = patient.Initials,
                    Streetname = patient.Streetname,
                    Housenumber = patient.Housenumber,
                    City = patient.City,
                    Country = patient.Country,
                    DateLastMutation = patient.DateLastMutation,
                };

                foreach (var item in patient.ItemsList)
                {
                    if(p.ItemsList == null)
                        p.ItemsList = new List<ContentItems>();

                    p.ItemsList.Add(new ContentItems
                    {
                        ContentId = item.PhysicianIdentification,
                        DateTimeMutation = item.DateTimeMutation,
                        DataBlockCount = item.DataBlocks
                    });
                }

                myPatients.Add(p);
            }

            return new PatientsResponse { Patients = myPatients };
        }

        public string LinkPhysicianToPatient(LinkingPhysicianRequest request)
        {
            PatientChain patientChain = new PatientChain();
            var reply = patientChain.AddPhysician(request.Address, request.PatientId, request.BsnNumber, request.SignMessage);

            return reply ? "{ \"Result\": \"Ok\" }": "{ \"Result\": \"Failure\" }";
        }

        public ContentData GetContentData(DataItem request)
        {
            var contentData = new ContentData();

            PatientChain patientChain = new PatientChain();
            var response = patientChain.GetChainData(new PatientChain.ParameterClass
            {
                Streamname = request.StreamName,
                PhysicianId = request.ContentId,
                Signature = request.Signature,
                Address = request.Address,
                PatientId = request.PatientId
            });

            foreach(var item in response)
            {
                var transaction = new Transaction
                {
                    //TransactionId = 
                    Data = item
                };

                contentData.Content.Add(transaction);
            }

            return contentData;
        }

        public string SetContentData(DataItemToStore request)
        {
            bool reply;

            var contentData = new ContentData();

            PatientChain patientChain = new PatientChain();
            //var reply =
            patientChain.SetChainData(new PatientChain.ParameterClass
            {
                Streamname = request.StreamName,
                PhysicianId = request.ContentId,
                DataToStore = request.DataToStore,
                Signature = request.Signature,
                Address = request.Address,
                PatientId = request.PatientId
            });

            reply = true;

            return reply ? "{ \"Result\": \"Ok\" }" : "{ \"Result\": \"Failure\" }";
        }
    }
}
