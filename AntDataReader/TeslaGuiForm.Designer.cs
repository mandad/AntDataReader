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
            this.dataCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDataLED = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblTempGraph = new System.Windows.Forms.Label();
            this.simulatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.dataCollectionToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(696, 24);
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
            this.COMPortToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.COMPortToolStripMenuItem.Text = "COM Port";
            // 
            // debugModeToolStripMenuItem
            // 
            this.debugModeToolStripMenuItem.CheckOnClick = true;
            this.debugModeToolStripMenuItem.Name = "debugModeToolStripMenuItem";
            this.debugModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.debugModeToolStripMenuItem.Text = "Debug Mode";
            this.debugModeToolStripMenuItem.Click += new System.EventHandler(this.debugModeToolStripMenuItem_Click);
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
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.CheckOnClick = true;
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            // 
            // lblDataLED
            // 
            this.lblDataLED.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataLED.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataLED.Location = new System.Drawing.Point(664, 36);
            this.lblDataLED.Name = "lblDataLED";
            this.lblDataLED.Size = new System.Drawing.Size(20, 20);
            this.lblDataLED.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temperature:";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(122, 36);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(31, 20);
            this.lblTemp.TabIndex = 3;
            this.lblTemp.Text = "XX";
            // 
            // lblTempGraph
            // 
            this.lblTempGraph.BackColor = System.Drawing.Color.LightGray;
            this.lblTempGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTempGraph.Location = new System.Drawing.Point(16, 69);
            this.lblTempGraph.Name = "lblTempGraph";
            this.lblTempGraph.Size = new System.Drawing.Size(200, 120);
            this.lblTempGraph.TabIndex = 4;
            // 
            // simulatedToolStripMenuItem
            // 
            this.simulatedToolStripMenuItem.CheckOnClick = true;
            this.simulatedToolStripMenuItem.Name = "simulatedToolStripMenuItem";
            this.simulatedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.simulatedToolStripMenuItem.Text = "Simulated";
            this.simulatedToolStripMenuItem.Click += new System.EventHandler(this.simulatedToolStripMenuItem_Click);
            // 
            // frmTeslaGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(696, 508);
            this.Controls.Add(this.lblTempGraph);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDataLED);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmTeslaGui";
            this.Text = "Sensor Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTeslaGui_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTeslaGui_FormClosed);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
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
    }
}