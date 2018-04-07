using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    public class DataItemToStore : DataItem
    {
        [DataMember]
        public string DataToStore { get; set; }
    }
}