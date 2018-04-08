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

        public ActionResult SubscribedPatientDetails(BlockChainPatient patient)
        {
            if(patient != null)
            {
                var patientViewModel = new PatientViewModel();

                patientViewModel.patient = patient;

                return View(patientViewModel);
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

            var entryViewModel = new EntryViewModel();

            return PartialView(entryViewModel);
        }


        [HttpPost]
        public ActionResult NewEntry(EntryViewModel entryViewModel)
        {
            return PartialView(entryViewModel);
        }


        public ActionResult SearchPatient(SearchPatientViewModel searchPatientViewModel)
        {
            //search for all patients
            var blockChainApiService = new Logic.Services.BlockChainService();
            var foundPatients = blockChainApiService.GetAllPatients();
            var foundPatientsViewModel = new FoundPatientsViewModel();
            foundPatientsViewModel.FoundPatients = foundPatients;

            if (foundPatients.Count() < 1)
            {
                return RedirectToAction("NoPatientsFound", "Home");
            }
            else
            {
                return RedirectToAction("FoundPatients", "Home", new { foundPatientsViewModel = foundPatientsViewModel });
            }
        }

        public ActionResult FoundPatients(FoundPatientsViewModel foundPatientsViewModel)
        {
            var blockChainApiService = new Logic.Services.BlockChainService();
            var foundPatients = blockChainApiService.GetAllPatients();
            var foundPatientsViewModel2 = new FoundPatientsViewModel();
            foundPatientsViewModel2.FoundPatients = foundPatients;

            return View(foundPatientsViewModel2);
        }

        public ActionResult NoPatientsFound()
        {
            return View();
        }
    }
}