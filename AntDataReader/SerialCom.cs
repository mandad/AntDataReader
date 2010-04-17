using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    class SerialCom
    {
        System.IO.Ports.SerialPort serialPort;
        BufferedReader spBuffer;

        public SerialCom()
        {
            serialPort.PortName = "COM9";
            serialPort.BaudRate = 57600;
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);

            //spBuffer = new BufferedReader(this);
        }

        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
