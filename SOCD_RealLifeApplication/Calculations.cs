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
                vehicle.vehicleCalculationData[vehicle.vehicleCalculationData.Count - 1].expectedWork = (vehicle.totalDistanceTraveled / (Program.convoy.Count));
                vehicle.totalDistanceTraveled = 0;
            }
        }
        /*
         *This takes the actual leader time for when a respective vehicle leaves the convoy. Then the ratio is calculated and stored into
         *an array along with the total expected and total actual work done
         */
        public static void setActualTime()
        {
            foreach (Vehicle vehicle in Program.convoy)
            {
                vehicle.vehicleCalculationData[vehicle.vehicleCalculationData.Count - 1].actualWorkDone = vehicle.leaderTime;
                vehicle.leaderTime = 0;
            }
        }
        /*
         *This takes the expected and actual work done and builds a ratio of acutal over expected.
         */
       public static void calculateRatio(Vehicle leavingVehicle)
        {
            var totalExpected = 0.0;
            var totalActual = 0.0;
            leavingVehicle.vehicleCalculatedRatios.Add(new VehicleCalculatedRatios());
            foreach(VehicleExpectedAndActualData dataValue in leavingVehicle.vehicleCalculationData) {
                if(leavingVehicle.numberOfConvoysParticipated == dataValue.convoyNumber)
                {
                    totalExpected += dataValue.expectedWork;
                    totalActual += dataValue.actualWorkDone;
                }
            }
            leavingVehicle.vehicleCalculatedRatios[leavingVehicle.numberOfConvoysParticipated].calculatedRatio =
            (totalActual / totalExpected);
            leavingVehicle.vehicleCalculatedRatios[leavingVehicle.numberOfConvoysParticipated].ratioConvoyNumber =
            leavingVehicle.numberOfConvoysParticipated;
            
        }

        public static void updateExpectedandActual()
        {                
            if (Program.convoy.Count > 1)
            {
                foreach (Vehicle vehicles in Program.convoy)
                {
                    vehicles.vehicleCalculationData.Add(new VehicleExpectedAndActualData());
                }

                updateProportionalExepectedDistance();
                setActualTime();
                
                foreach (Vehicle vehicles in Program.convoy)
                {
                    vehicles.vehicleCalculationData[vehicles.vehicleCalculationData.Count - 1].convoyNumber = vehicles.numberOfConvoysParticipated;
                }
            }
        }

        public static void findGreatestAndLeast(List<VehicleRatioAndVehicleID> arrayOfRatiosAndVehicles)
        {
            var greatest = arrayOfRatiosAndVehicles[0].ratio;
            var least = arrayOfRatiosAndVehicles[0].ratio;

            foreach (var ratioAndVehicleObj in arrayOfRatiosAndVehicles)
            {
                if (ratioAndVehicleObj.ratio > greatest)
                {
                    greatest = ratioAndVehicleObj.ratio;
                }

                if (ratioAndVehicleObj.ratio < least)
                {
                    least = ratioAndVehicleObj.ratio;
                }
            }
        }

        public static double[] createTextArrayForTextFile(List<VehicleCalculatedRatios> ratioList)
        {
            double[] returnedArrayOfAveragedRatios = new double[ratioList.Count];
            for (int i = 0; i < ratioList.Count; i++)
            {
                var total = 0.0;
                for(int index = 0; index <= i; index++)
                {
                    total += ratioList[index].calculatedRatio;
                }
                var average = (total / (i + 1));
                returnedArrayOfAveragedRatios[i] = average;
            }

            return returnedArrayOfAveragedRatios;
        }
    }
}
