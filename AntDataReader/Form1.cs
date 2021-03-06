﻿using System;
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
    /// The debug display form
    /// </summary>
    public partial class frmDisplay : Form, ANTDataInterpreter
    {
        ANTCommunication antComm;
        public delegate void StatusCallback();
        public StatusCallback statusCallback;
        delegate void UpdateLabel(string newText);
        UpdateLabel updateLabel;
        delegate void DisplayText(string toAdd);
        DisplayText displayText;
        bool openedOnce = false;
        BufferedReader spBuffer;
        int asciiMode = 0;
        bool debugMode = true;
        bool autoClear = false;
        frmChoose chooserForm;

        /// <summary>
        /// Initialized the debug form
        /// </summary>
        /// <param name="parent">The parent chooser form</param>
        public frmDisplay(frmChoose parent)
        {
            InitializeComponent();
            chooserForm = parent;

            updateLabel = new UpdateLabel(this.UpdateLabelFunction);
            displayText = new DisplayText(this.UpdateDisplayTextFunction);

            spBuffer = new BufferedReader(this);

            string[] serialPorts = System.IO.Ports.SerialPort.GetPortNames();
            cmbPort.Items.AddRange(serialPorts);
            cmbPort.SelectedIndex = 0;
            cmbBaudRate.SelectedIndex = 1;
            serialPort.PortName = cmbPort.Text;
        }

        /// <summary>
        /// EVENT: Called when the serial port recieves data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readData = new byte[serialPort.BytesToRead];
            serialPort.Read(readData, 0, serialPort.BytesToRead);
            spBuffer.AddNewReceived(readData);
        }

        #region UI Delegates

        /// <summary>
        /// Updates the channel status label
        /// </summary>
        /// <param name="newText">Text to set as label text</param>
        private void UpdateLabelFunction(string newText)
        {
            lblChannelStatus.Text = newText;
            //Switch the button
            if (newText == "Open")
            {
                btnOpenChannel.Text = btnScanMode.Text = "Close Channel"; 
            }
            else
            {
                btnOpenChannel.Text = "Open Channel";
                btnScanMode.Text = "Open RX Scan Mode";
            }
        }

        /// <summary>
        /// Called to update the display in a threadsafe manner
        /// </summary>
        /// <param name="addText">Text to add to the main display</param>
        private void RemoteDisplayUpdate(string addText)
        {
            if (!this.IsDisposed)
            {
                object[] pass = new object[1];
                pass[0] = addText + "\r\n";
                this.Invoke(displayText, pass);
            }
        }

        /// <summary>
        /// Updates the main display text by adding new text to the bottom
        /// </summary>
        /// <param name="toAdd">The text to add</param>
        private void UpdateDisplayTextFunction(string toAdd)
        {
            txtDisplay.Text += toAdd;
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.ScrollToCaret();
            //if (autoClear && txtDisplay.SelectionStart
        }

        #endregion

        #region UI Event Handlers

        /// <summary>
        /// EVENT: Called when the "Open COM" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// EVENT: Called when the "Open Channel" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        //statusCallback = new StatusCallback(this.CheckOpen);
                    }
                    else    //after first initialization, it only needs to be opened
                    {
                        antComm.OpenChannel();
                    }
                }
            }

        }

        /// <summary>
        /// EVENT: Called when the serial port dropdown selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.PortName = cmbPort.SelectedItem.ToString();
            }
        }

        /// <summary>
        /// EVENT: Called when "Clear Display" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDisplay_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
        }

        /// <summary>
        /// EVENT: Closes the ANT channel as the form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (antComm != null)
            {
                antComm.CloseChannel();
            }
        }

        /// <summary>
        /// EVENT: Closes the chooser form after the form has closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            chooserForm.Close();
        }

        /// <summary>
        /// EVENT: Called when "Open Rx Scan Mode" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanMode_Click(object sender, EventArgs e)
        {
            if (antComm != null)
            {
                if (antComm.ChannelOpen)
                {   //close the channel
                    antComm.CloseChannel();
                }
                else
                {   //open the channel
                    antComm.InitState = 0;  //reset the initalization sequence to the beginning
                    //statusCallback = new StatusCallback(this.CheckOpen);
                    antComm.RxScanMode();
                }
            }
        }

        /// <summary>
        /// EVENT: Called when the "Ascii Mode" checkbox changes state
        /// Sets the decoding display mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAscii_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbAscii.CheckState == CheckState.Checked)
            {
                asciiMode = 2;
            }
            else if (cbAscii.CheckState == CheckState.Indeterminate)
            {
                asciiMode = 1;
            }
            else
            {
                asciiMode = 0;
            }
        }

        /// <summary>
        /// EVENT: Called when the reset button is clicked, resets ANT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (antComm != null)
            {
                antComm.ResetANT();
                openedOnce = false;
            }
        }

        /// <summary>
        /// Called when the baud rate dropdown is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.BaudRate = Convert.ToInt32(cmbBaudRate.SelectedItem);
            }
        }

        private void cbDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            debugMode = cbDebugMode.Checked;
        }

        private void cbAutoClear_CheckedChanged(object sender, EventArgs e)
        {
            autoClear = cbAutoClear.Checked;
        }

        #endregion  //UI Event Handlers

        #region ANTDataInterpreter Interface Members

        /// <summary>
        /// This function will be called remotely when the buffer has read a full message
        /// </summary>
        public void HaveMessages()
        {
            List<byte[]> readMessages = new List<byte[]>(spBuffer.Messages);
            foreach (byte[] readData in readMessages)
            {

                if (readData[2] == 0x40)    //status message
                {
                    //0 = no error, 7 = channel closed
                    if ((readData[5] == 0) || (readData[5] == 7))
                    {
                        //success message
                        antComm.ResponseReceived = true;
                        if (antComm.callFunc != null)
                        {
                            antComm.callFunc(); //call the async data recieved function
                        }
                        if (debugMode)
                        {
                            RemoteDisplayUpdate(antComm.DecodeResponse(readData[4], readData[5]));
                        }
                    }
                    else if (debugMode)
                    {
                        RemoteDisplayUpdate(antComm.DecodeResponse(readData[4], readData[5]));
                    }
                }
                else if (readData[2] == 0x6F)
                {
                    //startup message (AP2 only)
                    if (debugMode)
                    {
                        RemoteDisplayUpdate("Startup message: " + readData[3].ToString("X"));
                    }
                    antComm.ResponseReceived = true;
                    if (antComm.callFunc != null)
                    {
                        antComm.callFunc();
                    }
                }
                else if (readData[2] == 0x4E) //broadcast data
                {
                    string fullText = "";
                    for (int i = 4; i < readData.Length - 1; i++)
                    {
                        if (asciiMode == 2)
                        {
                            if (readData[i] != 0)
                            {
                                fullText += (char)readData[i];
                            }
                            else
                            {
                                fullText += "N0 ";
                            }
                        }
                        else if (asciiMode == 1)
                        {
                            fullText += readData[i].ToString("D") + " ";
                        }
                        else
                        {
                            fullText += readData[i].ToString("X") + " ";
                        }
                    }
                    if (!antComm.ChecksumVerify(readData))
                    {
                        fullText += "  !! Checksum Fail !!";
                    }
                    RemoteDisplayUpdate(fullText);
                }
                else if (debugMode)
                {
                    RemoteDisplayUpdate("Message Id: " + readData[2].ToString("X"));
                }
            }
        }

        /// <summary>
        /// Updates the label to show that the channel has been closed
        /// </summary>
        /// <remarks>Called remotely</remarks>
        public void OnChannelClosed()
        {
            if (!this.IsDisposed)
            {
                object[] pass = new object[1];
                pass[0] = "Closed";
                this.Invoke(this.updateLabel, pass);
            }
        }

        /// <summary>
        /// Updates the label to show that the channel has been opened
        /// </summary>
        /// <remarks>Called remotely</remarks>
        public void OnChannelOpened()
        {
            if (!this.IsDisposed)
            {
                object[] pass = new object[1];
                pass[0] = "Open";
                this.Invoke(this.updateLabel, pass);
                openedOnce = true;
            }
        }

        /// <summary>
        /// Called remotely to display a message
        /// </summary>
        /// <param name="message">The string to display</param>
        public void DisplayMessage(string message)
        {
            RemoteDisplayUpdate(message);
        }

        #endregion
    }
}
