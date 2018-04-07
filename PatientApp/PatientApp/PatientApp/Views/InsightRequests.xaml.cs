using PatientApp.Controls;
using PatientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PatientApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InsightRequests : ContentPage
	{
		public InsightRequests ()
		{
			InitializeComponent ();
            // Create stub data
            InsightRequest[] stubList = {
                    new InsightRequest { doctorName = "Doctor Hank", requestDate = DateTime.Now, isApproved = false, isProcessed = false, doctorAddress = "doctorkey001", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Frank", requestDate = DateTime.Now, isApproved = false, isProcessed = true, doctorAddress = "doctorkey001", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Tank", requestDate = DateTime.Now, isApproved = true, isProcessed = true, doctorAddress = "doctorkey001", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Skank", requestDate = DateTime.Now, isApproved = true, isProcessed = true, doctorAddress = "doctorkey001", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Bank", requestDate = DateTime.Now, isApproved = false, isProcessed = false, doctorAddress = "doctorkey001", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Prank", requestDate = DateTime.Now, isApproved = false, isProcessed = true, doctorAddress = "doctorkey001", patientAddress = "patientkey001" }
                };

            List<InsightRequest> toApprove = new List<InsightRequest>();
            List<InsightRequest> approved = new List<InsightRequest>();
            List<InsightRequest> rejected = new List<InsightRequest>();

            foreach (InsightRequest i in stubList)
            {
                if (i.isProcessed == false)
                {
                    var test = i.doctorAddress;
                    toApprove.Add(i);
                }
                else
                {
                    if (i.isApproved == true)
                    {
                        approved.Add(i);
                    }
                    else{
                        rejected.Add(i);
                    }
                }
            }

            FillData(this.PendingData, toApprove);
            FillData(this.ApprovedData, approved);
            FillData(this.DeniedData, rejected);

        }

        public void FillData(Xamarin.Forms.Grid x, List<InsightRequest> toApprove)
        {
            x.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto }
            };

            x.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            };

            int r = 0;
            foreach (InsightRequest i in toApprove)
            {
                x.Children.Add(new Label { Text = i.doctorName }, 0, r);
                x.Children.Add(new Label { Text = i.requestDate.ToString() }, 1, r);
                if (x == this.PendingData)
                {
                    x.Children.Add(new Label { Text = "-" }, 2, r);
                }
                else if (x == this.ApprovedData)
                {
                    x.Children.Add(new Label { Text = "Yes" }, 2, r);
                }
                else
                {
                    x.Children.Add(new Label { Text = "No" }, 2, r);
                }
                x.Children.Add(new Label { Text = "-" }, 3, r);

                r++;
            }
        }

	}
}