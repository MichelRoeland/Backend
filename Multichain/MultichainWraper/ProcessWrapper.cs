using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class ProcessWrapper
    {
        #region Private Fields

        private Process process;

        #endregion Private Fields

        #region Properties

        public bool Mock { get; set; }

        public ProcessStartInfo ProcessInfo { get; set; }

        public string ReplyMessage { get; set; }

        public int ExitCode
        {
            get
            {
                if (this.process != null)
                {
                    return this.process.ExitCode;
                }

                return 0;
            }
        }

        #endregion Properties

        #region Public Methods

        public Process ProcessStart(ProcessStartInfo processInfo)
        {
            this.ProcessInfo = processInfo;

            return this.process = !this.Mock ? Process.Start(processInfo): null;
        }

        public void WaitForExit()
        {
            if (this.process != null)
            {
                this.process.WaitForExit();
            }
        }
        
        public string OutputReadToEnd()
        {
            if (this.process != null)
            {
                return this.process.StandardOutput.ReadToEnd();
            }

            return ReplyMessage;
        }

        public string ErrorReadToEnd()
        {
            if (this.process != null)
            {
                return this.process.StandardError.ReadToEnd();
            }

            return string.Empty;
        }

        public void Close()
        {
            if (this.process != null)
            {
                this.process.Close();
            }

            this.process = null;
        }

        #endregion Public Methods
    }
}
