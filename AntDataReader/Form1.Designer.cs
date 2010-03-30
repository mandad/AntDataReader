﻿namespace AntDataReader
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
            this.btnTest = new System.Windows.Forms.Button();
            this.lblComStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 4800;
            this.serialPort.Handshake = System.IO.Ports.Handshake.RequestToSend;
            this.serialPort.PortName = "COM5";
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(12, 12);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(82, 23);
            this.btnOpenCom.TabIndex = 0;
            this.btnOpenCom.Text = "Open COM5";
            this.btnOpenCom.UseVisualStyleBackColor = true;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(117, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test Data";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
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
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 306);
            this.Controls.Add(this.lblComStatus);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnOpenCom);
            this.Name = "frmDisplay";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblComStatus;
    }
}
