using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }

        //public IEnumerable<Patient> SubscribedPatients { get; set; }
    }
}