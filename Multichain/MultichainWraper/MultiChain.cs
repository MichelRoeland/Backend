﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using Newtonsoft.Json;

using Stoneycreek.libraries.MultichainWrapper.Structs;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class MultiChain
    {
        #region Private Fields

        private Configuration config;

        #endregion Private Fields

        #region Properties

        private string ChainLocation { get; set; }

        private string Chainname { get; set; }

        private string ChainClone { get; set; }

        private string UtilName { get; set; }

        private string ClientName { get; set; }

        private string IpAddress { get; set; }

        private string Portname { get; set; }

        private string MultichainServer { get; set; }

        private string SteamName { get; set; }

        public ProcessWrapper processWrapper { get; set; }

        #endregion Properties

        #region constructor

        public MultiChain(string streamname = null, ProcessWrapper processWrapper = null)
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ChainLocation = config.AppSettings.Settings["ChainLocation"].Value;
            Chainname = config.AppSettings.Settings["Chainname"].Value;
            ChainClone = config.AppSettings.Settings["Chainclonename"].Value;

            UtilName = config.AppSettings.Settings["UtilName"].Value;
            ClientName = config.AppSettings.Settings["ClientName"].Value;
            Portname = config.AppSettings.Settings["ChainPort"].Value;
            MultichainServer = config.AppSettings.Settings["MultichainServer"].Value;

            if (streamname != null)
            {
                SteamName = streamname;
            }
            else
            {
                SteamName = config.AppSettings.Settings["Streamname"]?.Value;
            }


            this.processWrapper = processWrapper ?? new ProcessWrapper();
            IpAddress = this.GetLocalIPAddress();
        }

        #endregion constructor

        #region Public Methods

        public byte[] HexStringToBytes(string hexString)
        {
            if (hexString == null) throw new ArgumentNullException("hexString");
            if (hexString.Length % 2 != 0) throw new ArgumentException("hexString must have an even length", "hexString");
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }
            return bytes;
        }

        // https://www.multichain.com/developers/stream-confidentiality/
        // https://www.multichain.com/developers/creating-connecting/
        public string StartServerProcess()
        {
            var outputdir = Path.Combine(ChainLocation, Chainname);
            // Start server
            var startChain = ChainLocation + MultichainServer + " " + Chainname + "@" + IpAddress + ":" + Portname + " -daemon";
                // + " -datadir=" + outputdir;

            return ExecuteCommand(startChain, "Blockchain parameter set was successfully cloned.");
        }

        public string CreateNewChain(string chainName, double adminConsensus, double createConsensus, int firstBlocks)
        {
            var command = ChainLocation + UtilName + " create " + chainName;

            string parameters = string.Format(MultichainUtilParameters.CommandlineParameters.AdminConsensusAdmin, adminConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, MultichainUtilParameters.CommandlineParameters.AdminConsensusCreate, createConsensus);
            parameters += " " + string.Format(CultureInfo.InvariantCulture, MultichainUtilParameters.CommandlineParameters.SetupFirstBlocks, firstBlocks);

            command += " " + parameters;

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.");
        }

        public string CloneChain(string chainName, string cloneName)
        {
            // Alternatively, to create a new blockchain based on the parameters of an existing chain [old-name], run:
            var command = ChainLocation + UtilName + " clone " + chainName + " " + cloneName;

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.")
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
            ;
        }

        public string StartClientProcess()
        {
            // To create a new blockchain called [chain-name] based on MultiChain’s default blockchain parameters, run:
            // parameters kunnen worden gewijzigd. -> zie 
            var command = ChainLocation + ClientName + " " + Chainname + " -deamon";

            return ExecuteCommand(command, "Blockchain parameter set was successfully generated.")
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
            ;
        }

        public string GrantPermisions(string address, MultichainClientCommands.GrantPermissions[] permissions, string comment = null, string commentTo = null, string nativeAmount = null)
        {
            var permissionstring = string.Join(",", permissions.Select(f => f.ToString()).ToArray());

            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + MultichainClientCommands.Grant;
            var command =
                string.Format(
                    commandtext,
                    address,
                    permissionstring,
                    string.Empty,
                    string.Empty,
                    nativeAmount,
                    this.GetValidStringdata(comment),
                    this.GetValidStringdata(commentTo)).Trim();

            return ExecuteCommand(command, null).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string RevokePermisions(string address, MultichainClientCommands.GrantPermissions[] permissions, string comment = null, string commentTo = null, string nativeAmount = null)
        {
            var permissionstring = string.Join(",", permissions.Select(f => f.ToString()).ToArray());

            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + MultichainClientCommands.Revoke;
            var command =
                string.Format(
                    commandtext,
                    address,
                    permissionstring,
                    string.Empty,
                    string.Empty,
                    nativeAmount,
                    this.GetValidStringdata(comment),
                    this.GetValidStringdata(commentTo)).Trim();

            return ExecuteCommand(command, null).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public Permissions ListPermissions()
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + MultichainClientCommands.Listpermissions;
            var result = ExecuteCommand(commandtext, null);
                //.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("[", string.Empty).Replace("[", string.Empty).Replace("{", string.Empty);

            result = "{\"permissions\" : " + result + "}";
            var tmp = JsonConvert.DeserializeObject<Permissions>(result);
            return tmp;
        }

        public Streams ListStreams()
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + MultichainClientCommands.Liststreams;
            var result = ExecuteCommand(commandtext, null);

            result = "{\"streams\" : " + result + "}";
            var tmp = JsonConvert.DeserializeObject<Streams>(result);
            return tmp;
        }

        public string CreateNewAddress()
        {
            var command = ChainLocation + ClientName + " " + Chainname + " " + MultichainClientCommands.Getnewaddress;
            var info = ExecuteCommand(command, "");
            return info.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string CreateNewStream(bool isOpenStream, string streamname)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.CreateStream, "stream", streamname, isOpenStream ? "true" : "false");
            var result = ExecuteCommand(commandtext, null);

            return result;
        }

        public string PublishMessage(string key, string hexstring, string streamName)
        {
            // multichain - cli mytestchain publish test "hello world" 48656C6C6F20576F726C64210A
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Publish, streamName, "\"" + key + "\"", hexstring);
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string PublishMessage(string key, string fromAddress, string hexstring, string streamName)
        {
            // multichain - cli mytestchain publish test "hello world" 48656C6C6F20576F726C64210A
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Publishfrom, fromAddress, streamName, "\"" + key + "\"", hexstring);
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        
        public string Subscribe(string streamname)
        {
            // multichain - cli mytestchain publish test "hello world" 48656C6C6F20576F726C64210A
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Subscribe, streamname, "true");
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string UnSubscribe(string streamName)
        {
            // multichain - cli mytestchain publish test "hello world" 48656C6C6F20576F726C64210A
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Unsubscribe, streamName);
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public StreamKey GetStreamKeys(string streamname)
        {
            // multichain - cli mytestchain publish test "hello world" 48656C6C6F20576F726C64210A
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Liststreamkeys, streamname);
            var result = ExecuteCommand(commandtext, null);

            result = "{\"streamkeys\" : " + result + "}";
            var tmp = JsonConvert.DeserializeObject<StreamKey>(result);
            return tmp;
        }

        //Multichain-cli.exe mytestchain liststreamkeys pinjo
        public StreamItem GetStreamItem(string streamname, string transactionId)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Getstreamitem, streamname, transactionId);
            var result = ExecuteCommand(commandtext, null);
            var tmp = JsonConvert.DeserializeObject<StreamItem>(result);
            return tmp;
        }

        public StreamItems GetStreamItemByKey(string streamname, string key)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Liststreamkeyitems, streamname, key);
            var result = ExecuteCommand(commandtext, null);

            result = "{\"streamitems\" : " + result + "}";

            try
            {
                var tmp = JsonConvert.DeserializeObject<StreamItems>(result);
                return tmp;
            }
            catch(Exception ex)
            {
                // log4net -> do something
            }

            return null;
        }

        public string SignMessage(string privatekey, string message)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + string.Format(MultichainClientCommands.Signmessage, privatekey, "\"" + message + "\"");
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string VerifyMessage(string address, string signature, string message)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Verifymessage, address, signature, "\"" + message + "\"");
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        public string ImportPrivateKey(string privatekey)
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " "
                              + string.Format(MultichainClientCommands.Importprivkey, privatekey);
            var result = ExecuteCommand(commandtext, null);

            return result.Replace("\r", string.Empty).Replace("\n", string.Empty);
            ;
        }

        public KeyPairs CreateKeys()
        {
            var commandtext = ChainLocation + ClientName + " " + Chainname + " " + string.Format(MultichainClientCommands.Createkeypairs);
            var result = ExecuteCommand(commandtext, null);

            result = "{\"keypairs\" : " + result + "}";
            var tmp = JsonConvert.DeserializeObject<KeyPairs>(result);

            return tmp;
        }

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

        public T DeserializeFile<T>(string fileName, Func<string, T> customDeserialization = null)
        {
            string resourceName = fileName;

            using (StreamReader reader = new StreamReader(resourceName))
            {
                string json = reader.ReadToEnd();
                return customDeserialization == null ? JsonConvert.DeserializeObject<T>(json) : customDeserialization(json);
            }

        }

        public string GetValidStringdata(string data)
        {
            if (data == null)
            {
                return string.Empty;
            }

            return "\"" + data + "\"";
        }

        public string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }

                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }

        #endregion Public Methods
    }
}
