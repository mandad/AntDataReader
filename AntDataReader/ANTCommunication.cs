using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace AntDataReader
{
    /// <summary>
    /// Handles ANT communication protocol and interaction with the Nordic chip
    /// </summary>
    class ANTCommunication
    {
        SerialPort sp;
        System.Timers.Timer waitTimer;
        bool responseReceived = false;
        bool channelOpen = false;
        bool needsResponse = true;
        int state = 0;
        public delegate void ContinuationCallback();
        public ContinuationCallback callFunc;
        Dictionary<byte, string> responseCodes;
        ANTDataInterpreter parent;
        long startTime;

        /// <summary>
        /// Set to indicate that a serial response has been received
        /// </summary>
        public bool ResponseReceived
        {
            set { responseReceived = value; }
        }

        /// <summary>
        /// Indicates if the ANT channel is open
        /// </summary>
        public bool ChannelOpen
        {
            get { return channelOpen; }
        }

        /// <summary>
        /// The state of a multistage function with multiple commands
        /// </summary>
        public int InitState
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Initiates the communication class
        /// </summary>
        /// <param name="spSet">The serial port used for communication to the USB receiver</param>
        /// <param name="parentForm">The form using this</param>
        public ANTCommunication(ref SerialPort spSet, ANTDataInterpreter parentForm)
        {
            sp = spSet;
            waitTimer = new System.Timers.Timer(500);
            waitTimer.Elapsed += new System.Timers.ElapsedEventHandler(waitTimer_Elapsed);
            parent = parentForm;
            //responseCodes = GetResponseCodes();
        }

        /// <summary>
        /// Fills a dictionary with keys of response message codes and values of their description
        /// </summary>
        /// <returns>the filled dictionary</returns>
        private Dictionary<byte, string> GetResponseCodes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// DEPRECATED: Used for ghetto asynch communications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void waitTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (responseReceived || !needsResponse)
            {
                responseReceived = false;
                waitTimer.Stop();
                callFunc();
            }
            else if (state != 0)    //this is a safeguard but shouldn't be needed, timer disabled when state = 0
            {
                waitTimer.Start();
            }

        }

        /// <summary>
        /// Initialized sychronous ANT communication in receive mode
        /// Default settings used
        /// </summary>
        public void InitializeAntSyncronous()
        {
            state++;
            switch (state)
            {
                case 1:
                    callFunc = new ContinuationCallback(this.InitializeAntSyncronous);
                    SendCommand(ANTCommands.Reset());   //no response on AP1
                    needsResponse = true;
                    //waitTimer.Start();
                    break;
                case 2:
                    needsResponse = true;
                    SendCommand(ANTCommands.AssignChannel());
                    break;
                case 3:
                    SendCommand(ANTCommands.SetChannelId());
                    break;
                case 4:
                    SendCommand(ANTCommands.SetChannelPeriod());
                    break;
                case 5:
                    //startTime = System.DateTime.Now.ToFileTimeUtc();
                    SendCommand(ANTCommands.OpenChannel());
                    break;
                case 6:
                    //long stopTime = System.DateTime.Now.ToFileTimeUtc();
                    channelOpen = true;
                    callFunc = null;
                    parent.OnChannelOpened();
                    state = 0;
                    break;
            }
        }

        /// <summary>
        /// Reopens a channel that has been previously initialized
        /// </summary>
        public void OpenChannel()
        {
            state++;
            if (state == 1)
            {
                callFunc = new ContinuationCallback(this.OpenChannel);
                SendCommand(ANTCommands.OpenChannel());
            }
            else if (state == 2)
            {
                channelOpen = true;
                callFunc = null;
                parent.OnChannelOpened();
                state = 0;
            }
        }

        /// <summary>
        /// Wites a command array to the serial port
        /// </summary>
        /// <param name="message">The message to send</param>
        private void SendCommand(byte[] message)
        {
            if (sp.IsOpen)
            {
                sp.Write(message, 0, message.Length);
            }
        }

        /// <summary>
        /// Closes the channel and waits for a response
        /// </summary>
        public void CloseChannel()
        {
            state++;
            if (state == 1)
            {
                callFunc = new ContinuationCallback(this.CloseChannel);
                SendCommand(ANTCommands.CloseChannel());
            }
            else if (state == 2)    //gets back an EVENT_CHANNEL_CLOSED as well
            {
                channelOpen = false;
                callFunc = null;
                parent.OnChannelClosed();
                state = 0;
            }
        }

        /// <summary>
        /// Opens RX Scan mode without extended packets
        /// </summary>
        public void RxScanMode()
        {

            state++;
            switch (state)
            {
                case 1:
                    callFunc = new ContinuationCallback(this.RxScanMode);
                    needsResponse = true;
                    SendCommand(ANTCommands.Reset());
                    break;
                case 2:
                    SendCommand(ANTCommands.AssignChannel());
                    break;
                case 3:
                    SendCommand(ANTCommands.SetChannelId());
                    break;
                case 4:
                    SendCommand(ANTCommands.RxExtMessageEnable());
                    break;
                case 5:
                    SendCommand(ANTCommands.OpenRxScanMode());
                    break;
                case 6:
                    channelOpen = true;
                    callFunc = null;
                    parent.OnChannelOpened();
                    state = 0;
                    break;
            }
        }

        /// <summary>
        /// Sends the reset command, no response required
        /// </summary>
        public void ResetANT()
        {
            SendCommand(ANTCommands.Reset());
        }

        /// <summary>
        /// Decodes a Channel Response / Event (0x40)
        /// </summary>
        /// <param name="messageId">The message id from the response</param>
        /// <param name="messageCode">The message code from the response</param>
        /// <returns>A string containing the message to display</returns>
        public string DecodeResponse(byte messageId, byte messageCode)
        {
            string displayMessage;
            if (messageId == 1)
            {
                displayMessage = "RF event - Message Code: ";
            }
            else
            {
                displayMessage = "Message ID: " + messageId.ToString("X") + " - Message Code: ";
            }
            switch (messageCode)
            {
                case 0:
                    displayMessage += "RESPONSE_NO_ERROR";
                    break;
                case 1:
                    displayMessage += "EVENT_RX_SEARCH_TIMEOUT";
                    //parent.OnChannelClosed();

                    /*
                    //this event is raised in another thread, so we have to use invoke on a delegate
                    object[] pass = new object[1];
                    pass[0] = "Closed";
                    this.Invoke(this.updateLabel, pass);
                     */
                    break;
                case 2:
                    displayMessage += "EVENT_RX_FAIL";
                    break;
                case 7:
                    displayMessage += "EVENT_CHANNEL_CLOSED";
                    parent.OnChannelClosed();
                    break;
                case 8:
                    displayMessage += "EVENT_RX_FAIL_GO_TO_SEARCH";
                    break;
                default:
                    displayMessage += "Message Code: " + messageCode.ToString();
                    break;
            }
            return displayMessage;

            //parent.DisplayMessage(displayMessage);

            /*
            RemoteDisplayUpdate(displayMessage);
             */
        }

        /// <summary>
        /// Runs through the elements of a data packet and checks its validity against the checksum byte
        /// </summary>
        /// <param name="toVerify">The data packet to verify</param>
        /// <returns>True if the checksum is correct</returns>
        public bool ChecksumVerify(byte[] toVerify)
        {
            byte checkSum = toVerify[0];
            for (int i = 1; i < (toVerify.Length - 1); i++)
            {
                checkSum ^= toVerify[i];
            }

            return (checkSum == toVerify[toVerify.Length - 1]);
        }
    }
}
