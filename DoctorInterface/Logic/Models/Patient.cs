using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logic.Models
{
    public abstract class Patient
    {
        public int Id { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }

        public PatientAddress PatientAddress { get; set; }

        public List<PatientStream> Streams { get; set; }
    }

    public class SubscribedPatient: Patient
    {

        public List<Stream> AccessibleStreams { get; set; }

        public Stream CurrentUserStream { get; set; }

        public SubscribedPatient()
        {
            //Fill list of Accessible Streams
            //Fill list of CurrentUserStream
        }
    }


    public class SearchPatient
    {
        public string BSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class PatientStream
    {
        public int Id { get; set; }
        public int StreamOwnerId { get; set; }
        public string StreamOwnerName { get; set; }
        public string StreamOwnerType { get; set; }
        public bool Access { get; set; }
    }

    public class PatientAddress
    {
        [Display(Name = "Straat")]
        public string Street { get; set; }
        [Display(Name = "Huisnummer")]
        public int HouseNumber { get; set; }
        [Display(Name = "Stad")]
        public string City { get; set; }
        [Display(Name = "Telefoon nummer")]
        public string PhoneNumber { get; set; }

    }
}