using Stoneycreek.libraries.MultichainWrapper.Properties;
using System.Configuration;
using System.Resources;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public abstract class MultiChainBase
    {
        #region Properties

        protected ResourceManager ResourceManager;

        protected string ChainLocation { get; set; }

        protected string Chainname { get; set; }

        protected string UtilName { get; set; }

        protected string ClientName { get; set; }

        protected string MyIpAddress { get; set; }

        protected string Portname { get; set; }

        protected string MultichainServer { get; set; }

        protected CommandExecuter CommandExecuter { get; set; }

        #endregion Properties

        #region constructor

        public MultiChainBase(string streamname = null, ProcessWrapper processWrapper = null)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.ResourceManager = Resources.ResourceManager;

            ChainLocation = config.AppSettings.Settings["ChainLocation"].Value;
            Chainname = config.AppSettings.Settings["Chainname"].Value;

            UtilName = config.AppSettings.Settings["UtilName"].Value;
            ClientName = config.AppSettings.Settings["ClientName"].Value;
            Portname = config.AppSettings.Settings["ChainPort"].Value;
            MultichainServer = config.AppSettings.Settings["MultichainServer"].Value;

            this.CommandExecuter = new CommandExecuter(processWrapper);
            MyIpAddress = IpAddress.GetLocalIPAddress();
        }

        #endregion constructor
    }
}
