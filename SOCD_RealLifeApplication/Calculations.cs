/*
 *This class is in charge of all the computations that occur with vehicles.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class Calculations
    {
        /*
         *This updates the proportional expected distance after the length of the convoy changes.
         */
        public static void updateProportionalExepectedDistance()
        {
            foreach (Vehicle vehicle in Program.convoy)
            {
                vehicle.vehicleCalculationData[vehicle.vehicleCalculationData.Count - 1].expectedWork += (vehicle.totalDistanceTraveled / (Program.convoy.Count));
                vehicle.totalDistanceTraveled = 0;
            }
        }
        /*
         *This takes the actual leader time for when a respective vehicle leaves the convoy. Then the ratio is calculated and stored into
         * an array along with the total expected and total actual work done
         */
        public static void setActualTime(Vehicle leavingVehicle)
        {
            leavingVehicle.vehicleCalculationData[leavingVehicle.vehicleCalculationData.Count - 1].actualWorkDone = leavingVehicle.leaderTime;
            leavingVehicle.numberOfConvoysParticipated++;
            leavingVehicle.leaderTime = 0;
            calculateRatio(leavingVehicle);
        }
         
       public static void calculateRatio(Vehicle leavingVehicle)
        {
            leavingVehicle.vehicleCalculationData[leavingVehicle.vehicleCalculationData.Count - 1].calculatedRatio =
            leavingVehicle.vehicleCalculationData[leavingVehicle.vehicleCalculationData.Count - 1].actualWorkDone /
            leavingVehicle.vehicleCalculationData[leavingVehicle.vehicleCalculationData.Count - 1].expectedWork;

        }

        public static double[] createTextArrayForTextFile(List<VehicleExpectedAndActualData> ratioList)
        {
            double[] returnedArrayOfAveragedRatios = new double[ratioList.Count];
            Console.WriteLine("Creating text array");
            for (int i = 0; i < ratioList.Count; i++)
            {
                var total = 0.0;
                for(int index = 0; index < i; index++)
                {
                    total += ratioList[index].calculatedRatio;
                }
                var average = total / (i + 1);
                returnedArrayOfAveragedRatios[i] = average;
            }

            return returnedArrayOfAveragedRatios;
        }

        public static void displayData(double ratio)
        {
            //Console.WriteLine("{0:0.00000000}", ratio);
            //Console.WriteLine(Program.convoy.Count);
        }
    }
}
