using System;
using System.Diagnostics;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class CommandExecuter
    {
        #region Properties

        public ProcessWrapper processWrapper { get; set; }

        #endregion Properties

        #region constructor

        public CommandExecuter(ProcessWrapper processWrapper = null)
        {
            this.processWrapper = processWrapper ?? new ProcessWrapper();
        }

        #endregion constructor

        #region Public Methods

        public string ExecuteCommand(string command, string containstext)
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

        #endregion Public Methods
    }
}
