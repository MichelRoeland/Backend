using System;
using System.Collections.Generic;
using System.Text;

namespace PatientApp.Models
{
    public class InsightRequest
    {

        public string doctorName { get; set; }
        public DateTime requestDate { get; set; }
        public Boolean isApproved { get; set; }
        public Boolean isProcessed { get; set; }
        public string doctorAddress { get; set; }
        public string patientAddress { get; set; }

        //public InsightRequest(string nm, DateTime rq, Boolean appr, Boolean proc, string daddr, string paddr)
        //{
        //    doctorName = nm;
        //    requestDate = rq;
        //    isApproved = appr;
        //    isProcessed = proc;
        //    doctorAddress = daddr;
        //    patientAddress = paddr;
        //}

    }
}
