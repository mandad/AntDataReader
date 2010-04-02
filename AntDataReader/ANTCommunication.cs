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
        int state = 0;
        delegate void ContinuationCallback();
        ContinuationCallback callFunc;

        public bool ResponseReceived
        {
            set { responseReceived = value; }
        }

        public bool ChannelOpen
        {
            get { return channelOpen; }
        }

        public ANTCommunication(ref SerialPort spSet)
        {
            sp = spSet;
            waitTimer = new System.Timers.Timer(500);
            waitTimer.Elapsed += new System.Timers.ElapsedEventHandler(waitTimer_Elapsed);
        }

        void waitTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (responseReceived)
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

        public void InitializeAntSyncronous()
        {
            state++;
            switch (state)
            {
                case 1:
                    callFunc = new ContinuationCallback(this.InitializeAntSyncronous);
                    SendCommand(ANTCommands.Reset());   //no response
                    SendCommand(ANTCommands.AssignChannel());
                    waitTimer.Start();
                    break;
                case 2:
                    SendCommand(ANTCommands.SetChannelId());
                    waitTimer.Start();
                    break;
                case 3:
                    SendCommand(ANTCommands.OpenChannel());
                    waitTimer.Start();
                    break;
                case 4:
                    channelOpen = true;
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
                waitTimer.Start();
            }
            else if (state == 2)
            {
                channelOpen = true;
                state = 0;
            }
        }

        private void SendCommand(byte[] message)
        {
            sp.Write(message, 0, message.Length);
        }


    }
}
