namespace RBAUtils
{
    partial class Application
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTerminalTime = new System.Windows.Forms.Label();
            this.txtTerminalTimeStamp = new System.Windows.Forms.TextBox();
            this.lblRebootTime = new System.Windows.Forms.Label();
            this.txtTerminalRebootTime = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblWaitforDevice = new System.Windows.Forms.Label();
            this.lblDeviceReboot = new System.Windows.Forms.Label();
            this.lblSearchingforDevice = new System.Windows.Forms.Label();
            this.pictureWaitDisplay = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblWarning1 = new System.Windows.Forms.Label();
            this.lblMessage3 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblTerminalModel = new System.Windows.Forms.Label();
            this.txtTerminalModel = new System.Windows.Forms.TextBox();
            this.lblTerminalSN = new System.Windows.Forms.Label();
            this.txtTerminalSN = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWaitDisplay)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(50, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(662, 350);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OnDeselectingMainTabPage);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(654, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "24REBOOT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTerminalSN);
            this.panel1.Controls.Add(this.txtTerminalSN);
            this.panel1.Controls.Add(this.lblTerminalModel);
            this.panel1.Controls.Add(this.txtTerminalModel);
            this.panel1.Controls.Add(this.lblTerminalTime);
            this.panel1.Controls.Add(this.txtTerminalTimeStamp);
            this.panel1.Controls.Add(this.lblRebootTime);
            this.panel1.Controls.Add(this.txtTerminalRebootTime);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 91);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // lblTerminalTime
            // 
            this.lblTerminalTime.AutoSize = true;
            this.lblTerminalTime.Location = new System.Drawing.Point(329, 26);
            this.lblTerminalTime.Name = "lblTerminalTime";
            this.lblTerminalTime.Size = new System.Drawing.Size(94, 13);
            this.lblTerminalTime.TabIndex = 0;
            this.lblTerminalTime.Text = "TERMINAL TIME:";
            // 
            // txtTerminalTimeStamp
            // 
            this.txtTerminalTimeStamp.Location = new System.Drawing.Point(434, 25);
            this.txtTerminalTimeStamp.MaxLength = 4;
            this.txtTerminalTimeStamp.Name = "txtTerminalTimeStamp";
            this.txtTerminalTimeStamp.ReadOnly = true;
            this.txtTerminalTimeStamp.Size = new System.Drawing.Size(107, 20);
            this.txtTerminalTimeStamp.TabIndex = 1;
            this.txtTerminalTimeStamp.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // lblRebootTime
            // 
            this.lblRebootTime.AutoSize = true;
            this.lblRebootTime.Location = new System.Drawing.Point(329, 59);
            this.lblRebootTime.Name = "lblRebootTime";
            this.lblRebootTime.Size = new System.Drawing.Size(84, 13);
            this.lblRebootTime.TabIndex = 2;
            this.lblRebootTime.Text = "REBOOT TIME:";
            // 
            // txtTerminalRebootTime
            // 
            this.txtTerminalRebootTime.Location = new System.Drawing.Point(435, 56);
            this.txtTerminalRebootTime.MaxLength = 4;
            this.txtTerminalRebootTime.Name = "txtTerminalRebootTime";
            this.txtTerminalRebootTime.Size = new System.Drawing.Size(107, 20);
            this.txtTerminalRebootTime.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(557, 59);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblWaitforDevice);
            this.panel2.Controls.Add(this.lblDeviceReboot);
            this.panel2.Controls.Add(this.lblSearchingforDevice);
            this.panel2.Controls.Add(this.pictureWaitDisplay);
            this.panel2.Location = new System.Drawing.Point(-3, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(655, 229);
            this.panel2.TabIndex = 9;
            // 
            // lblWaitforDevice
            // 
            this.lblWaitforDevice.AutoSize = true;
            this.lblWaitforDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitforDevice.Location = new System.Drawing.Point(3, 37);
            this.lblWaitforDevice.Name = "lblWaitforDevice";
            this.lblWaitforDevice.Size = new System.Drawing.Size(652, 42);
            this.lblWaitforDevice.TabIndex = 8;
            this.lblWaitforDevice.Text = "WAIT FOR DEVICE TO COMPLETE";
            this.lblWaitforDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeviceReboot
            // 
            this.lblDeviceReboot.AutoSize = true;
            this.lblDeviceReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeviceReboot.Location = new System.Drawing.Point(229, 161);
            this.lblDeviceReboot.Name = "lblDeviceReboot";
            this.lblDeviceReboot.Size = new System.Drawing.Size(182, 42);
            this.lblDeviceReboot.TabIndex = 9;
            this.lblDeviceReboot.Text = "REBOOT";
            this.lblDeviceReboot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSearchingforDevice
            // 
            this.lblSearchingforDevice.AutoSize = true;
            this.lblSearchingforDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchingforDevice.Location = new System.Drawing.Point(98, 161);
            this.lblSearchingforDevice.Name = "lblSearchingforDevice";
            this.lblSearchingforDevice.Size = new System.Drawing.Size(496, 42);
            this.lblSearchingforDevice.TabIndex = 5;
            this.lblSearchingforDevice.Text = "SEARCHING FOR DEVICE";
            this.lblSearchingforDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSearchingforDevice.Visible = false;
            // 
            // pictureWaitDisplay
            // 
            this.pictureWaitDisplay.Image = ((System.Drawing.Image)(resources.GetObject("pictureWaitDisplay.Image")));
            this.pictureWaitDisplay.Location = new System.Drawing.Point(2, 0);
            this.pictureWaitDisplay.Name = "pictureWaitDisplay";
            this.pictureWaitDisplay.Size = new System.Drawing.Size(651, 227);
            this.pictureWaitDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureWaitDisplay.TabIndex = 6;
            this.pictureWaitDisplay.TabStop = false;
            this.pictureWaitDisplay.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(654, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.lblWarning1);
            this.groupBox1.Controls.Add(this.lblMessage3);
            this.groupBox1.Controls.Add(this.lblMessage2);
            this.groupBox1.Controls.Add(this.lblMessage1);
            this.groupBox1.Location = new System.Drawing.Point(-4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblWarning1
            // 
            this.lblWarning1.AutoSize = true;
            this.lblWarning1.Font = new System.Drawing.Font("Showcard Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning1.Location = new System.Drawing.Point(7, 49);
            this.lblWarning1.Name = "lblWarning1";
            this.lblWarning1.Size = new System.Drawing.Size(35, 44);
            this.lblWarning1.TabIndex = 3;
            this.lblWarning1.Text = "!";
            // 
            // lblMessage3
            // 
            this.lblMessage3.AutoSize = true;
            this.lblMessage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage3.Location = new System.Drawing.Point(208, 192);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(216, 39);
            this.lblMessage3.TabIndex = 2;
            this.lblMessage3.Text = "AND RETRY";
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.Location = new System.Drawing.Point(83, 122);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(466, 39);
            this.lblMessage2.TabIndex = 1;
            this.lblMessage2.Text = "CHECK FOR RUNNING DAL";
            // 
            // lblMessage1
            // 
            this.lblMessage1.AutoSize = true;
            this.lblMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage1.Location = new System.Drawing.Point(50, 52);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(577, 39);
            this.lblMessage1.TabIndex = 0;
            this.lblMessage1.Text = "ERROR CONNECTING TO DEVICE";
            // 
            // lblTerminalModel
            // 
            this.lblTerminalModel.AutoSize = true;
            this.lblTerminalModel.Location = new System.Drawing.Point(42, 25);
            this.lblTerminalModel.Name = "lblTerminalModel";
            this.lblTerminalModel.Size = new System.Drawing.Size(106, 13);
            this.lblTerminalModel.TabIndex = 5;
            this.lblTerminalModel.Text = "TERMINAL MODEL:";
            // 
            // txtTerminalModel
            // 
            this.txtTerminalModel.Location = new System.Drawing.Point(147, 24);
            this.txtTerminalModel.MaxLength = 4;
            this.txtTerminalModel.Name = "txtTerminalModel";
            this.txtTerminalModel.ReadOnly = true;
            this.txtTerminalModel.Size = new System.Drawing.Size(107, 20);
            this.txtTerminalModel.TabIndex = 6;
            // 
            // lblTerminalSN
            // 
            this.lblTerminalSN.AutoSize = true;
            this.lblTerminalSN.Location = new System.Drawing.Point(42, 56);
            this.lblTerminalSN.Name = "lblTerminalSN";
            this.lblTerminalSN.Size = new System.Drawing.Size(83, 13);
            this.lblTerminalSN.TabIndex = 7;
            this.lblTerminalSN.Text = "TERMINAL SN:";
            // 
            // txtTerminalSN
            // 
            this.txtTerminalSN.Location = new System.Drawing.Point(147, 55);
            this.txtTerminalSN.MaxLength = 4;
            this.txtTerminalSN.Name = "txtTerminalSN";
            this.txtTerminalSN.ReadOnly = true;
            this.txtTerminalSN.Size = new System.Drawing.Size(107, 20);
            this.txtTerminalSN.TabIndex = 8;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Application";
            this.Text = "Application";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWaitDisplay)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        // TABPAGE: 1
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTerminalTimeStamp;
        private System.Windows.Forms.TextBox txtTerminalRebootTime;
        private System.Windows.Forms.Label lblTerminalTime;
        private System.Windows.Forms.Label lblSearchingforDevice;
        private System.Windows.Forms.Label lblRebootTime;
        private System.Windows.Forms.PictureBox pictureWaitDisplay;
        // TABPAGE: 2
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWaitforDevice;
        private System.Windows.Forms.Label lblDeviceReboot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.Label lblMessage3;
        private System.Windows.Forms.Label lblWarning1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTerminalSN;
        private System.Windows.Forms.TextBox txtTerminalSN;
        private System.Windows.Forms.Label lblTerminalModel;
        private System.Windows.Forms.TextBox txtTerminalModel;
    }
}

