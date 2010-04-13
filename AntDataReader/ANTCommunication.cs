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
        frmDisplay parent;

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

        public ANTCommunication(ref SerialPort spSet, frmDisplay parentForm)
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
                    needsResponse = false;
                    waitTimer.Start();
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
                    SendCommand(ANTCommands.OpenChannel());
                    break;
                case 6:
                    channelOpen = true;
                    callFunc = null;
                    parent.statusCallback();
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
                parent.statusCallback();
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
                parent.statusCallback();
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
                    parent.statusCallback();
                    state = 0;
                    break;
            }
        }
    }
}
