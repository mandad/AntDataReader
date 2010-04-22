using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    class DataDecoder
    {
        byte[] rawPacket;
        int dataLength;
        SensorType sensor = SensorType.InvalidData;
        DataItem[] processedData;

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
            if (dataLength == 9)
            {
                int sensorType = rawData[4] & 0x70; //01110000 mask
                sensorType = sensorType >> 4;
                switch (sensorType)
                {
                    case 1:
                        sensor = SensorType.Temperature;
                        break;
                    default:
                        sensor = SensorType.Unknown;
                        break;
                }
                ProcessData();
            }
        }

        /// <summary>
        /// 
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
                    //analog1.value = GetAnalogNum(rawPacket[3], rawPacket[4]);
                    //analog1.valid = true;
                    break;
            }

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
    }
}
