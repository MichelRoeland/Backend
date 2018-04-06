using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneyCreek.Services.Blockchain.DataContracts
{
    [Serializable]
    public class Physician
    {
        public enum rights
        {
            connect,
            send,
            receive,
            issue,
            create,
            mine,
            activate,
            admin
        }

        public enum physicianType
        {
            Surgeon,
            Cardiologist,
            Dermatologist,
            Endocrinolist,
            Gastroenterologist,
            Nephrologist,
            Ophthalmologist,
            otolaryngologist,
            obstetrician,
            Gynecologist,
            Pediatrician,
            pulmonologist,
            psychiatrist
        }

        public string Address { get; set; }
        public physicianType PhysicianType { get; set; }
        public rights[] Rights { get; set; } 
        public string[] ConnectedStreams { get; set; }
    }
}
