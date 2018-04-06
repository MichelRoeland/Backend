using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;

using Newtonsoft.Json;

using Owin;

using Stoneycreek.libraries.MultichainWrapper;
using Stoneycreek.libraries.MultichainWrapper.Structs;
using Stoneycreek.Services.Blockchain.DataAccess;
using Stoneycreek.Services.Blockchain.DataAccess.DataCom;

using Timer = System.Threading.Timer;
using Stoneycreek.Libraries.Blockchain.SignallR;

namespace BlockchainTest
{

    public partial class Form1 : Form
    {
        
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public string chainname = "mytestchain";
        MultiChain chain = new MultiChain();

        private string privatekey = "VAgxaGgnhawPv9VEamtRPp4zX5s5ywTrPbzmHLBcQH7SQdJ3q3197J8n";

        private string publicKey = "022027dede64a605ad7a9422ff2c4a85e401689da72e801f564fe66cf94648883e";

        private string address = "16HAdzhXUX78pFrVJ244jQABSGYvG7iWuHcqjG";

        private string signature = "";

        private string testmessage = "Dit is een test voor de brandweer gouda.";

        private string transactionId = "";
        
        private string ChainLocation { get; set; }
        private string Chainname { get; set; }
        private string ChainClone { get; set; }

        private string UtilName { get; set; }
        private string ClientName { get; set; }
        private string IpAddress { get; set; }
        private string Portname { get; set; }

        private string MultichainServer { get; set; }
        private string Streamname { get; set; }


        public StreamItem mystream { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newaddress = chain.CreateNewAddress();

            config.AppSettings.Settings["ClientAddress"].Value = newaddress;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task t = new Task(
                () =>
                    {
                        chain.StartServerProcess();
                    }); 
            t.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.chain.CreateNewChain(Chainname,01, 01, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var permissie = chain.GrantPermisions(
                "1YDR2LGn2SPAjfD7D8tdPmknSQqP4pESQt2Sg3",
                new[]
                    {
                        MultichainClientCommands.GrantPermissions.send, MultichainClientCommands.GrantPermissions.receive,
                        MultichainClientCommands.GrantPermissions.create
                    });

            config.AppSettings.Settings["PermissionTransactionId"].Value = permissie;
            config.Save(ConfigurationSaveMode.Modified);
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var permissie = chain.ListPermissions();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var permissie = chain.ListStreams();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var permissie = chain.CreateNewStream(true, "pinjo");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var permissie = chain.Subscribe("pinjo");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var permissie = chain.UnSubscribe("pinjo");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mystream = chain.GetStreamItem("pinjo", transactionId);

            this.publisher.Text = mystream.publishers.First();
            this.confirmed.Text = mystream.confirmations.ToString();

            this.key.Text = mystream.key;
            this.txid.Text = mystream.txid;
            this.blocktime.Text = mystream.blocktime.ToString();
            this.data.Text = mystream.data;

            var data = this.chain.HexStringToBytes(mystream.data);
            var data2string = Encoding.Default.GetString(data);

            this.datatrans.Text = data2string;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            var permissie = chain.GetStreamKeys("pinjo");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var permissie = chain.GetStreamItemByKey("pinjo", this.key.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            byte[] ba = Encoding.Default.GetBytes(this.message.Text);
            var hexString = BitConverter.ToString(ba).Replace("-", string.Empty).Replace(" ", string.Empty);
            //transactionId = chain.PublishMessage(this.indexkey.Text, this.adres.Text, hexString);
            transactionId = chain.PublishMessage(this.indexkey.Text, hexString);
            this.transaction.Text = this.transactionId;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Multichain-cli.exe mytestchain signmessage V9ZPNkWEB8NEzWoSzzJ82XbT184o65jcJXWztZvEzRKoiX8jmj2Haw67 "dit is een test voor de brandweer gouda"
            signature = this.chain.SignMessage(this.privkey.Text, this.message.Text);
            this.sign.Text = this.signature;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var permissie = chain.CreateKeys();
            this.privkey.Text = permissie.keypairs.First().privkey;
            this.adres.Text = permissie.keypairs.First().address;
            this.pubkey.Text = permissie.keypairs.First().pubkey;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            var address = this.adres.Text;
            var result = this.chain.VerifyMessage(address, this.sign.Text, this.datatrans.Text);

            if (result == "true")
            {
                this.yes.Checked = true;
                this.no.Checked = false;
            }
            else
            {
                this.yes.Checked = false;
                this.no.Checked = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChainLocation = config.AppSettings.Settings["ChainLocation"].Value;
            Chainname = config.AppSettings.Settings["Chainname"].Value;
            ChainClone = config.AppSettings.Settings["Chainclonename"].Value;

            UtilName = config.AppSettings.Settings["UtilName"].Value;
            ClientName = config.AppSettings.Settings["ClientName"].Value;
            Portname = config.AppSettings.Settings["ChainPort"].Value;
            MultichainServer = config.AppSettings.Settings["MultichainServer"].Value;
            Streamname = config.AppSettings.Settings["Streamname"].Value;

            this.streamname.Text = Streamname;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RSACrypt crypt = new RSACrypt();
            var result = crypt.AssignNewKey();
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            var privkeyimport = this.chain.ImportPrivateKey(this.privkey.Text);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var permissions = new List<MultichainClientCommands.GrantPermissions>();
            if (this.connect.Checked){ permissions.Add(MultichainClientCommands.GrantPermissions.connect); }
            if (this.send.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.send); }
            if (this.receive.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.receive); }
            if (this.create.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.create); }
            if (this.isse.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.issue); }
            if (this.mine.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.mine); }
            if (this.admin.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.admin); }
            if (this.activate.Checked) { permissions.Add(MultichainClientCommands.GrantPermissions.activate); }

            var grant = this.chain.GrantPermisions(this.adres.Text, permissions.ToArray());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            byte[] ba = Encoding.Default.GetBytes(this.message.Text);
            var hexString = BitConverter.ToString(ba).Replace("-", string.Empty).Replace(" ", string.Empty);
            //transactionId = chain.PublishMessage(this.indexkey.Text, this.adres.Text, hexString);
            transactionId = chain.PublishMessage(this.indexkey.Text, this.adres.Text, hexString);
            this.transaction.Text = this.transactionId;
        }

        private void button21_Click(object sender, EventArgs e)
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

            var grant = this.chain.RevokePermisions(this.adres.Text, permissions.ToArray());
        }

        private void button22_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            da.SaveClientData(new Client() { Client1 = "ABN Amro", ClientId = Guid.NewGuid() });
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            da.SaveClientKeys(new ClientKeypairs() {Address = this.adres.Text, PublicKey = this.pubkey.Text, ClientId = 1});
            //new KeyPair() { address = adres.Text, privkey = this.privkey.Text, pubkey = this.pubkey.Text)}
        }

        private void button25_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            var result = da.GetClientData();

        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            SignallerServer server = new SignallerServer();
            server.StartServer();
        }
    }

    public static class ReflectionUtil
    {
        public static T DeserializeFile<T>(string fileName, Func<string, T> customDeserialization = null)
        {
            Assembly assembly = typeof(ReflectionUtil).GetTypeInfo().Assembly;

            string resourceName = fileName;
                
            using (StreamReader reader = new StreamReader(resourceName))
            {
                string json = reader.ReadToEnd();
                return customDeserialization == null ? JsonConvert.DeserializeObject<T>(json) : customDeserialization(json);
            }
            
        }
    }
}

