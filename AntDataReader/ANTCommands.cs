using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    /// <summary>
    /// Stores commands for the ANT protocol
    /// </summary>
    static class ANTCommands
    {
        const byte channelNum = 0x00;

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

        /// <summary>
        /// Gets the checksum byte for a message
        /// </summary>
        /// <param name="dataCheck">The full message without the checksum</param>
        /// <returns>The checksum byte</returns>
        public static byte GetChecksum(byte[] dataCheck)
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
            data[3] = channelNum; //channel number
            data[4] = 0x40; //channel type = unidirectional receive only
            data[5] = 0x00; //network number (default public)
            data [6] = GetChecksum(data);

            return data;
        }

        /// <summary>
        /// Sets the channel ID for a slave device, match any transmission, receive any
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] SetChannelId()
        {
            byte[] message = BasicData(5);
            message[2] = 0x51;
            message[3] = channelNum;  //channel number
            message[4] = 0x00;  //device number LSB (0 = slave)
            message[5] = 0x00;  //device number MSB
            message[6] = 0x00;  //pairing request & device type (0 = match any)
            message[7] = 0x00;  //transmission type (0 = receive any)
            message[8] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Performs a soft reset of the ANT system
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] Reset()
        {
            byte[] message = BasicData(1);
            message[2] = 0x4A;
            message[3] = 0x00;
            message[4] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Opens the ANT channel after it has been initialized
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] OpenChannel()
        {
            byte[] message = BasicData(1);
            message[2] = 0x4B;
            message[3] = channelNum;  //channel number
            message[4] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Closes an open ANT channel
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] CloseChannel()
        {
            byte[] message = BasicData(1);
            message[2] = 0x4C;
            message[3] = channelNum;
            message[4] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Opens scan mode after initialization of the channel
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] OpenRxScanMode()
        {
            byte[] message = BasicData(1);
            message[2] = 0x5B;
            message[3] = channelNum;
            message[4] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Sets the channel message period
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] SetChannelPeriod()
        {
            byte[] message = BasicData(3);
            message[2] = 0x43;
            message[3] = channelNum;
            message[4] = 0x0F;
            message[5] = 0x00;
            message[6] = GetChecksum(message);

            return message;
        }

        /// <summary>
        /// Gets the message to enable extended messages
        /// </summary>
        /// <returns>The message packet</returns>
        public static byte[] RxExtMessageEnable()
        {
            byte[] message = BasicData(2);
            message[2] = 0x66;
            message[3] = 0; //filler
            message[4] = 1; //enable
            message[5] = GetChecksum(message);

            return message;
        }

        #endregion
        
    }
}
