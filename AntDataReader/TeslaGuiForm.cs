using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
//using Infragistics.UltraChart.Resources.Appearance;

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
        delegate void UpdateGUI(DataDecoder.SensorType senseType, int boardId, object[] parameters);
        UpdateGUI updateGUI;
        System.IO.FileStream fsLog;
        StreamWriter swLog;
        int writeCount = 0;
        TempVal tempConv;
        bool purposeClose = false;
        Dictionary<DataDecoder.SensorType, int> lastRecieved;

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
            tempConv = new TempVal();

            //Initialize timers
            flashTimer = new System.Timers.Timer(50);
            flashTimer.Elapsed += new System.Timers.ElapsedEventHandler(flashTimer_Elapsed);
            simTimer = new System.Timers.Timer(500);
            simTimer.Elapsed += new System.Timers.ElapsedEventHandler(simTimer_Elapsed);

            pastTemps = new Queue<double>(150);

            //Clear labels
            lblError.Text = "";
            lblLastMessage.Text = "";

            //Set up last received
            lastRecieved = new Dictionary<DataDecoder.SensorType, int>(3);
            lastRecieved.Add(DataDecoder.SensorType.Temperature, 100);
            lastRecieved.Add(DataDecoder.SensorType.Accelerometer, 100);
            lastRecieved.Add(DataDecoder.SensorType.Button, 100);
        }

        /// <summary>
        /// EVENT: Creates a simulation packet for testing
        /// </summary>
        /// <remarks>Called every 100ms</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //generate the temperature
            int tempADC = Convert.ToInt32(Math.Round(((Convert.ToDouble(lastTemp++) / 100) + .5) * 4096 / 2.5));
            byte LSB = Convert.ToByte(tempADC & 0xFF);
            byte MSB = Convert.ToByte(((tempADC & 0xF00) >> 8) | 0x80);
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

        /// <summary>
        /// EVENT: Resets the flash "LED" color back to normal
        /// </summary>
        /// <remarks>Called 50ms after being set green</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// EVENT: Processes data when it is received by the serial port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readData = new byte[serialPort.BytesToRead];
            serialPort.Read(readData, 0, serialPort.BytesToRead);
            spBuffer.AddNewReceived(readData);
        }

        /// <summary>
        /// EVENT: Opens the serial port and ANT protocol when the "Start" menu item is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    lastRecieved[DataDecoder.SensorType.Accelerometer]++;
                    lastRecieved[DataDecoder.SensorType.Button]++;
                    lastRecieved[DataDecoder.SensorType.Temperature]++;
                    lastRecieved[data.Sensor] = 0;
                    switch (data.Sensor)
                    {
                        case DataDecoder.SensorType.Temperature:
                            //convert to voltage
                            double dataValue = GetADCVoltage(data.ProcessedData[0].value);
                            //convert to temperature
                            //dataValue = (dataValue - .5) * 100;   //TMP 36
                            dataValue = tempConv.GetTemp(dataValue);
                            dataValue = Math.Round(dataValue, 2);
                            pastTemps.Enqueue(dataValue);
                            if (pastTemps.Count == 150)
                            {
                                pastTemps.Dequeue();
                            }
                            toPass = new object[1];
                            toPass[0] = dataValue;
                            break;
                        case DataDecoder.SensorType.Accelerometer:
                            double xValue = data.ProcessedData[0].value;
                            double yValue = data.ProcessedData[1].value;
                            double zValue = data.ProcessedData[2].value;

                            toPass = new object[3];
                            toPass[0] = Math.Round(xValue, 3);
                            toPass[1] = Math.Round(yValue, 3);
                            toPass[2] = Math.Round(zValue, 3);
                            break;
                        case DataDecoder.SensorType.Button:
                            toPass = new object[1];
                            toPass[0] = Convert.ToBoolean(data.ProcessedData[0].value);
                            break;
                        default:
                            toPass = new object[1];
                            break;
                    }
                    WriteInfo(toPass, data.Sensor, data.DeviceID);
                    RemoteDisplayUpdate(toPass, data.Sensor, data.DeviceID);
                }
            }

        }

        /// <summary>
        /// Converts a 12bit number from the ADC to the actual voltage read
        /// </summary>
        /// <param name="ADCVal">The raw ADC value</param>
        /// <returns></returns>
        private static double GetADCVoltage(int ADCVal)
        {
            return Convert.ToDouble(ADCVal) / 4095 * 1.5;   //using a 1.5V reference
        }

        /// <summary>
        /// Writes data to a log file and the web
        /// </summary>
        /// <param name="toPass">The data from the sensor</param>
        /// <param name="sensorType">The type of sensor from which the data was taken</param>
        /// <param name="boardId">The ID of the board</param>
        private void WriteInfo(object[] toPass, DataDecoder.SensorType sensorType, int boardId)
        {


            if (recordDataToolStripMenuItem.Checked)
            {
                WriteToWeb(toPass, sensorType, boardId);
            }
            if (saveToFileToolStripMenuItem.Checked)
            {
                WriteToFile(toPass, sensorType, boardId);
            }
        }

        /// <summary>
        /// Writes data to a local log file
        /// </summary>
        /// <remarks>File format is [Sensor type],[Board ID],[Data1],[Data2],...</remarks>
        /// <param name="toPass">The data from the sensor</param>
        /// <param name="sensorType">The type of sensor from which the data was taken</param>
        /// <param name="boardId">The ID of the board</param>
        private void WriteToFile(object[] toPass, DataDecoder.SensorType sensorType, int boardId)
        {
            if (fsLog != null)
            {
                switch (sensorType)
                {
                    case DataDecoder.SensorType.Temperature:
                        swLog.WriteLine("Temp," + boardId.ToString() + "," + toPass[0].ToString());
                        break;
                    case DataDecoder.SensorType.Accelerometer:
                        swLog.WriteLine("Accel," + boardId.ToString() + "," + toPass[0].ToString() + "," + toPass[1].ToString()
                            + "," + toPass[2].ToString());
                        break;
                    case DataDecoder.SensorType.Button:
                        swLog.WriteLine("Temp," + boardId.ToString() + "," + toPass[0].ToString());
                        break;
                }
            }
        }

        /// <summary>
        /// Puts data to web server for database storage
        /// </summary>
        /// <param name="toPass">The data from the sensor</param>
        /// <param name="sensorType">The type of sensor from which the data was taken</param>
        /// <param name="boardId">The ID of the board</param>
        private void WriteToWeb(object[] toPass, DataDecoder.SensorType sensorType, int boardId)
        {
            writeCount++;
            if ((!simulatedToolStripMenuItem.Checked || (writeCount > 10)) && !thrdWebSubmit.IsBusy)
            {
                writeCount = 0;
                switch (sensorType)
                {
                    case DataDecoder.SensorType.Temperature:
                        thrdWebSubmit.RunWorkerAsync("http://www.dmanda.com/capstone/putdata.php?datatype=temp&id=" + boardId.ToString()
                            + "&data[0]=" + toPass[0].ToString());
                        break;
                    case DataDecoder.SensorType.Accelerometer:
                        thrdWebSubmit.RunWorkerAsync("http://www.dmanda.com/capstone/putdata.php?datatype=accel&id=" + boardId.ToString()
                            + "&data[0]=" + toPass[0].ToString() + "&data[1]=" + toPass[1].ToString() + "&data[2]=" + toPass[2].ToString());
                        break;
                    case DataDecoder.SensorType.Button:
                        thrdWebSubmit.RunWorkerAsync("http://www.dmanda.com/capstone/putdata.php?datatype=button&id=" + boardId.ToString()
                            + "&data[0]=" + toPass[0].ToString());
                        break;
                }
            }
        }

        /// <summary>
        /// Invokes the GUI update to make it thread safe
        /// </summary>
        /// <param name="passIn">The data needed to update the GUI for that sensor</param>
        /// <param name="sensor">Sensor type of data to update</param>
        /// <param name="sensorBoard">The ID of the sensor board transmitting the data</param>
        private void RemoteDisplayUpdate(object[] passIn, DataDecoder.SensorType sensor, int sensorBoard)
        {
            if (!this.IsDisposed)
            {
                object[] pass = new object[3];
                pass[0] = sensor;
                pass[1] = sensorBoard;
                pass[2] = passIn;
                try
                {
                    this.Invoke(this.updateGUI, pass);
                }
                catch { }
            }
        }

        /// <summary>
        /// Updates the GUI from another thread to avoid conflicts
        /// </summary>
        /// <param name="senseType">The type of the sensor data to update</param>
        /// <param name="boardID">The board from which the data originated</param>
        /// <param name="parameters">The data values required by that sensor</param>
        private void UpdateGUIFunction(DataDecoder.SensorType senseType, int boardID, object[] parameters)
        {
            HideShowBlocks();

            lblError.Text = "";
            lblLastMessage.Text = DateTime.Now.ToLongTimeString();
            switch (senseType)
            {
                case DataDecoder.SensorType.Temperature:
                    //update label
                    lblTemp.Text = parameters[0].ToString();
                    lblTempF.Text = (Convert.ToDouble(parameters[0]) * 9 / 5 + 32).ToString();

                    //draw graph
                    DrawTempGraph();

                    //chrtTempHist.Data.DataSource = graphPts;
                    //chrtTempHist.Data.DataBind();
                    break;
                case DataDecoder.SensorType.Accelerometer:
                    double accelX = (Convert.ToDouble(parameters[0]) - 2047.5) / 2047.5 * 2;
                    double accelY = (Convert.ToDouble(parameters[1]) - 2047.5) / 2047.5 * 2;
                    double accelZ = (Convert.ToDouble(parameters[2]) - 2047.5) / 2047.5 * 2;
                    lblAccelX.Text = Math.Round(accelX, 2).ToString() + " G";
                    lblAccelY.Text = Math.Round(accelY, 2).ToString() + " G";
                    lblAccelZ.Text = Math.Round(accelZ, 2).ToString() + " G";
                    userControl11.UpdateDisplay(accelX, accelY, accelZ);
                    break;
                case DataDecoder.SensorType.Button:
                    if ((bool)parameters[0])
                    {
                        lblButtonPress.BackColor = Color.Blue;
                    }
                    else
                    {
                        lblButtonPress.BackColor = Color.White;
                    }
                    break;
                case DataDecoder.SensorType.Unknown:
                    lblError.Text = "Unknown Sensor Type";
                    break;
            }
        }

        /// <summary>
        /// Hides blocks for data that is not being received
        /// </summary>
        private void HideShowBlocks()
        {
            //Accelerometer
            if (lastRecieved[DataDecoder.SensorType.Accelerometer] > 10)
            {
                lastRecieved[DataDecoder.SensorType.Accelerometer] = 10;
                grpAccel.Visible = false;
            }
            else
            {
                grpAccel.Visible = true;
            }

            //Temperature
            if (lastRecieved[DataDecoder.SensorType.Temperature] > 10)
            {
                lastRecieved[DataDecoder.SensorType.Temperature] = 10;
                grpTemp.Visible = false;
            }
            else
            {
                grpTemp.Visible = true;
            }

            //Button
            if (lastRecieved[DataDecoder.SensorType.Button] > 10)
            {
                lastRecieved[DataDecoder.SensorType.Button] = 10;
                grpButton.Visible = false;
            }
            else
            {
                grpButton.Visible = true;
            }
        }

        /// <summary>
        /// Draws the temperature history graph
        /// </summary>
        private void DrawTempGraph()
        {
            Graphics g = lblTempGraph.CreateGraphics();
            g.Clear(Color.White);
            double[] graphPts = pastTemps.ToArray();
            Pen graphPen = new Pen(Color.Blue, 3);

            for (int i = 0; i < graphPts.Length; i++)
            {
                g.DrawLine(graphPen, 2 * i, lblTempGraph.Height - Convert.ToInt32(Math.Round(2 * graphPts[i], 0)),
                    (2 * i) + 2, lblTempGraph.Height - Convert.ToInt32(Math.Round(2 * graphPts[i])));
            }
        }

        /// <summary>
        /// Called remotely when the ANT channel is closed
        /// </summary>
        public void OnChannelClosed()
        {
            if (purposeClose)
            {
                purposeClose = false;
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
            else
            {
                antComm.OpenChannel();
            }
        }

        /// <summary>
        /// Called remotely when the ANT channel is opened
        /// </summary>
        public void OnChannelOpened()
        {
            channelOpen = true;
        }

        /// <summary>
        /// Called remotely to display a message
        /// </summary>
        /// <remarks>Not implemented</remarks>
        /// <param name="message">The me</param>
        public void DisplayMessage(string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Starts the sequence to flash the green square ("LED")
        /// </summary>
        private void FlashLED()
        {
            lblDataLED.BackColor = Color.LimeGreen;

            flashTimer.Start();
        }

        /// <summary>
        /// EVENT: Launches the Debug mode form when "Debug Mode" menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (antComm.ChannelOpen)
            {
                antComm.CloseChannel();
            }
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            Form launch = new frmDisplay(chooserForm);
            launch.Show();
            this.Hide();
        }

        /// <summary>
        /// EVENT: Closes the hidden chooser form when this one is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTeslaGui_FormClosed(object sender, FormClosedEventArgs e)
        {
            chooserForm.Close();
        }

        /// <summary>
        /// EVENT: Close the ant channel and serial port when the form closes
        /// Stop logging data if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// EVENT: Called when the "Stop" data collection menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (antComm.ChannelOpen)
            {
                purposeClose = true;
                antComm.CloseChannel();
            }
            else
            {
                //allow the user to try again
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// EVENT: Called when the "Simulation" menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// EVENT: Called when the "Save to file" menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saveToFileToolStripMenuItem.Checked)
            {
                dlgSaveFile.ShowDialog();
            }
            else
            {
                fsLog.Close();
                fsLog = null;
            }
        }

        /// <summary>
        /// EVENT: Handles saving the file for the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dlgSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                fsLog = new FileStream(dlgSaveFile.FileName, FileMode.Create);
                swLog = new StreamWriter(fsLog);
                //recordDataToolStripMenuItem.Enabled = true;
                saveToFileToolStripMenuItem.Checked = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File error: " + ex.Message);
                dlgSaveFile.ShowDialog();
            }
        }

        /// <summary>
        /// EVENT: Called when "Record Data" menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!recordDataToolStripMenuItem.Checked)
            {
                //fsLog.Close();
            }
        }

        /// <summary>
        /// Runs the web data submission in a background thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thrdWebSubmit_DoWork(object sender, DoWorkEventArgs e)
        {
            HttpWebRequest request = WebRequest.Create(e.Argument.ToString()) as HttpWebRequest;
            try
            {
                request.GetResponse();
            }
            catch   //this will eliminate errors due to timeouts
            {
            }
        }
    }
}
