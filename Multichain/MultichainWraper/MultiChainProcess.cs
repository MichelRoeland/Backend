using Stoneycreek.libraries.MultichainWrapper.Properties;
using StoneyCreek.Services.Blockchain.DataContracts;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class MultiChainProcess
    {
        #region Private Fields

        private Configuration config;
        private readonly ResourceManager ResourceManager;

        #endregion Private Fields

        #region Properties

        private string ChainLocation { get; set; }

        private string Chainname { get; set; }

        private string UtilName { get; set; }

        private string ClientName { get; set; }

        private string MyIpAddress { get; set; }

        private string Portname { get; set; }

        private string MultichainServer { get; set; }

        public ProcessWrapper processWrapper { get; set; }

        #endregion Properties

        #region constructor

        public MultiChainProcess(string streamname = null, ProcessWrapper processWrapper = null)
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.ResourceManager = Resources.ResourceManager;

            ChainLocation = config.AppSettings.Settings["ChainLocation"].Value;
            Chainname = config.AppSettings.Settings["Chainname"].Value;

            UtilName = config.AppSettings.Settings["UtilName"].Value;
            ClientName = config.AppSettings.Settings["ClientName"].Value;
            Portname = config.AppSettings.Settings["ChainPort"].Value;
            MultichainServer = config.AppSettings.Settings["MultichainServer"].Value;

            this.processWrapper = processWrapper ?? new ProcessWrapper();
            MyIpAddress = IpAddress.GetLocalIPAddress();
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

            return ExecuteCommand(startChain, "Blockchain parameter set was successfully cloned.");
        }

        public string StartClientProcess()
        {
            // To create a new blockchain called [chain-name] based on MultiChain’s default blockchain parameters, run:
            // parameters kunnen worden gewijzigd. -> zie 
            var command = ChainLocation + ClientName + " " + Chainname + " -deamon";

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.").Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string CreateNewChain(NewChainData data)
        {
            var command = ChainLocation + UtilName + " create " + data.ChainName;

            string parameters = string.Format(CommandlineParameters.AdminConsensusAdmin, data.AdminConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, CommandlineParameters.AdminConsensusCreate, data.CreateConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, CommandlineParameters.SetupFirstBlocks, data.FirstBlocks);

            command += " " + parameters;

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.");
        }

        public string CloneChain(string chainName, string cloneName)
        {
            // Alternatively, to create a new blockchain based on the parameters of an existing chain [old-name], run:
            var command = ChainLocation + UtilName + " clone " + chainName + " " + cloneName;

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.").Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        #endregion Public Methods

        #region Private Methods

        private string ExecuteCommand(string command, string containstext)
        {
            int exitCode;
            ProcessStartInfo processInfo;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            this.processWrapper.ProcessStart(processInfo); // Process.Start(processInfo);
            this.processWrapper.WaitForExit();

            string output = this.processWrapper.OutputReadToEnd();
            string error = this.processWrapper.ErrorReadToEnd();

            if (containstext != null)
            {
                var iscorrect = output.Contains(containstext);
                exitCode = this.processWrapper.ExitCode;
                Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
                Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
                Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
                this.processWrapper.Close();
            }

            return output;
        }

        #endregion Private Methods
    }
}
