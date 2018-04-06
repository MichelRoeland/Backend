namespace TestApplication
{
    partial class SecondClient
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Clients = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.TextBox();
            this.privkey = new System.Windows.Forms.TextBox();
            this.pubkey = new System.Windows.Forms.TextBox();
            this.adres = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.enc = new System.Windows.Forms.TextBox();
            this.aantalberichten = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Clients
            // 
            this.Clients.FormattingEnabled = true;
            this.Clients.Location = new System.Drawing.Point(89, 12);
            this.Clients.Name = "Clients";
            this.Clients.Size = new System.Drawing.Size(304, 21);
            this.Clients.TabIndex = 87;
            this.Clients.SelectedIndexChanged += new System.EventHandler(this.Clients_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Stream:";
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(387, 19);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(313, 124);
            this.message.TabIndex = 94;
            this.message.Text = "Dit is een bericht die we gaan inschieten via een blockchain transactie voor de s" +
    "econd client naar de local node. :) :) :)";
            // 
            // privkey
            // 
            this.privkey.Location = new System.Drawing.Point(77, 71);
            this.privkey.Name = "privkey";
            this.privkey.Size = new System.Drawing.Size(304, 20);
            this.privkey.TabIndex = 93;
            // 
            // pubkey
            // 
            this.pubkey.Location = new System.Drawing.Point(77, 45);
            this.pubkey.Name = "pubkey";
            this.pubkey.Size = new System.Drawing.Size(304, 20);
            this.pubkey.TabIndex = 92;
            // 
            // adres
            // 
            this.adres.Location = new System.Drawing.Point(77, 19);
            this.adres.Name = "adres";
            this.adres.Size = new System.Drawing.Size(304, 20);
            this.adres.TabIndex = 91;
            // 
            // button15
            // 
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button15.Location = new System.Drawing.Point(387, 149);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(155, 20);
            this.button15.TabIndex = 90;
            this.button15.Text = "Sign Message";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button14.Location = new System.Drawing.Point(545, 149);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(155, 20);
            this.button14.TabIndex = 89;
            this.button14.Text = "Publish message";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 97;
            this.label4.Text = "Private key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "Public key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 95;
            this.label2.Text = "Address";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button14);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button15);
            this.groupBox1.Controls.Add(this.message);
            this.groupBox1.Controls.Add(this.adres);
            this.groupBox1.Controls.Add(this.privkey);
            this.groupBox1.Controls.Add(this.pubkey);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 180);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verzenden";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.aantalberichten);
            this.groupBox2.Controls.Add(this.dec);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.enc);
            this.groupBox2.Location = new System.Drawing.Point(13, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 282);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ontvangen";
            // 
            // dec
            // 
            this.dec.Location = new System.Drawing.Point(6, 149);
            this.dec.Multiline = true;
            this.dec.Name = "dec";
            this.dec.Size = new System.Drawing.Size(313, 124);
            this.dec.TabIndex = 96;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 95;
            this.label5.Text = "Aantal berichten ontvangen";
            // 
            // enc
            // 
            this.enc.Location = new System.Drawing.Point(6, 19);
            this.enc.Multiline = true;
            this.enc.Name = "enc";
            this.enc.Size = new System.Drawing.Size(313, 124);
            this.enc.TabIndex = 94;
            // 
            // aantalberichten
            // 
            this.aantalberichten.AutoSize = true;
            this.aantalberichten.Location = new System.Drawing.Point(470, 20);
            this.aantalberichten.Name = "aantalberichten";
            this.aantalberichten.Size = new System.Drawing.Size(13, 13);
            this.aantalberichten.TabIndex = 98;
            this.aantalberichten.Text = "0";
            // 
            // SecondClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(739, 520);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Clients);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SecondClient";
            this.Text = "Second Client";
            this.Load += new System.EventHandler(this.FirstClient_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Clients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TextBox privkey;
        private System.Windows.Forms.TextBox pubkey;
        private System.Windows.Forms.TextBox adres;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox enc;
        private System.Windows.Forms.TextBox dec;
        private System.Windows.Forms.Label aantalberichten;
    }
}