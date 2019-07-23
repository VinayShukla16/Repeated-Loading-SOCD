using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class TripDuration
    {
        public static void updateTripDuration(Vehicle vehicle)
        {
            vehicle.tripDuration--;
        }

        public static void checkTripDuration(Vehicle vehicle)
        { 
            if (vehicle.tripDuration == 0)
            {
                if (Program.convoy.IndexOf(vehicle) == (Program.convoy.Count - 1))
                {
                    SelectLeader.resetLeader();
                }
                placeVehicleBackInCircleTrack(vehicle);
                ConvoyMovement.moveConvoyPositionAfterConvoyRemoved(Program.convoy.IndexOf(vehicle));
                Calculations.updateProportionalExepectedDistance();
                Calculations.setActualTime(vehicle);
                vehicle.vehicleCalculationData.Add(new VehicleExpectedAndActualData());
                Program.convoy.Remove(vehicle);
            }
        }

        public static void placeVehicleBackInCircleTrack(Vehicle vehicle)
        {
            Random rand = new Random();
            vehicle.tripDuration = rand.Next(1, 1000);
            Program.circleTrack[vehicle.position].Add(vehicle);
        }
    }
}
