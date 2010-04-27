using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    /// <summary>
    /// Used to correlated ADC voltages to LM94022 temperatures
    /// </summary>
    class TempVal
    {
        Dictionary<double, int> tempTransfer;

        /// <summary>
        /// Initializes the correlation array
        /// </summary>
        public TempVal()
        {
            tempTransfer = new Dictionary<double, int>(201);
            FillTransferTable();
        }

        /// <summary>
        /// Returns a temperature given an ADC voltage
        /// </summary>
        /// <param name="voltage">The voltage read by the ADC</param>
        /// <returns>Temperature in degrees C</returns>
        public double GetTemp(double voltage)
        {
            voltage = Math.Round(voltage, 3);
            double temp = -99;

            if (tempTransfer.ContainsKey(voltage))
            {
                temp = tempTransfer[voltage];
            }
            else
            {
                //get the bounds
                double upperTemp = SearchTemp(voltage, 1);
                double lowerTemp = SearchTemp(voltage, -1);

                //determine the fraction of a degree and add it
                double factor = (voltage - lowerTemp) / (upperTemp - lowerTemp);
                temp = tempTransfer[lowerTemp] + factor;
            }
            return temp;

        }

        /// <summary>
        /// Searches for a temperature near a given voltage
        /// </summary>
        /// <param name="searchVal">The voltage to search near</param>
        /// <param name="direction">Direction to search (+/-1)</param>
        /// <returns>The nearest voltage that has a corresponding temp</returns>
        private double SearchTemp(double searchVal, int direction)
        {
            double checkVal = searchVal;

            do
            {
                checkVal = checkVal + (0.001 * direction);
            } while (!tempTransfer.ContainsKey(checkVal));
            
            return checkVal;
        }

        /// <summary>
        /// Fills the correlation array with factors from the datasheet
        /// </summary>
        private void FillTransferTable()
        {
            tempTransfer.Add(1.299, -50);
            tempTransfer.Add(1.294, -49);
            tempTransfer.Add(1.289, -48);
            tempTransfer.Add(1.284, -47);
            tempTransfer.Add(1.278, -46);
            tempTransfer.Add(1.273, -45);
            tempTransfer.Add(1.268, -44);
            tempTransfer.Add(1.263, -43);
            tempTransfer.Add(1.257, -42);
            tempTransfer.Add(1.252, -41);
            tempTransfer.Add(1.247, -40);
            tempTransfer.Add(1.242, -39);
            tempTransfer.Add(1.236, -38);
            tempTransfer.Add(1.231, -37);
            tempTransfer.Add(1.226, -36);
            tempTransfer.Add(1.221, -35);
            tempTransfer.Add(1.215, -34);
            tempTransfer.Add(1.21, -33);
            tempTransfer.Add(1.205, -32);
            tempTransfer.Add(1.2, -31);
            tempTransfer.Add(1.194, -30);
            tempTransfer.Add(1.189, -29);
            tempTransfer.Add(1.184, -28);
            tempTransfer.Add(1.178, -27);
            tempTransfer.Add(1.173, -26);
            tempTransfer.Add(1.168, -25);
            tempTransfer.Add(1.162, -24);
            tempTransfer.Add(1.157, -23);
            tempTransfer.Add(1.152, -22);
            tempTransfer.Add(1.146, -21);
            tempTransfer.Add(1.141, -20);
            tempTransfer.Add(1.136, -19);
            tempTransfer.Add(1.13, -18);
            tempTransfer.Add(1.125, -17);
            tempTransfer.Add(1.12, -16);
            tempTransfer.Add(1.114, -15);
            tempTransfer.Add(1.109, -14);
            tempTransfer.Add(1.104, -13);
            tempTransfer.Add(1.098, -12);
            tempTransfer.Add(1.093, -11);
            tempTransfer.Add(1.088, -10);
            tempTransfer.Add(1.082, -9);
            tempTransfer.Add(1.077, -8);
            tempTransfer.Add(1.072, -7);
            tempTransfer.Add(1.066, -6);
            tempTransfer.Add(1.061, -5);
            tempTransfer.Add(1.055, -4);
            tempTransfer.Add(1.05, -3);
            tempTransfer.Add(1.044, -2);
            tempTransfer.Add(1.039, -1);
            tempTransfer.Add(1.034, 0);
            tempTransfer.Add(1.028, 1);
            tempTransfer.Add(1.023, 2);
            tempTransfer.Add(1.017, 3);
            tempTransfer.Add(1.012, 4);
            tempTransfer.Add(1.007, 5);
            tempTransfer.Add(1.001, 6);
            tempTransfer.Add(0.996, 7);
            tempTransfer.Add(0.99, 8);
            tempTransfer.Add(0.985, 9);
            tempTransfer.Add(0.98, 10);
            tempTransfer.Add(0.974, 11);
            tempTransfer.Add(0.969, 12);
            tempTransfer.Add(0.963, 13);
            tempTransfer.Add(0.958, 14);
            tempTransfer.Add(0.952, 15);
            tempTransfer.Add(0.947, 16);
            tempTransfer.Add(0.941, 17);
            tempTransfer.Add(0.936, 18);
            tempTransfer.Add(0.931, 19);
            tempTransfer.Add(0.925, 20);
            tempTransfer.Add(0.92, 21);
            tempTransfer.Add(0.914, 22);
            tempTransfer.Add(0.909, 23);
            tempTransfer.Add(0.903, 24);
            tempTransfer.Add(0.898, 25);
            tempTransfer.Add(0.892, 26);
            tempTransfer.Add(0.887, 27);
            tempTransfer.Add(0.882, 28);
            tempTransfer.Add(0.876, 29);
            tempTransfer.Add(0.871, 30);
            tempTransfer.Add(0.865, 31);
            tempTransfer.Add(0.86, 32);
            tempTransfer.Add(0.854, 33);
            tempTransfer.Add(0.849, 34);
            tempTransfer.Add(0.843, 35);
            tempTransfer.Add(0.838, 36);
            tempTransfer.Add(0.832, 37);
            tempTransfer.Add(0.827, 38);
            tempTransfer.Add(0.821, 39);
            tempTransfer.Add(0.816, 40);
            tempTransfer.Add(0.81, 41);
            tempTransfer.Add(0.804, 42);
            tempTransfer.Add(0.799, 43);
            tempTransfer.Add(0.793, 44);
            tempTransfer.Add(0.788, 45);
            tempTransfer.Add(0.782, 46);
            tempTransfer.Add(0.777, 47);
            tempTransfer.Add(0.771, 48);
            tempTransfer.Add(0.766, 49);
            tempTransfer.Add(0.76, 50);
            tempTransfer.Add(0.754, 51);
            tempTransfer.Add(0.749, 52);
            tempTransfer.Add(0.743, 53);
            tempTransfer.Add(0.738, 54);
            tempTransfer.Add(0.732, 55);
            tempTransfer.Add(0.726, 56);
            tempTransfer.Add(0.721, 57);
            tempTransfer.Add(0.715, 58);
            tempTransfer.Add(0.71, 59);
            tempTransfer.Add(0.704, 60);
            tempTransfer.Add(0.698, 61);
            tempTransfer.Add(0.693, 62);
            tempTransfer.Add(0.687, 63);
            tempTransfer.Add(0.681, 64);
            tempTransfer.Add(0.676, 65);
            tempTransfer.Add(0.67, 66);
            tempTransfer.Add(0.664, 67);
            tempTransfer.Add(0.659, 68);
            tempTransfer.Add(0.653, 69);
            tempTransfer.Add(0.647, 70);
            tempTransfer.Add(0.642, 71);
            tempTransfer.Add(0.636, 72);
            tempTransfer.Add(0.63, 73);
            tempTransfer.Add(0.625, 74);
            tempTransfer.Add(0.619, 75);
            tempTransfer.Add(0.613, 76);
            tempTransfer.Add(0.608, 77);
            tempTransfer.Add(0.602, 78);
            tempTransfer.Add(0.596, 79);
            tempTransfer.Add(0.591, 80);
            tempTransfer.Add(0.585, 81);
            tempTransfer.Add(0.579, 82);
            tempTransfer.Add(0.574, 83);
            tempTransfer.Add(0.568, 84);
            tempTransfer.Add(0.562, 85);
            tempTransfer.Add(0.557, 86);
            tempTransfer.Add(0.551, 87);
            tempTransfer.Add(0.545, 88);
            tempTransfer.Add(0.539, 89);
            tempTransfer.Add(0.534, 90);
            tempTransfer.Add(0.528, 91);
            tempTransfer.Add(0.522, 92);
            tempTransfer.Add(0.517, 93);
            tempTransfer.Add(0.511, 94);
            tempTransfer.Add(0.505, 95);
            tempTransfer.Add(0.499, 96);
            tempTransfer.Add(0.494, 97);
            tempTransfer.Add(0.488, 98);
            tempTransfer.Add(0.482, 99);
            tempTransfer.Add(0.476, 100);
            tempTransfer.Add(0.471, 101);
            tempTransfer.Add(0.465, 102);
            tempTransfer.Add(0.459, 103);
            tempTransfer.Add(0.453, 104);
            tempTransfer.Add(0.448, 105);
            tempTransfer.Add(0.442, 106);
            tempTransfer.Add(0.436, 107);
            tempTransfer.Add(0.43, 108);
            tempTransfer.Add(0.425, 109);
            tempTransfer.Add(0.419, 110);
            tempTransfer.Add(0.413, 111);
            tempTransfer.Add(0.407, 112);
            tempTransfer.Add(0.401, 113);
            tempTransfer.Add(0.396, 114);
            tempTransfer.Add(0.39, 115);
            tempTransfer.Add(0.384, 116);
            tempTransfer.Add(0.378, 117);
            tempTransfer.Add(0.372, 118);
            tempTransfer.Add(0.367, 119);
            tempTransfer.Add(0.361, 120);
            tempTransfer.Add(0.355, 121);
            tempTransfer.Add(0.349, 122);
            tempTransfer.Add(0.343, 123);
            tempTransfer.Add(0.337, 124);
            tempTransfer.Add(0.332, 125);
            tempTransfer.Add(0.326, 126);
            tempTransfer.Add(0.32, 127);
            tempTransfer.Add(0.314, 128);
            tempTransfer.Add(0.308, 129);
            tempTransfer.Add(0.302, 130);
            tempTransfer.Add(0.296, 131);
            tempTransfer.Add(0.291, 132);
            tempTransfer.Add(0.285, 133);
            tempTransfer.Add(0.279, 134);
            tempTransfer.Add(0.273, 135);
            tempTransfer.Add(0.267, 136);
            tempTransfer.Add(0.261, 137);
            tempTransfer.Add(0.255, 138);
            tempTransfer.Add(0.249, 139);
            tempTransfer.Add(0.243, 140);
            tempTransfer.Add(0.237, 141);
            tempTransfer.Add(0.231, 142);
            tempTransfer.Add(0.225, 143);
            tempTransfer.Add(0.219, 144);
            tempTransfer.Add(0.213, 145);
            tempTransfer.Add(0.207, 146);
            tempTransfer.Add(0.201, 147);
            tempTransfer.Add(0.195, 148);
            tempTransfer.Add(0.189, 149);
            tempTransfer.Add(0.183, 150);
        }

    }
}
