namespace TestApplication
{
    partial class PhysicianInterface
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.patientView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ToevoegingInStream = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Streams = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.bsnnumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.country = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.city = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.housenumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.streetname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.initials = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.firstname = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.docId = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create patient";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patient informatie:";
            // 
            // patientView
            // 
            this.patientView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.patientView.FullRowSelect = true;
            this.patientView.Location = new System.Drawing.Point(13, 29);
            this.patientView.Name = "patientView";
            this.patientView.Size = new System.Drawing.Size(232, 577);
            this.patientView.TabIndex = 1;
            this.patientView.UseCompatibleStateImageBehavior = false;
            this.patientView.View = System.Windows.Forms.View.Details;
            this.patientView.SelectedIndexChanged += new System.EventHandler(this.patientView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Voornaam";
            this.columnHeader1.Width = 69;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Achternaam";
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Adres";
            this.columnHeader3.Width = 69;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(251, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 287);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.ToevoegingInStream);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.Streams);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(615, 261);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mijn toevoegingen";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(345, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "To physician";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(418, 210);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(191, 20);
            this.textBox2.TabIndex = 8;
            // 
            // ToevoegingInStream
            // 
            this.ToevoegingInStream.Location = new System.Drawing.Point(345, 3);
            this.ToevoegingInStream.Multiline = true;
            this.ToevoegingInStream.Name = "ToevoegingInStream";
            this.ToevoegingInStream.Size = new System.Drawing.Size(264, 201);
            this.ToevoegingInStream.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(534, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Post";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Streams
            // 
            this.Streams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.Streams.FullRowSelect = true;
            this.Streams.Location = new System.Drawing.Point(14, 3);
            this.Streams.Name = "Streams";
            this.Streams.Size = new System.Drawing.Size(325, 252);
            this.Streams.TabIndex = 5;
            this.Streams.UseCompatibleStateImageBehavior = false;
            this.Streams.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Datum";
            this.columnHeader10.Width = 78;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Dokter";
            this.columnHeader11.Width = 94;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Aantal toevoegingen";
            this.columnHeader12.Width = 113;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(615, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mijn communicatie";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(615, 261);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mijn verzoeken";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Location = new System.Drawing.Point(12, 12);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(899, 644);
            this.tabControl2.TabIndex = 4;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.listView1);
            this.tabPage7.Controls.Add(this.label1);
            this.tabPage7.Controls.Add(this.tabControl1);
            this.tabPage7.Controls.Add(this.patientView);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(891, 618);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "Physician";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.tabPage7.Click += new System.EventHandler(this.tabPage7_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(251, 322);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(619, 284);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Date / time";
            this.columnHeader7.Width = 78;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Data type";
            this.columnHeader8.Width = 78;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Content";
            this.columnHeader9.Width = 439;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.label8);
            this.tabPage8.Controls.Add(this.bsnnumber);
            this.tabPage8.Controls.Add(this.label9);
            this.tabPage8.Controls.Add(this.country);
            this.tabPage8.Controls.Add(this.label5);
            this.tabPage8.Controls.Add(this.city);
            this.tabPage8.Controls.Add(this.label6);
            this.tabPage8.Controls.Add(this.housenumber);
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Controls.Add(this.streetname);
            this.tabPage8.Controls.Add(this.label4);
            this.tabPage8.Controls.Add(this.initials);
            this.tabPage8.Controls.Add(this.label3);
            this.tabPage8.Controls.Add(this.lastname);
            this.tabPage8.Controls.Add(this.label2);
            this.tabPage8.Controls.Add(this.firstname);
            this.tabPage8.Controls.Add(this.button1);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(891, 618);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Patient";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "BSN Number";
            // 
            // bsnnumber
            // 
            this.bsnnumber.Location = new System.Drawing.Point(91, 201);
            this.bsnnumber.Name = "bsnnumber";
            this.bsnnumber.Size = new System.Drawing.Size(196, 20);
            this.bsnnumber.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Country";
            // 
            // country
            // 
            this.country.Location = new System.Drawing.Point(91, 175);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(196, 20);
            this.country.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "City";
            // 
            // city
            // 
            this.city.Location = new System.Drawing.Point(91, 149);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(196, 20);
            this.city.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Housenumber";
            // 
            // housenumber
            // 
            this.housenumber.Location = new System.Drawing.Point(91, 123);
            this.housenumber.Name = "housenumber";
            this.housenumber.Size = new System.Drawing.Size(196, 20);
            this.housenumber.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Streetname";
            // 
            // streetname
            // 
            this.streetname.Location = new System.Drawing.Point(91, 97);
            this.streetname.Name = "streetname";
            this.streetname.Size = new System.Drawing.Size(196, 20);
            this.streetname.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Initials";
            // 
            // initials
            // 
            this.initials.Location = new System.Drawing.Point(91, 71);
            this.initials.Name = "initials";
            this.initials.Size = new System.Drawing.Size(196, 20);
            this.initials.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lastname";
            // 
            // lastname
            // 
            this.lastname.Location = new System.Drawing.Point(91, 45);
            this.lastname.Name = "lastname";
            this.lastname.Size = new System.Drawing.Size(196, 20);
            this.lastname.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Firstname";
            // 
            // firstname
            // 
            this.firstname.Location = new System.Drawing.Point(91, 19);
            this.firstname.Name = "firstname";
            this.firstname.Size = new System.Drawing.Size(196, 20);
            this.firstname.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(793, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Dokter Id: ";
            // 
            // docId
            // 
            this.docId.AutoSize = true;
            this.docId.Location = new System.Drawing.Point(856, 9);
            this.docId.Name = "docId";
            this.docId.Size = new System.Drawing.Size(55, 13);
            this.docId.TabIndex = 6;
            this.docId.Text = "12456789";
            // 
            // PhysicianInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 663);
            this.Controls.Add(this.docId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tabControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhysicianInterface";
            this.Text = "PhysicianInterface";
            this.Load += new System.EventHandler(this.PhysicianInterface_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView patientView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox city;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox housenumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox streetname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox initials;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox firstname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bsnnumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox country;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ListView Streams;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox ToevoegingInStream;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label docId;
    }
}