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
        ANTCommunication antComm;
        delegate void StatusCallback();
        StatusCallback statusCallback;
        delegate void UpdateLabel(string newText);
        UpdateLabel updateLabel;
        bool openedOnce = false;

        public frmDisplay()
        {
            InitializeComponent();
            updateLabel = new UpdateLabel(this.UpdateLabelFunction);
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
                    antComm = new ANTCommunication(ref serialPort);
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
                    antComm = null;
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (antComm != null)
            {
                if (!openedOnce)
                {
                    antComm.InitializeAntSyncronous();
                    statusCallback = new StatusCallback(this.CheckOpen);
                    asyncTimer.Start();
                }
                else
                {
                    antComm.OpenChannel();
                    asyncTimer.Start();
                }
            }

        }

        private void CheckOpen()
        {
            if (antComm.ChannelOpen)
            {
                lblChannelStatus.Text = "Open";
                openedOnce = true;
            }
            else
            {
                asyncTimer.Start(); //check again
            }
        }


        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort.BytesToRead >= 7)
            {
                byte[] readData = new byte[serialPort.BytesToRead];
                serialPort.Read(readData, 0, serialPort.BytesToRead);

                if (readData[2] == 0x40)    //status message
                {
                    if (readData[5] == 0)
                    {
                        //success message
                        antComm.ResponseReceived = true;
                    }
                    else
                    {
                        DecodeResponse(readData[4], readData[5]);
                    }
                    //broadcast data
                }
                else if (readData[2] == 0x4E) //broadcast data
                {
                    MessageBox.Show("Data Received");
                }
            }
        }

        private void DecodeResponse(byte messageId, byte messageCode)
        {
            string displayMessage;
            if (messageId == 1)
            {
                displayMessage = "RF event\n";
            }
            else
            {
                displayMessage = "Message ID: " + messageId.ToString() + "\n";
            }
            switch (messageCode)
            {
                case 0:
                    displayMessage += "Message Code: RESPONSE_NO_ERROR";
                    break;
                case 1:
                    displayMessage += "Message Code: EVENT_RX_SEARCH_TIMEOUT";
                    //this event is raised in another thread, so we have to use invoke on a delegate
                    object[] pass = new object[1];
                    pass[0] = "Closed";
                    this.Invoke(this.updateLabel, pass);
                    break;
                default:
                    displayMessage += "Message Code: " + messageCode.ToString();
                    break;
            }
            MessageBox.Show(displayMessage, "Message Recieved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void asyncTimer_Tick(object sender, EventArgs e)
        {
            asyncTimer.Stop();
            if (statusCallback != null)
            {
                statusCallback();
            }
        }

        private void UpdateLabelFunction(string newText)
        {
            lblChannelStatus.Text = newText;
        }
    }
}
