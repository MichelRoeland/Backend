using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Stoneycreek.libraries.MultichainWrapper;
using Stoneycreek.Services.Blockchain.DataAccess;

namespace TestApplication
{
    public partial class FirstClient : Form
    {
        DataAccess da = new DataAccess();

        private int berichtenontvangen = 0;

        private MultiChain chain = null;
        public FirstClient()
        {
            InitializeComponent();
        }

        private void FirstClient_Load(object sender, EventArgs e)
        {
            var info = da.GetStreamsData();
            foreach (var i in info)
            {
                Clients.Items.Add(i.Streamname);
            }
        }

        private void Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chain = new MultiChain(this.Clients.SelectedItem.ToString());
        }

        private string signature = "";

        private string transactionId = "";

        private void button15_Click(object sender, EventArgs e)
        {
            this.chain = new MultiChain(Program.streamname);
            string signature = this.chain.SignMessage(this.privkey.Text, this.message.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            byte[] ba = Encoding.Default.GetBytes(this.message.Text);
            var hexString = BitConverter.ToString(ba).Replace("-", string.Empty).Replace(" ", string.Empty);
            //transactionId = chain.PublishMessage(this.indexkey.Text, this.adres.Text, hexString);
            transactionId = chain.PublishMessage(Program.streamname, hexString);
            Program.SecondClient.MessageReceived(transactionId);
        }

        public void MessageReceived(string transid)
        {
            if (this.Clients.SelectedItem != null)
            {
                berichtenontvangen += 1;
                var permissie = chain.Subscribe(Program.streamname);
                var mystream = chain.GetStreamItem(Program.streamname, transid);
                this.enc.Text = mystream.data;

                var data = this.chain.HexStringToBytes(mystream.data);
                var data2string = Encoding.Default.GetString(data);

                this.dec.Text = data2string;
                aantalberichten.Text = this.berichtenontvangen.ToString();
            }
        }

        public void RefreshStreams()
        {
            this.Clients.Items.Clear();
            var info = da.GetStreamsData();
            foreach (var i in info)
            {
                Clients.Items.Add(i.Streamname);
            }
        }
    }
}
