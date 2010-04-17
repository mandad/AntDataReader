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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplay));
            this.serialPort = new System.IO.Ports.SerialPort();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.btnOpenChannel = new System.Windows.Forms.Button();
            this.lblComStatus = new System.Windows.Forms.Label();
            this.lblChannelStatus = new System.Windows.Forms.Label();
            this.asyncTimer = new System.Windows.Forms.Timer();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnClearDisplay = new System.Windows.Forms.Button();
            this.btnScanMode = new System.Windows.Forms.Button();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAscii = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbDebugMode = new System.Windows.Forms.CheckBox();
            this.cbAutoClear = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 57600;
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
            this.btnOpenChannel.Location = new System.Drawing.Point(123, 12);
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
            this.lblChannelStatus.Location = new System.Drawing.Point(206, 41);
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
            this.cmbPort.Location = new System.Drawing.Point(62, 68);
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
            this.txtDisplay.Location = new System.Drawing.Point(12, 131);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(475, 321);
            this.txtDisplay.TabIndex = 5;
            // 
            // btnClearDisplay
            // 
            this.btnClearDisplay.Location = new System.Drawing.Point(405, 102);
            this.btnClearDisplay.Name = "btnClearDisplay";
            this.btnClearDisplay.Size = new System.Drawing.Size(82, 23);
            this.btnClearDisplay.TabIndex = 6;
            this.btnClearDisplay.Text = "Clear Display";
            this.btnClearDisplay.UseVisualStyleBackColor = true;
            this.btnClearDisplay.Click += new System.EventHandler(this.btnClearDisplay_Click);
            // 
            // btnScanMode
            // 
            this.btnScanMode.Location = new System.Drawing.Point(241, 12);
            this.btnScanMode.Name = "btnScanMode";
            this.btnScanMode.Size = new System.Drawing.Size(130, 23);
            this.btnScanMode.TabIndex = 7;
            this.btnScanMode.Text = "Open RX Scan Mode";
            this.btnScanMode.UseVisualStyleBackColor = true;
            this.btnScanMode.Click += new System.EventHandler(this.btnScanMode_Click);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "4800",
            "57600"});
            this.cmbBaudRate.Location = new System.Drawing.Point(246, 68);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(105, 21);
            this.cmbBaudRate.TabIndex = 8;
            this.cmbBaudRate.SelectionChangeCommitted += new System.EventHandler(this.cmbBaudRate_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Baud Rate";
            // 
            // cbAscii
            // 
            this.cbAscii.AutoSize = true;
            this.cbAscii.Location = new System.Drawing.Point(14, 108);
            this.cbAscii.Name = "cbAscii";
            this.cbAscii.Size = new System.Drawing.Size(90, 17);
            this.cbAscii.TabIndex = 11;
            this.cbAscii.Text = "ASCII Display";
            this.cbAscii.ThreeState = true;
            this.cbAscii.UseVisualStyleBackColor = true;
            this.cbAscii.CheckStateChanged += new System.EventHandler(this.cbAscii_CheckStateChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(392, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset ANT";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbDebugMode
            // 
            this.cbDebugMode.AutoSize = true;
            this.cbDebugMode.Checked = true;
            this.cbDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDebugMode.Location = new System.Drawing.Point(110, 108);
            this.cbDebugMode.Name = "cbDebugMode";
            this.cbDebugMode.Size = new System.Drawing.Size(88, 17);
            this.cbDebugMode.TabIndex = 13;
            this.cbDebugMode.Text = "Debug Mode";
            this.cbDebugMode.UseVisualStyleBackColor = true;
            this.cbDebugMode.CheckedChanged += new System.EventHandler(this.cbDebugMode_CheckedChanged);
            // 
            // cbAutoClear
            // 
            this.cbAutoClear.AutoSize = true;
            this.cbAutoClear.Location = new System.Drawing.Point(324, 106);
            this.cbAutoClear.Name = "cbAutoClear";
            this.cbAutoClear.Size = new System.Drawing.Size(75, 17);
            this.cbAutoClear.TabIndex = 14;
            this.cbAutoClear.Text = "Auto Clear";
            this.cbAutoClear.UseVisualStyleBackColor = true;
            this.cbAutoClear.CheckedChanged += new System.EventHandler(this.cbAutoClear_CheckedChanged);
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 464);
            this.Controls.Add(this.cbAutoClear);
            this.Controls.Add(this.cbDebugMode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbAscii);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.btnScanMode);
            this.Controls.Add(this.btnClearDisplay);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.lblChannelStatus);
            this.Controls.Add(this.lblComStatus);
            this.Controls.Add(this.btnOpenChannel);
            this.Controls.Add(this.btnOpenCom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDisplay";
            this.Text = "ANT Data Reciever";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDisplay_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDisplay_FormClosed);
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
        private System.Windows.Forms.Button btnScanMode;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAscii;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox cbDebugMode;
        private System.Windows.Forms.CheckBox cbAutoClear;
    }
}

