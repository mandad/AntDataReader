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

        public DataItem[] ProcessedData
        {
            get { return processedData; }
        }

        public SensorType Sensor
        {
            get { return sensor; }
        }

        public enum SensorType
        {
            Temperature,
            Accelerometer,
            Button,
            Unknown,
            InvalidData
        }

        public enum DataType
        {
            Analog,
            Digital
        }

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
