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
            var config = ConfigurationManager.AppSettings;
            this.ResourceManager = Resources.ResourceManager;

            ChainLocation = config.Get("ChainLocation");
            Chainname = config.Get("Chainname");
            ClientName = config.Get("ClientName");

            this.CommandExecuter = new CommandExecuter(processWrapper);
            MyIpAddress = IpAddress.GetLocalIPAddress();
        }

        #endregion constructor
    }
}
