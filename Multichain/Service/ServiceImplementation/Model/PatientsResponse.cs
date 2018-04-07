using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceImplementation.Model
{
    [DataContract]
    public class PatientsResponse
    {
        [DataMember]
        public List<Patient> Patients { get; set; }
    }
}