using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    abstract class ANTDisplay : ANTDataInterpreter
    {
        ANTCommunication antComm;
        BufferedReader spBuffer;

        private System.IO.Ports.SerialPort serialPort;
        bool debugMode = true;

        public ANTDisplay()
        {
            antComm = new ANTCommunication(ref serialPort, this);
            spBuffer = new BufferedReader(this);

            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readData = new byte[serialPort.BytesToRead];
            serialPort.Read(readData, 0, serialPort.BytesToRead);
            spBuffer.AddNewReceived(readData);
        }

        public abstract void HaveMessages();

        public abstract void OnChannelClosed();

        public abstract void OnChannelOpened();

        public abstract void DisplayMessage(string message);
    }
}
