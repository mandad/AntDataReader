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
    public partial class frmDisplay : Form
    {
        public frmDisplay()
        {
            InitializeComponent();
        }

        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (serialPort.IsOpen)
                {
                    lblComStatus.Text = "Open";
                    btnOpenCom.Text = "Close COM5";
                }
            }
            else
            {
                try
                {
                    serialPort.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (!serialPort.IsOpen)
                {
                    lblComStatus.Text = "Closed";
                    btnOpenCom.Text = "Open COM5";
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            serialPort.RtsEnable = true;
            //byte[] data;
            //data = new byte[5];
            //data[0] = 0xA4;
            //data[1] = 0x01;
            //data[2] = 0x4B;
            //data[3] = 0x01;
            //data[4] = 0xEF;  //checksum
            //serialPort.Write(data, 0, 5);
            ANTCommunication.InitializeAntSyncronous(ref serialPort);
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort.BytesToRead > 0)
            {
                byte[] readData = new byte[serialPort.BytesToRead];
                serialPort.Read(readData, 0, serialPort.BytesToRead);
                MessageBox.Show("Data Received");
            }
        }
    }
}
