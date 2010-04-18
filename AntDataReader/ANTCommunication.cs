using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace AntDataReader
{
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

        public bool ResponseReceived
        {
            set { responseReceived = value; }
        }

        public bool ChannelOpen
        {
            get { return channelOpen; }
        }

        public int InitState
        {
            get { return state; }
            set { state = value; }
        }

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
        /// Gets called repeatedly to sequence
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
                    startTime = System.DateTime.Now.ToFileTimeUtc();
                    SendCommand(ANTCommands.OpenChannel());
                    break;
                case 6:
                    long stopTime = System.DateTime.Now.ToFileTimeUtc();
                    channelOpen = true;
                    callFunc = null;
                    parent.OnChannelOpened();
                    state = 0;
                    break;
            }
        }

        /// <summary>
        /// Reopens a channel that has been initialized
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
            sp.Write(message, 0, message.Length);
        }

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
                    SendCommand(ANTCommands.OpenRxScanMode());
                    break;
                case 5:
                    channelOpen = true;
                    callFunc = null;
                    parent.OnChannelOpened();
                    state = 0;
                    break;
            }
        }

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
