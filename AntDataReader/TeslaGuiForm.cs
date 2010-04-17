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
    public partial class frmTeslaGui : Form, ANTDataInterpreter
    {
        private frmChoose chooserForm;
        private System.IO.Ports.SerialPort serialPort;
        private ANTCommunication antComm;
        private BufferedReader spBuffer;
        private bool channelOpen = false;
        bool debugMode = false;

        public frmTeslaGui(frmChoose frmChoose)
        {
            InitializeComponent();
            this.chooserForm = frmChoose;

            //set up serial port
            serialPort.PortName = "COM9";
            serialPort.BaudRate = 57600;
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);

            antComm = new ANTCommunication(ref serialPort, this);
            spBuffer = new BufferedReader(this);
        }

        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readData = new byte[serialPort.BytesToRead];
            serialPort.Read(readData, 0, serialPort.BytesToRead);
            spBuffer.AddNewReceived(readData);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        #region ANTDataInterpreter Interface Members

        public void HaveMessages()
        {
            FlashLED();
            List<byte[]> readMessages = new List<byte[]>(spBuffer.Messages);
            foreach (byte[] readData in readMessages)
            {

                if (readData[2] == 0x40)    //status message
                {
                    //0 = no error, 7 = channel closed
                    if ((readData[5] == 0) || (readData[5] == 7))
                    {
                        //success message
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
                    if (antComm.callFunc != null)
                    {
                        antComm.callFunc();
                    }
                }
                else if (readData[2] == 0x4E) //broadcast data
                {
                    ProcessData(readData);
                }
                else if (debugMode)
                {
                    RemoteDisplayUpdate("Message Id: " + readData[2].ToString("X"));
                }
            }
        }

        private void ProcessData(byte[] readData)
        {
            int dataLength = readData[1];

        }

        private void RemoteDisplayUpdate(string p)
        {
            throw new NotImplementedException();
        }

        public void OnChannelClosed()
        {
            antComm.OpenChannel();
        }

        public void OnChannelOpened()
        {
            channelOpen = true;
        }

        public void DisplayMessage(string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void FlashLED()
        {
            lblDataLED.BackColor = Color.LimeGreen;
            tmrFlash.Start();
        }

        private void tmrFlash_Tick(object sender, EventArgs e)
        {
            lblDataLED.BackColor = Color.White;
        }
    }
}
