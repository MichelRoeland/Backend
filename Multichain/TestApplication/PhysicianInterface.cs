using Stoneycreek.libraries.MultichainWrapper;
using StoneyCreek.Services.Blockchain.DataContracts.StreamContracts;
using System;
using System.Configuration;
using System.IO;
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

            docId.SelectedIndex = 0;
            type.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            patientChain.CreateNewPatientChain(new NawContracts
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
            if (patientView.SelectedItems.Count == 0)
                return;

            var patientstreams = patients.Where(f => f.BsnNumber == patientView.SelectedItems[0].SubItems[2].Text);
            if (patientstreams.Any())
            {
                var mainstream = patientstreams.First();
                Streams.Items.Clear();
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
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var privatekey = config.AppSettings.Settings["privkey"].Value;
            var address = config.AppSettings.Settings["address"].Value;

            patientChain.AddPhysician(address, docId.Text, patientView.SelectedItems[0].SubItems[2].Text, patientChain.SignMessage(privatekey, docId.Text));
        }

        private void Streams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (patientView.SelectedItems.Count == 0 || Streams.SelectedItems.Count == 0)
                return;


            PatientChain patientChain = new PatientChain();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var privatekey = config.AppSettings.Settings["privkey"].Value;

            var result = patientChain.GetChainData(new PatientChain.ParameterClass
            {
                DataToStore = ToevoegingInStream.Text,
                Streamname = Streams.SelectedItems[0].SubItems[1].Text,
                PhysicianId = docId.Text,
                Signature = patientChain.SignMessage(privatekey, docId.Text),
                Address = config.AppSettings.Settings["address"].Value,
                PatientId = patientView.SelectedItems[0].SubItems[2].Text
            });

            content.Items.Clear();

            if (result != null)
            {
                foreach (var i in result)
                {
                    var lv = new ListViewItem("-");
                    lv.SubItems.Add("-");
                    lv.SubItems.Add(i);
                    content.Items.Add(lv);
                }
            }
            else
            {
                MessageBox.Show(
                    "Er is voor deze patient geen medische informatie of u bent niet geautoriseerd om medische stromen te zien voor deze patient.",
                    "Medical streams", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (patientView.SelectedItems.Count == 0 || Streams.SelectedItems.Count == 0)
                return;


            PatientChain patientChain = new PatientChain();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var privatekey = config.AppSettings.Settings["privkey"].Value;

            patientChain.SetChainData(new PatientChain.ParameterClass
            {
                DataToStore = ToevoegingInStream.Text,
                Streamname = Streams.SelectedItems[0].SubItems[1].Text.Replace("-items", string.Empty) + "-" + type.Text,
                PhysicianId = docId.Text,
                Signature = patientChain.SignMessage(privatekey, docId.Text),
                Address = config.AppSettings.Settings["address"].Value,
                PatientId = patientView.SelectedItems[0].SubItems[2].Text
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();
            patientChain.CreateNewPatientChain(new NawContracts()
            {
                Address = "Logboek",
                BsnNumber = "",
                City = "",
                Country = "",
                DateLastMutation = new DateTime()
            });
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (patientView.SelectedItems.Count == 0 || Streams.SelectedItems.Count == 0)
                return;

            content.Items.Clear();
            PatientChain patientChain = new PatientChain();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var privatekey = config.AppSettings.Settings["privkey"].Value;

            var result = patientChain.GetChainData(new PatientChain.ParameterClass
            {
                DataToStore = ToevoegingInStream.Text,
                Streamname = Streams.SelectedItems[0].SubItems[1].Text.Replace("-items", string.Empty) + "-" + type.Text,
                PhysicianId = docId.Text,
                Signature = patientChain.SignMessage(privatekey, docId.Text),
                Address = config.AppSettings.Settings["address"].Value,
                PatientId = patientView.SelectedItems[0].SubItems[2].Text,
                StreamType = type.Text == @"Items" ? PatientChain.ParameterClass.type.Items : PatientChain.ParameterClass.type.Log
            });

            content.Items.Clear();

            foreach (var i in result)
            {
                var lv = new ListViewItem("-");
                lv.SubItems.Add("-");
                lv.SubItems.Add(i);
                content.Items.Add(lv);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void content_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (content.SelectedItems.Count > 0)
            {
                if (type.Text == "Items")
                {
                    FullTextview.Text = content.SelectedItems[0].SubItems[2].Text;
                }
                else
                {
                    FullTextview.Rtf = content.SelectedItems[0].SubItems[2].Text;
                }
                
            }
        }

        private void docId_SelectedIndexChanged(object sender, EventArgs e)
        {
            content.Items.Clear();
            Streams.SelectedItems.Clear();
            FullTextview.Text = "";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            PatientChain patientChain = new PatientChain();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var privatekey = config.AppSettings.Settings["privkey"].Value;

            patientChain.AddPhysicianRights(patientView.SelectedItems[0].SubItems[2].Text, docId.Text,
                patientChain.SignMessage(privatekey, docId.Text));
        }
    }
}
