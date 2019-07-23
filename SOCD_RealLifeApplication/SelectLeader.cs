/*
 *This class is in charge of all leader setting or selecting first leader.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class SelectLeader
    {
        /*
         *This is in charge of selecting the first leader of the convoy.
         */
        public static int selectFirstLeader()
        {
            var randomNumber = 0;
            while (Program.convoy.ElementAtOrDefault(0) == null)
            {
                var rand = new Random();
                randomNumber = (rand.Next(1000) * 150);
                if (Program.circleTrack[randomNumber].Count != 0)
                {
                    var randomConvoy = rand.Next(0, Program.circleTrack[randomNumber].Count);
                    Program.convoy.Add(Program.circleTrack[randomNumber][randomConvoy]);
                    Program.convoy[0].leader = true;
                    Program.circleTrack[randomNumber].RemoveAt(randomConvoy);
                }
            }
            return randomNumber;
        }
        /*
         *This resets the leader by looping through the convoy and changing the last vehicle in the convoy into the leader. (The last vehicle
         * would have the farthest position anyways.)
         */
        public static void resetLeader()
        {
            foreach (Vehicle vehicle in Program.convoy)
            {
                vehicle.leader = false;
                Program.convoy[Program.convoy.Count - 1].leader = true;
            }
        }
    }
}
