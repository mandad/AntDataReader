namespace AntDataReader
{
    partial class frmTeslaGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTeslaGui));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.COMPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDataLED = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblTempGraph = new System.Windows.Forms.Label();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.thrdWebSubmit = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLastMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblAccelX = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAccelY = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAccelZ = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTempF = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblButtonPress = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new AntDataReader.UserControl1();
            this.mnuMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.dataCollectionToolStripMenuItem,
            this.recordingToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(533, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.CheckOnClick = true;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.COMPortToolStripMenuItem,
            this.debugModeToolStripMenuItem,
            this.simulatedToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // COMPortToolStripMenuItem
            // 
            this.COMPortToolStripMenuItem.Name = "COMPortToolStripMenuItem";
            this.COMPortToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.COMPortToolStripMenuItem.Text = "COM Port";
            // 
            // debugModeToolStripMenuItem
            // 
            this.debugModeToolStripMenuItem.Name = "debugModeToolStripMenuItem";
            this.debugModeToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.debugModeToolStripMenuItem.Text = "Debug Mode";
            this.debugModeToolStripMenuItem.Click += new System.EventHandler(this.debugModeToolStripMenuItem_Click);
            // 
            // simulatedToolStripMenuItem
            // 
            this.simulatedToolStripMenuItem.CheckOnClick = true;
            this.simulatedToolStripMenuItem.Name = "simulatedToolStripMenuItem";
            this.simulatedToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.simulatedToolStripMenuItem.Text = "Simulated";
            this.simulatedToolStripMenuItem.Click += new System.EventHandler(this.simulatedToolStripMenuItem_Click);
            // 
            // dataCollectionToolStripMenuItem
            // 
            this.dataCollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.pauseToolStripMenuItem});
            this.dataCollectionToolStripMenuItem.Name = "dataCollectionToolStripMenuItem";
            this.dataCollectionToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.dataCollectionToolStripMenuItem.Text = "Data Collection";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.CheckOnClick = true;
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            // 
            // recordingToolStripMenuItem
            // 
            this.recordingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem,
            this.recordDataToolStripMenuItem});
            this.recordingToolStripMenuItem.Name = "recordingToolStripMenuItem";
            this.recordingToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.recordingToolStripMenuItem.Text = "Recording";
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToFileToolStripMenuItem.Text = "Save to File";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
            // 
            // recordDataToolStripMenuItem
            // 
            this.recordDataToolStripMenuItem.CheckOnClick = true;
            this.recordDataToolStripMenuItem.Name = "recordDataToolStripMenuItem";
            this.recordDataToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.recordDataToolStripMenuItem.Text = "Record Data";
            this.recordDataToolStripMenuItem.Click += new System.EventHandler(this.recordDataToolStripMenuItem_Click);
            // 
            // lblDataLED
            // 
            this.lblDataLED.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataLED.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataLED.Location = new System.Drawing.Point(501, 36);
            this.lblDataLED.Name = "lblDataLED";
            this.lblDataLED.Size = new System.Drawing.Size(20, 20);
            this.lblDataLED.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "°C:";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(66, 61);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(31, 20);
            this.lblTemp.TabIndex = 3;
            this.lblTemp.Text = "XX";
            // 
            // lblTempGraph
            // 
            this.lblTempGraph.BackColor = System.Drawing.Color.White;
            this.lblTempGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTempGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTempGraph.Location = new System.Drawing.Point(160, 31);
            this.lblTempGraph.Name = "lblTempGraph";
            this.lblTempGraph.Size = new System.Drawing.Size(300, 120);
            this.lblTempGraph.TabIndex = 4;
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.Filter = "Log Files|*.tdl";
            this.dlgSaveFile.RestoreDirectory = true;
            this.dlgSaveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgSaveFile_FileOk);
            // 
            // thrdWebSubmit
            // 
            this.thrdWebSubmit.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thrdWebSubmit_DoWork);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError,
            this.lblLastMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(92, 17);
            this.lblError.Text = "Cleared On start";
            // 
            // lblLastMessage
            // 
            this.lblLastMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblLastMessage.Name = "lblLastMessage";
            this.lblLastMessage.Size = new System.Drawing.Size(426, 17);
            this.lblLastMessage.Spring = true;
            this.lblLastMessage.Text = "Last Message Time";
            this.lblLastMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAccelX
            // 
            this.lblAccelX.AutoSize = true;
            this.lblAccelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccelX.Location = new System.Drawing.Point(43, 30);
            this.lblAccelX.Name = "lblAccelX";
            this.lblAccelX.Size = new System.Drawing.Size(31, 20);
            this.lblAccelX.TabIndex = 7;
            this.lblAccelX.Text = "XX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "X:";
            // 
            // lblAccelY
            // 
            this.lblAccelY.AutoSize = true;
            this.lblAccelY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccelY.Location = new System.Drawing.Point(43, 62);
            this.lblAccelY.Name = "lblAccelY";
            this.lblAccelY.Size = new System.Drawing.Size(31, 20);
            this.lblAccelY.TabIndex = 9;
            this.lblAccelY.Text = "XX";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Y:";
            // 
            // lblAccelZ
            // 
            this.lblAccelZ.AutoSize = true;
            this.lblAccelZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccelZ.Location = new System.Drawing.Point(43, 92);
            this.lblAccelZ.Name = "lblAccelZ";
            this.lblAccelZ.Size = new System.Drawing.Size(31, 20);
            this.lblAccelZ.TabIndex = 11;
            this.lblAccelZ.Text = "XX";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Z:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wpfHost);
            this.groupBox1.Controls.Add(this.lblAccelZ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblAccelX);
            this.groupBox1.Controls.Add(this.lblAccelY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 287);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accelerometer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblTempF);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblTemp);
            this.groupBox2.Controls.Add(this.lblTempGraph);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 168);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Temperature";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "°F:";
            // 
            // lblTempF
            // 
            this.lblTempF.AutoSize = true;
            this.lblTempF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempF.Location = new System.Drawing.Point(66, 93);
            this.lblTempF.Name = "lblTempF";
            this.lblTempF.Size = new System.Drawing.Size(31, 20);
            this.lblTempF.TabIndex = 6;
            this.lblTempF.Text = "XX";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(500, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 452);
            this.label4.TabIndex = 14;
            this.label4.Text = "T\r\nE\r\nS\r\nL\r\nA\r\n\r\nD\r\nA\r\nT\r\nA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblButtonPress);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 522);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(482, 93);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Button";
            // 
            // lblButtonPress
            // 
            this.lblButtonPress.Location = new System.Drawing.Point(91, 25);
            this.lblButtonPress.Name = "lblButtonPress";
            this.lblButtonPress.Size = new System.Drawing.Size(92, 55);
            this.lblButtonPress.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Pressed:";
            // 
            // wpfHost
            // 
            this.wpfHost.Location = new System.Drawing.Point(160, 21);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(308, 257);
            this.wpfHost.TabIndex = 13;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = this.userControl11;
            // 
            // frmTeslaGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 648);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblDataLED);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmTeslaGui";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Sensor Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTeslaGui_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTeslaGui_FormClosed);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem COMPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.Label lblDataLED;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblTempGraph;
        private System.Windows.Forms.ToolStripMenuItem simulatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ToolStripMenuItem recordDataToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker thrdWebSubmit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
        private System.Windows.Forms.Label lblAccelX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAccelY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAccelZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private UserControl1 userControl11;
        private System.Windows.Forms.ToolStripStatusLabel lblLastMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTempF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblButtonPress;
    }
}