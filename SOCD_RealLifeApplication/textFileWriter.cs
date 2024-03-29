﻿/*This manages the text file writiing. It contains 2 classes for compiling the data and then writing to the text file.*/
using System;
using System.IO;
using System.Threading;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class TextFileWriter
    {
        /*
         *Compiles the data for the specified vehicles and creates an array from it.
         */
        public static string[] compileData()
        {
            string[] arrayData = new string[102];
            arrayData[0] = "Categories, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25";
            for(int i = 0; i < Program.vehicleList.Length; i++)
            {
                arrayData[i + 1] = "Series " + (i + 1);
                var arrayOfVehicleRatioMeans = Calculations.createTextArrayForTextFile(Program.vehicleList[i].vehicleCalculatedRatios);
                foreach (var ratio in arrayOfVehicleRatioMeans) {
                    arrayData[i + 1] += ", " + ratio;
                } 
            }

            return arrayData;
        }
        /*
         *Writes to a text file from the array that was compiled. 
         */
        public static void textWriter(string[] dataArray)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            /// Users / vinayshukla / Projects / SOCD_RealLifeApplication / SOCD_Graphing_Data.txt
            StreamWriter sw = new StreamWriter("/Users/vinayshukla/Projects/SOCD_RealLifeApplication/SOCD_Graphing_Data.txt");
            foreach (var data in dataArray)
            {
                sw.WriteLine(data);
            }
            sw.Close();
        }
    }
}
