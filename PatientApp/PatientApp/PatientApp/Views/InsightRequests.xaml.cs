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
                    new InsightRequest { doctorName = "Doctor Frank", requestDate = DateTime.Now, isApproved = false, isProcessed = true, doctorAddress = "doctorkey002", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Tank", requestDate = DateTime.Now, isApproved = true, isProcessed = true, doctorAddress = "doctorkey003", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Skank", requestDate = DateTime.Now, isApproved = true, isProcessed = true, doctorAddress = "doctorkey004", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Bank", requestDate = DateTime.Now, isApproved = false, isProcessed = false, doctorAddress = "doctorkey005", patientAddress = "patientkey001" },
                    new InsightRequest { doctorName = "Doctor Prank", requestDate = DateTime.Now, isApproved = false, isProcessed = true, doctorAddress = "doctorkey006", patientAddress = "patientkey001" }
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
            int fz = 12;
            x.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = 40},
                new RowDefinition { Height = 40}
            };
            x.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            };

            int r = 0;
            foreach (InsightRequest i in toApprove)
            {
                // Create list of possible items to pic
                Dictionary<string, Boolean> list = new Dictionary<string, Boolean>
                {
                    { "Accept", true },
                    { "Deny", false }
                };

                Picker picker = new Picker { Scale = 0.60, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Center, WidthRequest = 70 };

                foreach (string pk in list.Keys)
                {
                    picker.Items.Add(pk);
                }

                x.Children.Add(new Label { Text = i.doctorName, FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand}, 0, r);
                x.Children.Add(new Label { Text = i.requestDate.ToString(), FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }, 1, r);
                if (x == this.PendingData)
                {
                    x.Children.Add(new Label { Text = "-", FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }, 2, r);
                }
                else if (x == this.ApprovedData)
                {
                    x.Children.Add(new Label { Text = "Yes", FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }, 2, r);
                    picker.SelectedIndex = 0;
                }
                else
                {
                    x.Children.Add(new Label { Text = "No", FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }, 2, r);
                    picker.SelectedIndex = 1;
                }
                x.Children.Add(picker, 3, r);
                x.Children.Add(new Label { Text = i.doctorAddress, FontSize = fz, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }, 4, r);

                r++;
            }
        }

        public void SaveData(object sender, EventArgs args)
        {
            foreach(View x in PendingData.Children)
            {
                PendingData.Children.Add( new Label { Text = x.ToString()});
            }
        }

    }
}