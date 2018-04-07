using Stoneycreek.libraries.MultichainWrapper.Structs;
using StoneyCreek.Services.Blockchain.DataContracts;
using StoneyCreek.Services.Blockchain.DataContracts.StreamContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Stoneycreek.libraries.MultichainWrapper.Encryption;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class PatientChain
    {
        public class ParameterClass
        {
            public string Streamname { get; set; }
            public string PhysicianId { get; set; }
            public string DataToStore { get; set; }
            public string Signature { get; set; }

            public string Address { get; set; }
            public string PatientId { get; set; }
        }

        private const string authorisation = "auth";
        private const string encryption = "encryption";
        private const string items = "items";
        private const string access = "access";
        private const string patientnaw = "patientnaw";

        private string StreamName = "{0}-{1}-{2}";

        public string SignMessage(string privateKey, string physicianAddress)
        {
            MultiChain chain = new MultiChain();

            var signature = chain.SignMessage(privateKey, physicianAddress);
            return signature;
        }
        
        public bool CreateNewPatientChain(NawContracts nawcdata)
        {
            // Create serveral Objects:
            //      Patient stream -> all physician addresses separated by ;

            // autorisatie stream wordt gevuld met de autorisatie van de medicus ten opzichte van andere streams anders dan zijn/haar eigen
            // de streams waarop hij/zij toegang heeft zijn abonnementen
            // de abonnementen kunnen worden ingenomen/ ongedaan gemaakt worden

            // de stappen
            // Create stream client

            var patientAddress = nawcdata.BsnNumber;

            MultiChain chain = new MultiChain();
            chain.CreateNewStream(true, patientAddress);

            var xml = new XmlSerializer(typeof(NawContracts));
            var reader = new System.IO.StringWriter();

            xml.Serialize(reader, nawcdata);

            var datalocation = this.CreateFileBackup(reader.ToString());
            chain.PublishMessage(new PublishMessageData { Key = patientnaw, HexString = this.EncryptHexData(datalocation + "|" + ("NogGeenKey").Replace("-", string.Empty).Replace(" ", string.Empty)), StreamName = patientAddress });

            chain.CreateNewStream(true, patientAddress + "-css");
            chain.Subscribe(patientAddress);
            
            return true;
        }

        public string[] GetChainData(ParameterClass data)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(new MessageData { address = data.Address, signature = data.Signature, message = data.PhysicianId }) == "true";
            if (verified)
            {
                var res = chain.GetStreamKeys(data.Streamname);

                var autorisatieStream = this.GetChainName(data.PatientId, data.PhysicianId, authorisation); 
                var result = chain.GetStreamItemByKey(autorisatieStream, authorisation).streamitems.ToArray();

                if (result.Any() && this.DeEncryptHexData(result.Last().data).Split(';').Select(f => data.PhysicianId)
                        .ToArray().Any())
                {
                    var linkarray = new List<string>();
                    foreach (var i in res.streamkeys)
                    {
                        linkarray.AddRange(chain.GetStreamItemByKey(data.Streamname, i.key).streamitems.Select(f => this.DeEncryptHexData(f.data)).ToArray());
                    }

                    var datalist = linkarray.Select(f => File.ReadAllText(f.Split('|')[0])).ToArray();

                    return datalist.ToArray();
                }
            }

            return null;
        }

        public string[] SetChainData(ParameterClass data)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(new MessageData { address = data.Address, signature = data.Signature, message = data.PhysicianId }) == "true";
            if (verified)
            {
                var datalocation = this.CreateFileBackup(data.DataToStore);
                chain.PublishMessage(new PublishMessageData
                {
                    Key = patientnaw,
                    HexString = this.EncryptHexData(datalocation + "|" +
                                                    ("NogGeenKey").Replace("-", string.Empty)
                                                    .Replace(" ", string.Empty)),
                    StreamName = data.Streamname
                });
            }

            return null;
        }

        public NawContracts[] GetPatients()
        {
            MultiChain chain = new MultiChain();
            var patients = chain.ListStreams();

            List<NawContracts> patientsContracts = new List<NawContracts>();

            foreach(var patient in patients.streams)
            {
                chain.Subscribe(patient.name);
                var result = chain.GetStreamItemByKey(patient.name, patientnaw);

                NawContracts deserialized = null;
                if (result != null && result.streamitems.Any())
                {
                    try
                    {
                        var locationAndKey = this.DeEncryptHexData(result.streamitems.Last().data);
                        var filedata = File.ReadAllText(locationAndKey.Split('|')[0]);

                        var xml = new XmlSerializer(typeof(NawContracts));
                        deserialized = (NawContracts)xml.Deserialize(new StringReader(filedata));

                        patientsContracts.Add(deserialized);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                if(patient.name.Contains("-items"))
                {
                    patientsContracts.Last().ItemsList = new List<Items>{new Items { DataBlocks = patient.items, PhysicianIdentification = patient.name }  };
                }
            }

            return patientsContracts.ToArray();
        }

        public bool AddPhysician(string psysicianAddress, string physicianIdentification, string clientBsn, string signature)
        {
            // Create serveral Objects:
            //      Authorisation stream
            //      Items stream

            // autorisatie stream wordt gevuld met de autorisatie van de medicus ten opzichte van andere streams anders dan zijn/haar eigen
            // de streams waarop hij/zij toegang heeft zijn abonnementen
            // de abonnementen kunnen worden ingenomen/ ongedaan gemaakt worden

            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(new MessageData { address = psysicianAddress, signature = signature, message = physicianIdentification }) == "true";
            if (verified)
            {
                var itemStreamname = this.GetChainName(clientBsn, physicianIdentification, items);
                var accessStreamname = this.GetChainName(clientBsn, physicianIdentification, access);
                var authorisationStreamname = this.GetChainName(clientBsn, physicianIdentification, authorisation);

                chain.CreateNewStream(true, itemStreamname);
                chain.CreateNewStream(true, accessStreamname);
                chain.CreateNewStream(true, authorisationStreamname);

                chain.Subscribe(itemStreamname);
                chain.Subscribe(accessStreamname);
                chain.Subscribe(authorisation);
                
                // toevoegen authorisatie arts
                var result = chain.GetStreamItemByKey(authorisationStreamname, authorisation);
                StreamItem streamitem = new StreamItem {data = string.Empty };

                if (result != null && result.streamitems.Any())
                {
                    streamitem = result.streamitems.Last();
                }

                var data = streamitem.data.Any() ? this.DeEncryptHexData(streamitem.data) : string.Empty;
                data += this.EncryptHexData((data.Any() ? ";" : string.Empty) + physicianIdentification);

                var datalocation = this.CreateFileBackup(data);
                chain.PublishMessage(new PublishMessageData {Key = authorisation, HexString = this.EncryptHexData(datalocation + "|" + ("NogGeenKey").Replace("-", string.Empty).Replace(" ", string.Empty)) , StreamName = authorisationStreamname });

                return true;
            }
            
            return false;
        }

        public bool AddPhysicianRights(string patientBsn, string physicianIdentification, string physicianToAdd, string signature)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(new MessageData { address = patientBsn, signature = signature, message = physicianIdentification }) == "true";
            if (verified)
            {
                var result = chain.GetStreamItemByKey(patientBsn, authorisation);
                StreamItem streamitem = new StreamItem();

                if (result.streamitems.Any())
                {
                    streamitem = result.streamitems.Last();
                }

                var data = streamitem.data.Any() ? this.DeEncryptHexData(streamitem.data) : string.Empty;
                data += this.EncryptHexData((data.Any() ? ";" : string.Empty) + physicianIdentification);

                var transactionkey = this.GetTransactionKey(physicianIdentification);
                var transactionId = chain.PublishMessage(new PublishMessageData { Key = transactionkey, HexString = data, StreamName = authorisation } );

                return true;
            }

            return false;
        }

        public bool RemovePhysicianRights(string patientBsn, string physicianIdentification, string physicianToRemove, string signature)
        {
            MultiChain chain = new MultiChain();
            var verified = chain.VerifyMessage(new MessageData { address = patientBsn, signature = signature, message = physicianIdentification }) == "true";
            if (verified)
            {
                var result = chain.GetStreamItemByKey(patientBsn, authorisation);
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

                var transactionkey = this.GetTransactionKey(physicianIdentification);
                var transactionId = chain.PublishMessage(new PublishMessageData { Key = transactionkey, HexString = data, StreamName = authorisation } );
            }
            
            return false;
        }

        public bool CrossStreamCommunicationPost(string patientBsn, string physicianIdentification, string physician2Identification, string messageToPost, string signature, string transactionId = null)
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
            var verified = chain.VerifyMessage(new MessageData { address = patientBsn, signature = signature, message = messageToPost }) == "true";
            if (verified)
            {
                var crossStreamName = patientBsn + "-crossstreamcomm";
                var senderStreamName = this.GetChainName(patientBsn, physicianIdentification, items);

                var transactionkey = this.GetTransactionKey(physicianIdentification);

                var hexdata = this.EncryptHexData(messageToPost);
                var TxId = chain.PublishMessage(new PublishMessageData { Key = transactionkey, HexString = hexdata, StreamName = senderStreamName } );

                var hexTxId = this.EncryptHexData(TxId + "|" + physicianIdentification);
                var TxIdCross = chain.PublishMessage(new PublishMessageData { Key = transactionId ?? TxId, HexString = hexTxId, StreamName = crossStreamName } );
            }

            return false;
        }

        public string[] CrossStreamCommunicationRead(string patientBsn, string transactionId, string signature)
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
            var verified = chain.VerifyMessage(new MessageData { address = patientBsn, signature = signature, message = transactionId } ) == "true";
            if (verified)
            {
                List<string> dataitems = new List<string>();

                var crossStreamName = patientBsn + "-crossstreamcomm";
                var result = chain.GetStreamItemByKey(crossStreamName, transactionId);

                var data = new List<string>();
                data.AddRange(result.streamitems.Select(f => this.DeEncryptHexData(f.data)).ToArray());

                foreach(var i in data)
                {
                    var TxId = i.Split('|')[0];
                    var address = i.Split('|')[1];

                    var chainname = this.GetChainName(patientBsn, address, items);
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
            return string.Format(StreamName, address1, address2, type);
        }

        private string GetTransactionKey(string address1)
        {
            string key = "{0}_{1}";
            return string.Format(key, address1, DateTime.Now.ToString().Replace("-", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty).Replace(" ", string.Empty));
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

        private byte[] ToByteArray(string value)
        {
            byte[] iv = new byte[16];
            for (int i = 0; i < 32; i += 2)
            {
                iv[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }

            return iv;
        }

        private string CreateFileBackup(string data)
        {
            var guid = Guid.NewGuid();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var datalocation = Path.Combine(config.AppSettings.Settings["FileRepo"].Value, guid.ToString());

            var encryptdata = new EncryptStream();
            encryptdata.GenerateNewKey();

            File.WriteAllText(datalocation, data);

            return datalocation;
        }
    }
}
