using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Models;

namespace Web.Services
{
    public class PopulateViewModel
    {
        
        public UserViewModel GetUserViewModel()
        {
            var blockChainService = new BlockChainService();

            var userViewModel = new UserViewModel
            {
                CurrentUser = new User
                {
                    Id = 1,
                    Username = HttpContext.Current.Session["LoggedInUserName"].ToString(),
                    Key = HttpContext.Current.Session["LoggedInUserAccessKey"].ToString()
                },
                SubscribedPatients = blockChainService.GetAllPatients()
            };

            return userViewModel;
        }

        public PatientViewModel GetPatientViewModel(BlockChainPatient subscribedPatient)
        {
            var patientViewModel = new PatientViewModel
            {
                patient = subscribedPatient
            };

            return patientViewModel;
        }
    }


}
