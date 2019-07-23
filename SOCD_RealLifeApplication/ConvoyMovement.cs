using System;
using System.Collections.Generic;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class ConvoyMovement
    {
        /*
         *This method updates the leader time for the leader and increases the total distance traveled, which is equal to the
         *time it takes(in minutes) because we are going 1 mile per minute
         */
        public static void updateTime()
        {
            if (Program.convoy.Count > 1)
            {
                foreach (Vehicle vehicle in Program.convoy)
                {
                    if (vehicle.leader == true)
                    {
                        vehicle.leaderTime += 0.00284;
                    }
                    vehicle.totalDistanceTraveled += 0.00284;
                }
            }
        }
        /*
         *This increments the position of each vehicle in the convoy. One case is when the convoy is at 14999, which then makes the vehicle
         * reset its position to 0.
         */
        public static void moveConvoyPosition()
        {
            for (int i = 0; i < Program.convoy.Count; i++)
            {
                if (Program.convoy[i].position == (Program.numAvailablePosition - 1))
                {
                    Program.convoy[i].position = 0;
                }
                Program.convoy[i].position++;
            }
        }

        /*
         *After a vehicle is removed, a little bit of catch up is necessary to maintain the convoy. The vehicles behind the vehicle that was removed all increment one position.
         * Leader time and total distance traveled also incremented.
         */
        public static void moveConvoyPositionAfterConvoyRemoved(int indexRemoved)
        {
            for(int i = indexRemoved - 1; i >= 0; i--)
            {
                Program.convoy[i].position++;
                if(Program.convoy[i].leader == true)
                {
                    Program.convoy[i].leaderTime += 0.00284;
                }
                Program.convoy[i].totalDistanceTraveled += 0.00284;
            }
        }
    }
}
