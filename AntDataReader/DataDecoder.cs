using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    /// <summary>
    /// Decodes a data packet from the ANT protocol to extract sensor information
    /// </summary>
    class DataDecoder
    {
        byte[] rawPacket;
        int dataLength;
        SensorType sensor = SensorType.InvalidData;
        DataItem[] processedData;
        bool isExtendedMessage;
        int deviceID;

        /// <summary>
        /// Gives the device ID portion of the extended message
        /// </summary>
        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        /// <summary>
        /// Indicates if the message is an extended message
        /// </summary>
        public bool IsExtendedMessage
        {
            get { return isExtendedMessage; }
        }

        /// <summary>
        /// The data after it has been decoded
        /// </summary>
        public DataItem[] ProcessedData
        {
            get { return processedData; }
        }

        /// <summary>
        /// The type of sensor from which the data came
        /// </summary>
        public SensorType Sensor
        {
            get { return sensor; }
        }

        /// <summary>
        /// Represents the type of sensor
        /// </summary>
        public enum SensorType
        {
            Temperature,
            Accelerometer,
            Button,
            Unknown,
            InvalidData
        }

        /// <summary>
        /// Represents whether the data stored is analog or digital
        /// </summary>
        public enum DataType
        {
            Analog,
            Digital
        }

        /// <summary>
        /// Stores one piece of data from the sensor
        /// </summary>
        public class DataItem
        {
            public DataItem(DataType thisType = DataType.Analog)
            {
                type = thisType;
            }

            public DataType type = DataType.Analog;
            public int value = 0;
            public bool valid = false;
        }

        /// <summary>
        /// Initialized the decoder class, will fill all elements if the format is properly recognised
        /// </summary>
        /// <param name="rawData">The raw ANT packet to process</param>
        public DataDecoder(byte[] rawData)
        {
            rawPacket = rawData;
            dataLength = rawPacket[1];
            //if standard packet (first byte of data is the channel id)
            if (dataLength == 9 || dataLength == 14)
            {
                int sensorType = rawData[4] & 0xE0; //11100000 mask
                sensorType = sensorType >> 5;

                //set the extended message parameters
                if ((dataLength == 14) && (rawData[12] == 0x80))
                {
                    isExtendedMessage = true;
                    deviceID = GetDeviceID(rawData[13], rawData[14]);
                }
                else
                {
                    isExtendedMessage = false;
                }

                switch (sensorType)
                {
                    case 2:
                        sensor = SensorType.Accelerometer;
                        break;
                    case 4:
                        sensor = SensorType.Temperature;
                        break;
                    case 3:
                        sensor = SensorType.Button;
                        break;
                    default:
                        sensor = SensorType.Unknown;
                        break;
                }
                ProcessData();
            }
        }

        /// <summary>
        /// Sets the data array for the type of sensor
        /// </summary>
        private void ProcessData()
        {
            switch (sensor)
            {
                case SensorType.Temperature:
                    processedData = new DataItem[1];
                    processedData[0] = new DataItem();
                    processedData[0].value = GetAnalogNum(rawPacket[4], rawPacket[5]);
                    processedData[0].valid = true;
                    break;
                case SensorType.Accelerometer:
                    processedData = new DataItem[3];
                    processedData[0] = new DataItem();
                    processedData[0].value = GetAnalogNum(rawPacket[4], rawPacket[5]);
                    processedData[0].valid = true;
                    processedData[1] = new DataItem();
                    processedData[1].value = GetAnalogNum(rawPacket[6], rawPacket[7]);
                    processedData[1].valid = true;
                    processedData[2] = new DataItem();
                    processedData[2].value = GetAnalogNum(rawPacket[7], rawPacket[8]);
                    processedData[2].valid = true;
                    break;
                case SensorType.Button:
                    processedData = new DataItem[1];
                    processedData[0] = new DataItem();
                    processedData[0].type = DataType.Digital;
                    processedData[0].valid = true;
                    processedData[0].value = GetDigitalVal(rawPacket[4]);
                    break;
            }

        }

        /// <summary>
        /// Extracts the digital value bit from a data packet byte
        /// </summary>
        /// <param name="rawData">The byte containing the digital bit in the 000x0000 position</param>
        /// <returns>1 or 0 as per the digital value</returns>
        private int GetDigitalVal(byte rawData)
        {
            return (rawData >> 4) & 0x01;
        }

        /// <summary>
        /// Extracts a 12 bit analog value from two bytes, aligned to lower end
        /// </summary>
        /// <param name="MSB">The upper byte</param>
        /// <param name="LSB">The lower byte</param>
        /// <returns>The concatenated 12 bit number</returns>
        private int GetAnalogNum(byte MSB, byte LSB)
        {
            //mask off the 4 bits on top of MSB
            MSB &= 0x0F;

            //concatenate the two bytes
            int construct = MSB;
            construct = construct << 8;
            construct |= LSB;
            return construct;
        }

        /// <summary>
        /// Concatenates the two bytes that make up the device ID
        /// </summary>
        /// <param name="MSB">The upper byte</param>
        /// <param name="LSB">The lower byte (second in data transmission)</param>
        /// <returns></returns>
        private int GetDeviceID(byte MSB, byte LSB)
        {
            //concatenate the two bytes
            int construct = MSB;
            construct = construct << 8;
            construct |= LSB;
            return construct;
        }
    }
}
