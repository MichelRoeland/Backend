using Stoneycreek.libraries.MultichainWrapper;
using StoneyCreek.Services.Blockchain.DataContracts.StreamContracts;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class PhysicianInterface : Form
    {
        private NawContracts[] patients { get; set; }
        public PhysicianInterface()
        {
            InitializeComponent();
        }

        private void PhysicianInterface_Load(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            patients = patientChain.GetPatients();
            foreach(var i in patients)
            {
                ListViewItem lvi = new ListViewItem(i.Firstname);
                lvi.SubItems.Add(i.Lastname);
                lvi.SubItems.Add(i.BsnNumber);
                patientView.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            patientChain.CreateNewPatientChain(new StoneyCreek.Services.Blockchain.DataContracts.StreamContracts.NawContracts
            {
                Address = null,
                BsnNumber = bsnnumber.Text,
                City = city.Text,
                Country = country.Text,
                DateLastMutation = DateTime.Now,
                Firstname = firstname.Text,
                Housenumber = housenumber.Text,
                Initials = initials.Text,
                Lastname = lastname.Text,
                Streetname = streetname.Text
            });
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void patientView_SelectedIndexChanged(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            var patientstreams = patients.Where(f => f.BsnNumber == patientView.SelectedItems[0].SubItems[2].Text);
            if (patientstreams.Any())
            {
                var mainstream = patientstreams.First();
                foreach (var i in mainstream.ItemsList)
                {
                    var lv = new ListViewItem(i.DateTimeMutation.ToString());
                    lv.SubItems.Add(i.PhysicianIdentification);
                    lv.SubItems.Add(i.DataBlocks.ToString());
                    Streams.Items.Add(lv);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            patientChain.AddPhysician(patientView.SelectedItems[0].SubItems[2].Text, docId.Text, patientChain.SignMessage("", docId.Text));
        }
    }
}
