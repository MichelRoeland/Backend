using Stoneycreek.libraries.MultichainWrapper.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class PatientChain
    {
        private const string authorisation = "authorisation";
        private const string encryption = "encryption";

        private string StreamName = "{0}-{1}-{2}";
        
        public string SignMessage(string privateKey, string physicianAddress)
        {
            MultiChain chain = new MultiChain();
            var signature = chain.SignMessage(privateKey, physicianAddress);
            return signature;
        }
        public bool CreateNewPatientChain(string patientAddress)
        {
            // Create serveral Objects:
            //      Patient stream -> all physician addresses separated by ;

            // autorisatie stream wordt gevuld met de autorisatie van de medicus ten opzichte van andere streams anders dan zijn/haar eigen
            // de streams waarop hij/zij toegang heeft zijn abonnementen
            // de abonnementen kunnen worden ingenomen/ ongedaan gemaakt worden

            // de stappen
            // Create stream client
            MultiChain chain = new MultiChain();
            chain.CreateNewStream(true, patientAddress);
            chain.Subscribe(patientAddress);
            
            return true;
        }

        public bool AddPhysician(string patientAddress, string physicianAddress, string signature)
        {
            // Create serveral Objects:
            //      Authorisation stream
            //      Items stream

            // autorisatie stream wordt gevuld met de autorisatie van de medicus ten opzichte van andere streams anders dan zijn/haar eigen
            // de streams waarop hij/zij toegang heeft zijn abonnementen
            // de abonnementen kunnen worden ingenomen/ ongedaan gemaakt worden

            // de stappen
            // Create stream client

            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, physicianAddress) == "true";
            if (verified)
            {

                var itemStreamname = this.GetChainName(patientAddress, physicianAddress, "item");
                var accessStreamname = this.GetChainName(patientAddress, physicianAddress, "access");

                chain.CreateNewStream(true, patientAddress);
                chain.CreateNewStream(true, accessStreamname);

                chain.Subscribe(patientAddress);
                chain.Subscribe(accessStreamname);

                // toevoegen authorisatie arts
                var result = chain.GetStreamItemByKey(patientAddress, authorisation);
                StreamItem streamitem = new StreamItem();

                if (result.streamitems.Any())
                {
                    streamitem = result.streamitems.Last();
                }

                var data = streamitem.data.Any() ? this.DeEncryptHexData(streamitem.data) : string.Empty;
                data += this.EncryptHexData((data.Any() ? ";" : string.Empty) + physicianAddress);
                var transactionId = chain.PublishMessage(authorisation, data);

                return true;
            }

            return false;
        }

        public bool AddPhysicianRights(string patientAddress, string physicianAddress, string physicianToAdd, string signature)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, physicianAddress) == "true";
            if (verified)
            {
                var result = chain.GetStreamItemByKey(patientAddress, authorisation);
                StreamItem streamitem = new StreamItem();

                if (result.streamitems.Any())
                {
                    streamitem = result.streamitems.Last();
                }

                var data = streamitem.data.Any() ? this.DeEncryptHexData(streamitem.data) : string.Empty;
                data += this.EncryptHexData((data.Any() ? ";" : string.Empty) + physicianAddress);
                var transactionId = chain.PublishMessage(authorisation, data);

                return true;
            }

            return false;
        }

        public bool RemovePhysicianRights(string patientAddress, string physicianAddress, string physicianToRemove, string signature)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, physicianAddress) == "true";
            if (verified)
            {
                var result = chain.GetStreamItemByKey(patientAddress, authorisation);
                StreamItem streamitem = new StreamItem();

                if (result.streamitems.Any())
                {
                    streamitem = result.streamitems.Last();
                }

                var data = streamitem.data.Any() ? this.DeEncryptHexData(streamitem.data) : string.Empty;
                var res = string.Concat(data.Split(';').Where(f => f != physicianToRemove).Select(f => f + ";").ToArray());
                if (res.Any())
                {
                    data = this.EncryptHexData(res);
                }

                var transactionId = chain.PublishMessage(authorisation, data);
            }
            
            return false;
        }

        // doc a stelt vraag aan doc b -> vraag tx ID = 123123123 
        // doc b geeft antwoord op vraag tx ID 123123123
        // doc a vraagt info -> tx ID 123123123 (chain)

        private string GetChainName(string address1, string address2, string type)
        {
            return string.Format(StreamName, type, address1, address2);
        }

        private string EncryptHexData(string plain)
        {
            byte[] ba = Encoding.Default.GetBytes(plain);
            var hexString = BitConverter.ToString(ba).Replace("-", string.Empty).Replace(" ", string.Empty);
            return hexString;
        }

        private string DeEncryptHexData(string encrypted)
        {
            MultiChain chain = new MultiChain();
            var data = chain.HexStringToBytes(encrypted);
            var data2string = Encoding.Default.GetString(data);
            return data2string;
        }
    }
}
