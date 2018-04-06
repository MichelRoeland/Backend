using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Stoneycreek.libraries.MultichainWrapper;
using Stoneycreek.Services.Blockchain.DataAccess;
using Stoneycreek.Services.Blockchain.DataAccess.DataCom;

namespace TestApplication
{
    public partial class BCPartner1 : Form
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        MultiChain chain = new MultiChain();
        private string ChainLocation { get; set; }
        private string Chainname { get; set; }
        private string ChainClone { get; set; }

        private string UtilName { get; set; }
        private string ClientName { get; set; }
        private string IpAddress { get; set; }
        private string Portname { get; set; }

        private string MultichainServer { get; set; }
        private string Streamname { get; set; }

        public BCPartner1()
        {
            InitializeComponent();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (this.keypairname.Text.Any())
            {
                var permissie = chain.CreateKeys();
                this.newkeypair.Visible = false;
                this.keypairok.Visible = true;

                var privkey = permissie.keypairs.First().privkey;
                var adres = permissie.keypairs.First().address;
                var pubkey = permissie.keypairs.First().pubkey;

                var privkeyimport = this.chain.ImportPrivateKey(privkey);
                this.privatekey.Visible = false;
                this.privatekeyok.Visible = true;

                var grant= this.GrandPermisions(adres);
                this.grantpermissions.Visible = false;
                this.grantpermissionsok.Visible = true;

                DataAccess da =new DataAccess();
                da.SaveClientData(
                    new Client()
                        {
                            Client1 = this.keypairname.Text,
                            ClientId = Guid.NewGuid(),
                            ClientKeypairs =
                                new List<ClientKeypairs> { new ClientKeypairs() { Address = adres, PublicKey = pubkey, ClientId = 1 } }
                        });

                this.privkey.Text = privkey;
                this.pubkey.Text = pubkey;
                this.adres.Text = adres;

                ReloadClientData();

                Program.SecondClient.RefreshStreams();
                Program.FirstClient.RefreshStreams();
            }
            else
            {
                MessageBox.Show(
                    "No clientId provided, please fix this issue.",
                    "Client Id not availlable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BCPartner1_Load(object sender, EventArgs e)
        {
            ChainLocation = config.AppSettings.Settings["ChainLocation"].Value;
            Chainname = config.AppSettings.Settings["Chainname"].Value;
            ChainClone = config.AppSettings.Settings["Chainclonename"].Value;

            UtilName = config.AppSettings.Settings["UtilName"].Value;
            ClientName = config.AppSettings.Settings["ClientName"].Value;
            Portname = config.AppSettings.Settings["ChainPort"].Value;
            MultichainServer = config.AppSettings.Settings["MultichainServer"].Value;
            Streamname = config.AppSettings.Settings["Streamname"].Value;


            ReloadClientData();
            Program.FirstClient = new FirstClient();
            Program.SecondClient = new SecondClient();
            Program.FirstClient.Show();
            Program.SecondClient.Show();
            //this.streamname.Text = Streamname;

            DataAccess da = new DataAccess();
            var info = da.GetStreamsData();
            foreach (var i in info)
            {
                Clients.Items.Add(i.Streamname);
            }
        }

        private void ReloadClientData()
        {
            //DataAccess da = new DataAccess();
            //var result = da.GetClientData().ToArray();
            //foreach (var i in result)
            //{
            //    Clients.Items.Add(i.Client1);
            //}
        }

        private string GrandPermisions(string address)
        {
            var permissions = new List<MultichainClientCommands.GrantPermissions>();
            if (this.connect.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.connect); }
            if (this.send.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.send); }
            if (this.receive.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.receive); }
            if (this.create.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.create); }
            if (this.isse.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.issue); }
            if (this.mine.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.mine); }
            if (this.admin.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.admin); }
            if (this.activate.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.activate); }

            var grant = this.chain.GrantPermisions(address, permissions.ToArray());
            return grant;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var newstream1 = chain.CreateNewStream(true, streamname.Text);
            newstream.Visible = false;
            this.newstreamok.Visible = true;

            DataAccess da = new DataAccess();
            da.SaveStream(new Streams {Streamname = this.streamname.Text, StreamIdentifier = newstream1});

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chain = new MultiChain(this.Clients.SelectedItem.ToString());
        }

        private void Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.streamname = this.Clients.SelectedItem.ToString();
        }
    }
}
