namespace AntDataReader
{
    partial class frmDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.serialPort = new System.IO.Ports.SerialPort();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.btnOpenChannel = new System.Windows.Forms.Button();
            this.lblComStatus = new System.Windows.Forms.Label();
            this.lblChannelStatus = new System.Windows.Forms.Label();
            this.asyncTimer = new System.Windows.Forms.Timer();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnClearDisplay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 4800;
            this.serialPort.PortName = "COM5";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(12, 12);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(82, 23);
            this.btnOpenCom.TabIndex = 0;
            this.btnOpenCom.Text = "Open COM";
            this.btnOpenCom.UseVisualStyleBackColor = true;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // btnOpenChannel
            // 
            this.btnOpenChannel.Location = new System.Drawing.Point(145, 12);
            this.btnOpenChannel.Name = "btnOpenChannel";
            this.btnOpenChannel.Size = new System.Drawing.Size(89, 23);
            this.btnOpenChannel.TabIndex = 1;
            this.btnOpenChannel.Text = "Open Channel";
            this.btnOpenChannel.UseVisualStyleBackColor = true;
            this.btnOpenChannel.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblComStatus
            // 
            this.lblComStatus.AutoSize = true;
            this.lblComStatus.Location = new System.Drawing.Point(30, 41);
            this.lblComStatus.Name = "lblComStatus";
            this.lblComStatus.Size = new System.Drawing.Size(39, 13);
            this.lblComStatus.TabIndex = 2;
            this.lblComStatus.Text = "Closed";
            // 
            // lblChannelStatus
            // 
            this.lblChannelStatus.AutoSize = true;
            this.lblChannelStatus.Location = new System.Drawing.Point(169, 38);
            this.lblChannelStatus.Name = "lblChannelStatus";
            this.lblChannelStatus.Size = new System.Drawing.Size(39, 13);
            this.lblChannelStatus.TabIndex = 3;
            this.lblChannelStatus.Text = "Closed";
            this.lblChannelStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // asyncTimer
            // 
            this.asyncTimer.Interval = 5000;
            this.asyncTimer.Tick += new System.EventHandler(this.asyncTimer_Tick);
            // 
            // cmbPort
            // 
            this.cmbPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPort.Location = new System.Drawing.Point(12, 68);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(98, 21);
            this.cmbPort.TabIndex = 4;
            this.cmbPort.SelectionChangeCommitted += new System.EventHandler(this.cmbPort_SelectionChangeCommitted);
            // 
            // txtDisplay
            // 
            this.txtDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplay.Location = new System.Drawing.Point(12, 108);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(359, 249);
            this.txtDisplay.TabIndex = 5;
            // 
            // btnClearDisplay
            // 
            this.btnClearDisplay.Location = new System.Drawing.Point(289, 79);
            this.btnClearDisplay.Name = "btnClearDisplay";
            this.btnClearDisplay.Size = new System.Drawing.Size(82, 23);
            this.btnClearDisplay.TabIndex = 6;
            this.btnClearDisplay.Text = "Clear Display";
            this.btnClearDisplay.UseVisualStyleBackColor = true;
            this.btnClearDisplay.Click += new System.EventHandler(this.btnClearDisplay_Click);
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 369);
            this.Controls.Add(this.btnClearDisplay);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.lblChannelStatus);
            this.Controls.Add(this.lblComStatus);
            this.Controls.Add(this.btnOpenChannel);
            this.Controls.Add(this.btnOpenCom);
            this.Name = "frmDisplay";
            this.Text = "ANT Data Reciever";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDisplay_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.Button btnOpenChannel;
        private System.Windows.Forms.Label lblComStatus;
        private System.Windows.Forms.Label lblChannelStatus;
        private System.Windows.Forms.Timer asyncTimer;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnClearDisplay;
    }
}

