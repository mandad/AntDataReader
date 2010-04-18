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
        System.Timers.Timer flashTimer;
        delegate void UpdateGUI(DataDecoder.SensorType senseType, object[] parameters);
        UpdateGUI updateGUI;

        public frmTeslaGui(frmChoose frmChoose)
        {
            InitializeComponent();
            this.chooserForm = frmChoose;
            updateGUI = new UpdateGUI(this.UpdateGUIFunction);

            //add the serial port selection to the menu
            string[] serialPorts = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string portName in serialPorts)
            {
                COMPortToolStripMenuItem.DropDownItems.Add(portName, null, (object sender, EventArgs e) =>
                {
                    serialPort.PortName = portName;
                    UncheckAllComs();
                    ((ToolStripMenuItem)sender).Checked = true;
                });
                if (portName == "COM9")
                {
                    ((ToolStripMenuItem)COMPortToolStripMenuItem.DropDownItems[COMPortToolStripMenuItem.DropDownItems.Count - 1]).Checked = true;
                }
            }

            //set up serial port
            serialPort = new System.IO.Ports.SerialPort();
            if (serialPorts.Contains("COM9"))
            {
                serialPort.PortName = "COM9";
            }
            serialPort.BaudRate = 57600;
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);

            //Initialize classes
            antComm = new ANTCommunication(ref serialPort, this);
            spBuffer = new BufferedReader(this);

            flashTimer = new System.Timers.Timer(50);
            flashTimer.Elapsed += new System.Timers.ElapsedEventHandler(flashTimer_Elapsed);


        }

        void flashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            flashTimer.Stop();
            lblDataLED.BackColor = Color.White;
        }

        private void UncheckAllComs()
        {
            foreach (ToolStripMenuItem comItem in COMPortToolStripMenuItem.DropDownItems)
            {
                comItem.Checked = false;
            }
        }

        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readData = new byte[serialPort.BytesToRead];
            serialPort.Read(readData, 0, serialPort.BytesToRead);
            spBuffer.AddNewReceived(readData);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open serial port: " + ex.Message);
            }
            if (serialPort.IsOpen)
            {
                antComm.RxScanMode();
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = true;
            }
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
                            //RemoteDisplayUpdate(antComm.DecodeResponse(readData[4], readData[5]));
                        }
                    }
                    else if (debugMode)
                    {
                        //RemoteDisplayUpdate(antComm.DecodeResponse(readData[4], readData[5]));
                    }
                }
                else if (readData[2] == 0x6F)
                {
                    //startup message (AP2 only)
                    if (debugMode)
                    {
                        //RemoteDisplayUpdate("Startup message: " + readData[3].ToString("X"));
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
                    //RemoteDisplayUpdate("Message Id: " + readData[2].ToString("X"));
                }
            }
        }

        private void ProcessData(byte[] readData)
        {
            if (antComm.ChecksumVerify(readData)) {
                DataDecoder data = new DataDecoder(readData);
                if (data.Sensor != DataDecoder.SensorType.InvalidData)
                {
                    object[] toPass;
                    switch (data.Sensor)
                    {
                        case DataDecoder.SensorType.Temperature:
                            double dataValue = Convert.ToDouble(data.ProcessedData[0].value) / 4095 * 2.5;
                            toPass = new object[1];
                            toPass[0] = dataValue;
                            break;
                        default:
                            toPass = new object[1];
                            break;
                    }
                    RemoteDisplayUpdate(toPass, data.Sensor);
                }
            }

        }

        private void RemoteDisplayUpdate(object[] passIn, DataDecoder.SensorType sensor)
        {
            if (!this.IsDisposed)
            {
                object[] pass = new object[2];
                pass[0] = sensor;
                pass[1] = passIn;
                this.Invoke(this.updateGUI, pass);
            }
        }

        private void UpdateGUIFunction(DataDecoder.SensorType senseType, object[] parameters) {
            switch (senseType)
            {
                case DataDecoder.SensorType.Temperature:
                    lblTemp.Text = parameters[0].ToString();
                    break;
                default:
                    MessageBox.Show("Unknown Sensor Type");
                    break;
            }
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

            flashTimer.Start();
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugMode = debugModeToolStripMenuItem.Checked;
        }

        private void frmTeslaGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            chooserForm.Close();
        }

        private void frmTeslaGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (antComm != null)
            {
                antComm.CloseChannel();
            }
        }
    }
}
