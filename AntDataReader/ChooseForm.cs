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
    public partial class frmChoose : Form
    {
        public frmChoose()
        {
            InitializeComponent();
        }

        private void btnDebugLaunch_Click(object sender, EventArgs e)
        {
            Form launch = new frmDisplay(this);
            launch.Show();
            this.Hide();
        }

        private void btnGUILaunch_Click(object sender, EventArgs e)
        {
            //Application.Run(new frmTeslaGui(this));
            Form launch = new frmTeslaGui(this);
            launch.Show();
            this.Hide();
        }
    }
}
