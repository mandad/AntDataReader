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
        public delegate void StatusCallback();
        public StatusCallback statusCallback;
        delegate void UpdateLabel(string newText);
        UpdateLabel updateLabel;
        delegate void DisplayText(string toAdd);
        DisplayText displayText;
        bool openedOnce = false;

        public frmDisplay()
        {
            InitializeComponent();
            updateLabel = new UpdateLabel(this.UpdateLabelFunction);
            displayText = new DisplayText(this.UpdateDisplayTextFunction);

            string[] serialPorts = System.IO.Ports.SerialPort.GetPortNames();
            cmbPort.Items.AddRange(serialPorts);
            cmbPort.SelectedIndex = 0;
            serialPort.PortName = cmbPort.Text;
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
                    btnOpenCom.Text = "Close COM";
                    antComm = new ANTCommunication(ref serialPort, this);
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
                    btnOpenCom.Text = "Open COM";
                    antComm = null;
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (antComm != null)
            {
                if (antComm.ChannelOpen)
                {   //close the channel
                    antComm.CloseChannel();
                }
                else
                {   //open the channel
                    if (!openedOnce)
                    {
                        antComm.InitState = 0;  //reset the initalization sequence to the beginning
                        antComm.InitializeAntSyncronous();
                        statusCallback = new StatusCallback(this.CheckOpen);
                        //asyncTimer.Start();
                    }
                    else    //after first initialization, it only needs to be opened
                    {
                        antComm.OpenChannel();
                        //asyncTimer.Start();
                    }
                }
            }

        }

        private void CheckOpen()
        {
            if (antComm.ChannelOpen)
            {
                //asyncTimer.Stop();
                object[] pass = new object[1];
                pass[0] = "Open";
                this.Invoke(this.updateLabel, pass);
                openedOnce = true;
            }
            else
            {
                //asyncTimer.Stop();
                object[] pass = new object[1];
                pass[0] = "Closed";
                this.Invoke(this.updateLabel, pass);
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
                        antComm.callFunc(); //call the async data recieved function
                    }
                    else
                    {
                        DecodeResponse(readData[4], readData[5]);
                    }
                }
                else if (readData[2] == 0x4E) //broadcast data
                {
                    string fullText = "";
                    for (int i = 3; i < readData.Length - 2; i++)
                    {
                        fullText += (char)readData[i];
                    }
                    RemoteDisplayUpdate(fullText);
                    //MessageBox.Show("Data Received");
                }
                else
                {
                    RemoteDisplayUpdate("Message Id: " + readData[2].ToString("X"));   
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId">The message id from the response</param>
        /// <param name="messageCode">The message code from the response</param>
        private void DecodeResponse(byte messageId, byte messageCode)
        {
            string displayMessage;
            if (messageId == 1)
            {
                displayMessage = "RF event";
            }
            else
            {
                displayMessage = "Message ID: " + messageId.ToString();
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
                case 2:
                    displayMessage += "Message Code: EVENT_RX_FAIL";
                    break;
                default:
                    displayMessage += "Message Code: " + messageCode.ToString("x");
                    break;
            }
            RemoteDisplayUpdate(displayMessage);
            //MessageBox.Show(displayMessage, "Message Recieved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void asyncTimer_Tick(object sender, EventArgs e)
        {
            asyncTimer.Stop();
            if (statusCallback != null)
            {
                statusCallback();
            }
        }

        #region UI Delegates

        private void UpdateLabelFunction(string newText)
        {
                lblChannelStatus.Text = newText;
                //Switch the button
                if (newText == "Open")
                {
                    btnOpenChannel.Text = "Close Channel";
                }
                else
                {
                    btnOpenChannel.Text = "Open Channel";
                }
        }

        private void UpdateDisplayTextFunction(string toAdd)
        {
            txtDisplay.Text += toAdd;
        }

        #endregion

        private void cmbPort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.PortName = cmbPort.SelectedItem.ToString();
            }
        }

        private void RemoteDisplayUpdate(string addText)
        {
            object[] pass = new object[1];
            pass[0] = addText + "\r\n";
            this.Invoke(displayText, pass);
        }

        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
        }
    }
}
