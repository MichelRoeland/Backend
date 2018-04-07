using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class PopulateViewModel
    {
        public UserViewModel GetUserViewModel()
        {
            var userViewModel = new UserViewModel
            {
                CurrentUser = new User
                {
                    Id = 1,
                    Username = "DoctorHouse"

            //Add SubscribedPatients

                }
            };

            return userViewModel;
        }

        public PatientViewModel GetPatientViewModel(SubscribedPatient subscribedPatient)
        {
            var patientViewModel = new PatientViewModel
            {
                patient = subscribedPatient
            };

            return patientViewModel;
        }
    }


}
