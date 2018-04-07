using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;
using static System.Collections.Specialized.BitVector32;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //need to set SessionState
        
        public bool login = false;

        public ActionResult Index(bool? Login)
        {
            login = Login.HasValue ? Login.Value : false;

            if (!login)
            {
                
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var populateViewModel = new PopulateViewModel();

                var userViewModel = populateViewModel.GetUserViewModel();

                return View(userViewModel);
            }

           
        }

        public ActionResult SubscribedPatientDetails(UserViewModel userViewModel)
        {
            if(userViewModel != null)
            { 
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        public PartialViewResult _SearchPatient()
        {

            var searchPatientViewModel = new SearchPatientViewModel();

            return PartialView(searchPatientViewModel);
        }

        public PartialViewResult _NewEntryModal()
        {

            var searchPatientViewModel = new SearchPatientViewModel();

            return PartialView(searchPatientViewModel);
        }


        [HttpPost]
        public ActionResult NewEntry(EntryViewModel entryViewModel)
        {
            return PartialView(entryViewModel);
        }


        public ActionResult SearchPatient(SearchPatientViewModel searchPatientViewModel)
        {
            //Search for patient
            var foundPatients = new List<SearchPatient>();

            if(foundPatients.Count() < 1)
            {
                return RedirectToAction("NoPatientsFound", "Home");
            }
            else
            {
                return RedirectToAction("FoundPatients", "Home", new { foundPatientsViewModel = searchPatientViewModel });
            }
        }

        public ActionResult FoundPatients(FoundPatientsViewModel foundPatientsViewModel)
        {
            return View(foundPatientsViewModel);
        }

        public ActionResult NoPatientsFound()
        {
            return View();
        }
    }
}