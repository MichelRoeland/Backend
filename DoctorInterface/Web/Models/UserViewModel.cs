using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserViewModel
    {
        public User CurrentUser { get; set; }

        public IEnumerable <SubscribedPatient> SubscribedPatients { get; set; }
    }
}