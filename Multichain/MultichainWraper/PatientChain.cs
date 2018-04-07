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
        private const string items = "items";
        private const string access = "access";

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

            chain.CreateNewStream(true, patientAddress + "-crossstreamcomm");
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

  
            // Create stream client

            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, physicianAddress) == "true";
            if (verified)
            {

                var itemStreamname = this.GetChainName(patientAddress, physicianAddress, items);
                var accessStreamname = this.GetChainName(patientAddress, physicianAddress, access);

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

                var transactionkey = this.GetTransactionKey(physicianAddress);
                var transactionId = chain.PublishMessage(transactionkey, data, authorisation);

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

                var transactionkey = this.GetTransactionKey(physicianAddress);
                var transactionId = chain.PublishMessage(transactionkey, data, authorisation);

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

                var transactionkey = this.GetTransactionKey(physicianAddress);
                var transactionId = chain.PublishMessage(transactionkey, data, authorisation);
            }
            
            return false;
        }

        public bool CrossStreamCommunicationPost(string patientAddress, string address1, string address2, string messageToPost, string signature, string transactionId = null)
        {
            // Als wij van chain naar chain willen communiceren, dan moeten beide of alle partijen, inzage hebben in alle chains
            // Aangezien dit niet wenselijk is, kunnen wij ook de transaction Id's opslaan.
            // Stel voor:
            //              doc A post een vraag -> insert question in stream van doc A
            //              doc B mag niet lezen in stream van doc A
            //              vraag van doc A is gepost in stream van doc A (eigen stream, geen cross stream posts)
            //              doc A -> krijgt transaction Id van vraag -> wordt opgeslagen in streamPatientAddress-crossstreamcomm
            //              doc B krijgt een melding dat er een 'crossstream' communicatie klaar staat 
            //              doc B mag vanwege tijdelijke autorisatie op enkel block TxId data lezen in stream van doc A
            //              doc B geeft antwoord in eigen stream (geen cross stream posts)
            //              doc B krijgt TxId terug van de post -> dit TxId wordt gepost in streamPatientAddress-crossstreamcomm onder de TxId van de vraag (Key is zelfde TxId van vraag)
            //              doc A en doc B kunnen historische vraag en antwoord -> van alle posts gekoppeld aan TxId van vraag teruglezen
            //              wanneer doc A verdere vraag uitzet naar doc C wordt ook die gekoppeld onder zelfde TxId -> doc B kan ook resultaat van doc C lezen

            // Address1 = addres from -> posts question
            // Address2 = address to -> receives question
            // patientAddress = adres van patient -> his steams need to be used

            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, messageToPost) == "true";
            if (verified)
            {
                var crossStreamName = patientAddress + "-crossstreamcomm";
                var senderStreamName = this.GetChainName(patientAddress, address1, items);

                var transactionkey = this.GetTransactionKey(address1);

                var hexdata = this.EncryptHexData(messageToPost);
                var TxId = chain.PublishMessage(transactionkey, hexdata, senderStreamName);

                var hexTxId = this.EncryptHexData(TxId + "|" + address1);
                var TxIdCross = chain.PublishMessage(transactionId ?? TxId, hexTxId, crossStreamName);
            }

            return false;
        }

        public string[] CrossStreamCommunicationRead(string patientAddress, string transactionId, string signature)
        {
            // Als wij van chain naar chain willen communiceren, dan moeten beide of alle partijen, inzage hebben in alle chains
            // Aangezien dit niet wenselijk is, kunnen wij ook de transaction Id's opslaan.
            // Stel voor:
            //              doc A post een vraag -> insert question in stream van doc A
            //              doc B mag niet lezen in stream van doc A
            //              vraag van doc A is gepost in stream van doc A (eigen stream, geen cross stream posts)
            //              doc A -> krijgt transaction Id van vraag -> wordt opgeslagen in streamPatientAddress-crossstreamcomm
            //              doc B krijgt een melding dat er een 'crossstream' communicatie klaar staat 
            //              doc B mag vanwege tijdelijke autorisatie op enkel block TxId data lezen in stream van doc A
            //              doc B geeft antwoord in eigen stream (geen cross stream posts)
            //              doc B krijgt TxId terug van de post -> dit TxId wordt gepost in streamPatientAddress-crossstreamcomm onder de TxId van de vraag (Key is zelfde TxId van vraag)
            //              doc A en doc B kunnen historische vraag en antwoord -> van alle posts gekoppeld aan TxId van vraag teruglezen
            //              wanneer doc A verdere vraag uitzet naar doc C wordt ook die gekoppeld onder zelfde TxId -> doc B kan ook resultaat van doc C lezen

            // Address1 = addres from -> posts question
            // Address2 = address to -> receives question
            // patientAddress = adres van patient -> his steams need to be used

            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(patientAddress, signature, transactionId) == "true";
            if (verified)
            {
                List<string> dataitems = new List<string>();

                var crossStreamName = patientAddress + "-crossstreamcomm";
                var result = chain.GetStreamItemByKey(crossStreamName, transactionId);

                var data = new List<string>();
                data.AddRange(result.streamitems.Select(f => this.DeEncryptHexData(f.data)).ToArray());

                foreach(var i in data)
                {
                    var TxId = i.Split('|')[0];
                    var address = i.Split('|')[1];

                    var chainname = this.GetChainName(patientAddress, address, items);
                    var chaindata = chain.GetStreamItemByKey(chainname, TxId);

                    var originaldata = chaindata.streamitems.Select(f => this.DeEncryptHexData(f.data)).ToArray();
                    dataitems.AddRange(originaldata);
                }

                return dataitems.ToArray();
            }

            return new string[0];
        }

        private string GetChainName(string address1, string address2, string type)
        {
            return string.Format(StreamName, type, address1, address2);
        }

        private string GetTransactionKey(string address1)
        {
            string key = "{0}_{1}";
            return string.Format(StreamName, DateTime.Now.ToString().Replace("-", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty));
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
