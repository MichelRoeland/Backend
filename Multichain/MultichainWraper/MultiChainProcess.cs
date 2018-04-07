using Stoneycreek.libraries.MultichainWrapper.Properties;
using StoneyCreek.Services.Blockchain.DataContracts;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Resources;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class MultiChainProcess : MultiChainBase
    {
        #region constructor

        public MultiChainProcess(string streamname = null, ProcessWrapper processWrapper = null) : base(streamname, processWrapper)
        {
        }

        #endregion constructor

        #region Public Methods

        // https://www.multichain.com/developers/stream-confidentiality/
        // https://www.multichain.com/developers/creating-connecting/
        public string StartServerProcess()
        {
            var outputdir = Path.Combine(ChainLocation, Chainname);
            // Start server
            var startChain = ChainLocation + MultichainServer + " " + Chainname + "@" + MyIpAddress + ":" + Portname + " -daemon";

            return this.CommandExecuter.ExecuteCommand(startChain, "Blockchain parameter set was successfully cloned.");
        }

        public string StartClientProcess()
        {
            // To create a new blockchain called [chain-name] based on MultiChain’s default blockchain parameters, run:
            // parameters kunnen worden gewijzigd. -> zie 
            var command = ChainLocation + ClientName + " " + Chainname + " -deamon";

            return this.CommandExecuter.ExecuteCommand(command, "Blockchain parameter set was successfully generated.").Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string CreateNewChain(NewChainData data)
        {
            var command = ChainLocation + UtilName + " create " + data.ChainName;

            string parameters = string.Format(CommandlineParameters.AdminConsensusAdmin, data.AdminConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, CommandlineParameters.AdminConsensusCreate, data.CreateConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, CommandlineParameters.SetupFirstBlocks, data.FirstBlocks);

            command += " " + parameters;

            return this.CommandExecuter.ExecuteCommand(command, "Blockchain parameter set was successfully generated.");
        }

        public string CloneChain(string chainName, string cloneName)
        {
            // Alternatively, to create a new blockchain based on the parameters of an existing chain [old-name], run:
            var command = ChainLocation + UtilName + " clone " + chainName + " " + cloneName;

            return this.CommandExecuter.ExecuteCommand(command, "Blockchain parameter set was successfully generated.").Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        #endregion Public Methods
    }
}
