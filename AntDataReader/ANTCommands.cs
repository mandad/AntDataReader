using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    static class ANTCommands
    {
        #region Utility Functions

        /// <summary>
        /// Performs initial setup of the message packet
        /// </summary>
        /// <param name="dataBytes">Number of data bytes in the message</param>
        /// <returns>The full packet prefilled with the sync and data length bytes</returns>
        static byte[] BasicData(int dataBytes)
        {
            byte[] data = new byte[dataBytes + 4];
            data[0] = 0xA4; //Sync
            data[1] = (byte)dataBytes;
            return data;
        }
        static byte GetChecksum(byte[] dataCheck)
        {
            byte checkSum = dataCheck[0];
            for (int i = 1; i < dataCheck.Length; i++)
            {
                checkSum ^= dataCheck[i];
            }
            return checkSum;
        }

        #endregion

        #region ANT Commands

        /// <summary>
        /// Assigns the ANT Module to the default channel, bidirectional recieve mode
        /// </summary>
        /// <returns>The message content necessary to assign the channel</returns>
        public static byte[] AssignChannel()
        {
            byte[] data = BasicData(3);
            data[2] = 0x42; //msg id (assign channel)
            data[3] = 0x00; //channel number
            data[4] = 0x00; //channel type
            data[5] = 0x00; //channel number (default public)
            data [6] = GetChecksum(data);

            return data;
        }

        public static byte[] SetChannelId()
        {
            byte[] message = BasicData(5);
            message[2] = 0x51;
            message[3] = 0x00;  //channel number
            message[4] = 0x00;  //device number LSB (0 = slave)
            message[5] = 0x00;  //device number MSB
            message[6] = 0x00;  //pairing request & device type (0 = match any)
            message[7] = 0x00;  //transmissing type (0 = receive any)
            message[8] = GetChecksum(message);

            return message;
        }

        public static byte[] Reset()
        {
            byte[] message = BasicData(1);
            message[2] = 0x4A;
            message[3] = 0x00;  //channel number
            message[4] = GetChecksum(message);

            return message;
        }

        public static byte[] OpenChannel()
        {
            byte[] message = BasicData(1);
            message[2] = 0x4B;
            message[3] = 0x00;  //channel number
            message[4] = GetChecksum(message);

            return message;
        }

        #endregion
    }
}
