/*
 *This is in charge of creating our environment. More specifically, the random list and the entry points.
 */
using System;
using System.Collections.Generic;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class RandomList
    {
        /*
         *Creates a random list of vehicles that are part of a convoy that have randomized positions and specific ID's.
         */
        public static void createRandomList()
        {
            for (int i = 0; i < Program.numConvoy; i++)
            {
                var rand = new Random();
                var randomPosition = (rand.Next(1000) * 150);
                Program.vehicleList[i] = new Vehicle
                {
                    Id = i,
                    position = (randomPosition),
                    tripDuration = rand.Next(1, 1000),
                    leader = false,
                    leaderTime = 0,
                    numberOfConvoysParticipated = 0,
                    vehicleCalculationData = new List<VehicleExpectedAndActualData>(),
                    vehicleCalculatedRatios = new List<VehicleCalculatedRatios>()
                };
            }
        }
        /*
         *Places vehicles in the entry points in the Circle track based upon their position.
         */
        public static void createEntryPoints()
        {
            //create entry points
            for (int i = 0; i < Program.numAvailablePosition; i++)
            {
                Program.circleTrack[i] = new List<Vehicle>();
                foreach (Vehicle vehicle in Program.vehicleList)
                {
                    if (vehicle.position == i)
                    {
                        Program.circleTrack[i].Add(vehicle);
                    }
                }
            }
        }
    }
}
