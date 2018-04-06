using System.Collections;
using System.Configuration.Install;
using System.Linq;

namespace WindowsService1
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceProcessIntaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SignallerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServiceProcessIntaller
            // 
            this.ServiceProcessIntaller.Password = null;
            this.ServiceProcessIntaller.Username = null;
            // 
            // SignallerInstaller
            // 
            this.SignallerInstaller.ServiceName = "Sun Technologies SignallRService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new Installer[] {
            this.ServiceProcessIntaller,
            this.SignallerInstaller});

        }

        // Override the 'OnBeforeInstall' method.
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            string username = GetContextParameter("username").Trim();
            string password = GetContextParameter("password").Trim();

            if (username.Any())
            {
                this.ServiceProcessIntaller.Account = System.ServiceProcess.ServiceAccount.User;
                this.ServiceProcessIntaller.Password = username;
                this.ServiceProcessIntaller.Username = password;
            }

            this.ServiceProcessIntaller.AfterInstall += this.ServiceProcessIntallerAfterInstall;
        }

        public string GetContextParameter(string key)
        {
            string sValue = "";
            try
            {
                sValue = this.Context.Parameters[key].ToString();
            }
            catch
            {
                sValue = "";
            }
            return sValue;
        }
        private void ServiceProcessIntallerAfterInstall(object sender, InstallEventArgs e)
        {
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServiceProcessIntaller;
        private System.ServiceProcess.ServiceInstaller SignallerInstaller;
    }

    
}