namespace AntDataReader
{
    partial class frmChoose
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
            this.btnDebugLaunch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGUILaunch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDebugLaunch
            // 
            this.btnDebugLaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebugLaunch.Location = new System.Drawing.Point(12, 51);
            this.btnDebugLaunch.Name = "btnDebugLaunch";
            this.btnDebugLaunch.Size = new System.Drawing.Size(173, 63);
            this.btnDebugLaunch.TabIndex = 0;
            this.btnDebugLaunch.Text = "Debug Interface";
            this.btnDebugLaunch.UseVisualStyleBackColor = true;
            this.btnDebugLaunch.Click += new System.EventHandler(this.btnDebugLaunch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose A Program";
            // 
            // btnGUILaunch
            // 
            this.btnGUILaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGUILaunch.Location = new System.Drawing.Point(12, 135);
            this.btnGUILaunch.Name = "btnGUILaunch";
            this.btnGUILaunch.Size = new System.Drawing.Size(173, 63);
            this.btnGUILaunch.TabIndex = 2;
            this.btnGUILaunch.Text = "GUI";
            this.btnGUILaunch.UseVisualStyleBackColor = true;
            this.btnGUILaunch.Click += new System.EventHandler(this.btnGUILaunch_Click);
            // 
            // frmChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 215);
            this.Controls.Add(this.btnGUILaunch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDebugLaunch);
            this.Name = "frmChoose";
            this.Text = "ChooseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDebugLaunch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGUILaunch;
    }
}