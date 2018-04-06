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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;

            // Option 'View Insight Requests
            if(item.Id == 0)
            {
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
                        else
                        {
                            rejected.Add(i);
                        }
                    }
                }
            }
        }
    }
}