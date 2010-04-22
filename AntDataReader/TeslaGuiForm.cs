﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace AntDataReader
{
    /// <summary>
    /// The GUI form that displays received data
    /// </summary>
    public partial class frmTeslaGui : Form, ANTDataInterpreter
    {
        private frmChoose chooserForm;
        private System.IO.Ports.SerialPort serialPort;
        private ANTCommunication antComm;
        private BufferedReader spBuffer;
        private bool channelOpen = false;
        bool debugMode = false;
        System.Timers.Timer flashTimer;
        System.Timers.Timer simTimer;
        delegate void UpdateGUI(DataDecoder.SensorType senseType, object[] parameters);
        UpdateGUI updateGUI;
        System.IO.FileStream fsLog;
        StreamWriter swLog;
        int writeCount = 0;

        Queue<double> pastTemps;
        byte lastTemp = 0;

        /// <summary>
        /// Initialized the GUI
        /// Attempts to open COM 9 by default
        /// </summary>
        /// <param name="frmChoose">The parent chooser form</param>
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
            simTimer = new System.Timers.Timer(100);
            simTimer.Elapsed += new System.Timers.ElapsedEventHandler(simTimer_Elapsed);

            pastTemps = new Queue<double>(100);
        }

        void simTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //generate the temperature
            int tempADC = Convert.ToInt32(Math.Round(((Convert.ToDouble(lastTemp++) / 100) + .5) * 4096 / 2.5));
            byte LSB = Convert.ToByte(tempADC & 0xFF);
            byte MSB = Convert.ToByte(((tempADC & 0xF00) >> 8) | 0x10);
            if (lastTemp == 120)
            {
                lastTemp = 0;
            }

            byte[] readData = new byte[13];
            readData[0] = 0xA4;
            readData[1] = 9;
            readData[2] = 0x4E;
            readData[3] = 0;
            readData[4] = MSB;  //start of data
            readData[5] = LSB;
            readData[6] = 0;
            readData[7] = 0;
            readData[8] = 0;
            readData[9] = 0;
            readData[10] = 0;
            readData[11] = 0;
            readData[12] = ANTCommands.GetChecksum(readData);

            spBuffer.AddNewReceived(readData);
        }

        void flashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            flashTimer.Stop();
            lblDataLED.BackColor = Color.White;
        }

        /// <summary>
        /// Unchecks all the COM port selection menu items before selecting a new one
        /// </summary>
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

        /// <summary>
        /// Called from BufferedReader when a full message has arrived
        /// </summary>
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

        /// <summary>
        /// Puts the data into a format for display
        /// </summary>
        /// <param name="readData">The full packet received from ANT</param>
        private void ProcessData(byte[] readData)
        {
            if (antComm.ChecksumVerify(readData))
            {
                DataDecoder data = new DataDecoder(readData);
                if (data.Sensor != DataDecoder.SensorType.InvalidData)
                {
                    object[] toPass;
                    switch (data.Sensor)
                    {
                        case DataDecoder.SensorType.Temperature:
                            //convert to voltage
                            double dataValue = GetADCVoltage(data.ProcessedData[0].value);
                            //convert to temperature
                            dataValue = (dataValue - .5) * 100;
                            dataValue = Math.Round(dataValue, 2);
                            pastTemps.Enqueue(dataValue);
                            if (pastTemps.Count == 100)
                            {
                                pastTemps.Dequeue();
                            }
                            toPass = new object[1];
                            toPass[0] = dataValue;
                            break;
                        default:
                            toPass = new object[1];
                            break;
                    }
                    WriteInfo(toPass, data.Sensor);
                    RemoteDisplayUpdate(toPass, data.Sensor);
                }
            }

        }

        private static double GetADCVoltage(int ADCVal)
        {
            return Convert.ToDouble(ADCVal) / 4095 * 2.5;
        }

        /// <summary>
        /// Writes data to a log file and the web
        /// </summary>
        /// <param name="toPass"></param>
        /// <param name="sensorType"></param>
        private void WriteInfo(object[] toPass, DataDecoder.SensorType sensorType)
        {
            writeCount++;

            // write data to log
            if (recordDataToolStripMenuItem.Checked)
            {
                switch (sensorType)
                {
                    case DataDecoder.SensorType.Temperature:
                        if (fsLog != null)
                        {
                            swLog.WriteLine("Temp," + toPass[0].ToString());
                        }

                        //only write to the web every so often
                        if ((writeCount > 10) && !thrdWebSubmit.IsBusy)
                        {
                            thrdWebSubmit.RunWorkerAsync("http://www.dmanda.com/capstone/putdata.php?datatype=temp&data[0]=" + toPass[0].ToString());
                            writeCount = 0;
                        }
                        break;
                    default:
                        break;
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

        /// <summary>
        /// Updates the GUI from another thread to avoid conflicts
        /// </summary>
        /// <param name="senseType">The type of the sensor data to update</param>
        /// <param name="parameters">The data values required by that sensor</param>
        private void UpdateGUIFunction(DataDecoder.SensorType senseType, object[] parameters)
        {
            switch (senseType)
            {
                case DataDecoder.SensorType.Temperature:
                    //update label
                    lblTemp.Text = parameters[0].ToString();

                    //draw grap
                    Graphics g = lblTempGraph.CreateGraphics();
                    g.Clear(Color.LightGray);
                    double[] graphPts = pastTemps.ToArray();
                    Pen graphPen = new Pen(Color.Blue);
                    for (int i = 0; i < graphPts.Length; i++)
                    {
                        g.DrawLine(graphPen, 2 * i, lblTempGraph.Height - Convert.ToInt32(Math.Round(graphPts[i], 0)),
                            (2 * i) + 1, lblTempGraph.Height - Convert.ToInt32(Math.Round(graphPts[i])));
                    }
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
            if (fsLog != null)
            {
                swLog.Close();
                fsLog.Close();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            antComm.CloseChannel();
        }

        private void simulatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (simulatedToolStripMenuItem.Checked)
            {
                simTimer.Start();
            }
            else
            {
                simTimer.Stop();
            }
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSaveFile.ShowDialog();
        }

        private void dlgSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                fsLog = new FileStream(dlgSaveFile.FileName, FileMode.Create);
                swLog = new StreamWriter(fsLog);
                //recordDataToolStripMenuItem.Enabled = true;
                recordDataToolStripMenuItem.Checked = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File error: " + ex.Message);
                dlgSaveFile.ShowDialog();
            }
        }

        private void recordDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!recordDataToolStripMenuItem.Checked)
            {
                //fsLog.Close();
            }
        }

        private void thrdWebSubmit_DoWork(object sender, DoWorkEventArgs e)
        {
            HttpWebRequest request = WebRequest.Create(e.Argument.ToString()) as HttpWebRequest;
            request.GetResponse();
        }
    }
}
