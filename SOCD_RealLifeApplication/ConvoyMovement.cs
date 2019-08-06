/*
 *Controls the movement of the convoy.
 */
using System;
using System.Collections.Generic;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class ConvoyMovement
    {
        //minutes or miles
        static double travelForOneSpace = 0.00284;
        /* 
         *This increments the position of each vehicle in the convoy. One case is when the convoy is at 14999, which then makes the vehicle
         * reset its position to 0.
         */
        public static void moveConvoyPosition()
        {
            for (int i = 0; i < Program.convoy.Count; i++)
            {
                moveSingleConvoy(Program.convoy[i]);
            }
        }

        /*
         *This is in charge o moving a singular vehicle, updating its poisiton as well as total distance traveled
         * if applicable.
         */
        public static void moveSingleConvoy(Vehicle vehicle)
        {  
            if (vehicle.position == (Program.numAvailablePosition - 1))
            {
                vehicle.position = 0;
            }
            else
            {
                vehicle.position++;
            }
            if (Program.convoy.Count > 1)
            {
                if (vehicle.leader == true)
                {
                    vehicle.leaderTime += travelForOneSpace;
                }
                vehicle.totalDistanceTraveled += travelForOneSpace;
            }
        }

        /*
         *After a vehicle is removed, a little bit of catch up is necessary to maintain the convoy. The vehicles behind the vehicle that was removed all increment one position.
         * Leader time and total distance traveled also incremented.
         */
        public static void moveConvoyPositionAfterConvoyRemoved(int indexRemoved)
        { 
            for (int i = indexRemoved - 1; i >= 0; i--)
            {
                moveSingleConvoy(Program.convoy[i]);
            }
        }
    }
}
