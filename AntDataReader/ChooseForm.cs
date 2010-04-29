using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntDataReader
{
    /// <summary>
    /// Presents a chooser form to the user to select either the GUI or debug interface
    /// </summary>
    public partial class frmChoose : Form
    {
        System.Timers.Timer tmrHide;
        delegate void HideSelf();
        HideSelf hider;

        /// <summary>
        /// Initializes the form and starts the GUI by default
        /// Gives choice if control is held
        /// </summary>
        public frmChoose()
        {
            InitializeComponent();
            tmrHide = new System.Timers.Timer(500);
            tmrHide.Elapsed += new System.Timers.ElapsedEventHandler(tmrHide_Elapsed);
            hider = new HideSelf(HideFunction);

            //autolaunch the GUI
            if (ModifierKeys != Keys.Control)
            {
                Form launch = new frmTeslaGui(this);
                tmrHide.Start();
                launch.Show();
            }
        }

        /// <summary>
        /// EVENT: called when the hide timer elapses, invoves the GUI function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmrHide_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmrHide.Stop();
            this.Invoke(this.hider);
        }

        /// <summary>
        /// Hides the form
        /// </summary>
        void HideFunction()
        {
            this.Hide();
        }

        /// <summary>
        /// EVENT: Called when the launch debug button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDebugLaunch_Click(object sender, EventArgs e)
        {
            Form launch = new frmDisplay(this);
            this.Hide();
            launch.Show();
        }

        /// <summary>
        /// EVENT: Called when the launch GUI button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGUILaunch_Click(object sender, EventArgs e)
        {
            //Application.Run(new frmTeslaGui(this));
            Form launch = new frmTeslaGui(this);
            this.Hide();
            launch.Show();
        }
    }
}
