﻿/*
 *This class manages all of the trip duration functions and operations.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class TripDuration
    {
        /*
         *This function is responsible for updating a vehicle's 
         */
        public static void updateTripDuration(Vehicle vehicle)
        {
            vehicle.tripDuration--;
        }
        /*
         *This function is responsible for checking if a specific vehicle has reached a trip duration of 0 and thus has finished
         *its time. Following suchm a series of commands are utilized to place the vehicle back in the circle track and update its
         *internal data values.
         */
        public static void checkTripDuration(Vehicle vehicle)
        { 
            if (vehicle.tripDuration == 0)
            {
                placeVehicleBackInCircleTrack(vehicle);
                //Catch up
                int index = Program.convoy.IndexOf(vehicle);
                vehicle.leader = false;
                Program.convoy.Remove(vehicle);
                SelectLeader.resetLeader();
                ConvoyMovement.moveConvoyPositionAfterConvoyRemoved(index);
                Calculations.updateExpectedandActual();
                Calculations.calculateRatio(vehicle);
                vehicle.numberOfConvoysParticipated++;

            } 
            
        }
        /*
         *This is just a simple method used to place a vehicle back into the circle track and randomize its trip duration.
         */
        public static void placeVehicleBackInCircleTrack(Vehicle vehicle)
        {
            Random rand = new Random();
            vehicle.tripDuration = rand.Next(1, 1000);
            Program.circleTrack[vehicle.position].Add(vehicle);
        }
    }
}
