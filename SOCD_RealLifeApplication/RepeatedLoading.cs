/*
 *This is the actual repeated loading game, where we have a a huge loop that loops from 0 to 149999 and resets whenever
 * we hit index 149999. Essentially, this simulates a convoy or vehicle going around a track with 150000 spaces. There
 * are 4 "cases of motion" that are adressed in this repeated loading simulation.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using SOCD_Algorith_System;

namespace SOCD_RealLifeApplication
{
    public class RepeatedLoading{

        public static void startRepeatedLoading(int startingPosition)
        {
            var currentEntryPoint = startingPosition;
            int counter = 0;
            var rand = new Random();
            /*
             *We start at the starting position, which is the position of the first vehicle and is also our first entry point. Thus,
             * currentEntryPoint is set to our starting Position.
             */
            for (int i = startingPosition; i <= Program.numAvailablePosition; i++)
            {
                /*
                 *First case is if we are at index 149999, at that point, we set i to 0 and then we name our currentEntryPoint as 0 because
                 * that is our currentEntryPoint.
                 */
                 if(i == Program.numAvailablePosition)
                {
                    i = 0;
                }

                if ((i == Program.numAvailablePosition - 1))
                {
                    currentEntryPoint = 0;

                    /*foreach(Vehicle vehicle in Program.convoy)
                    {
                        if (vehicle.leader == true)
                        {
                            Console.WriteLine(vehicle.Id + ": " + "leader");
                        }
                        else
                        {
                            Console.WriteLine(vehicle.Id + ": " + "notleader");
                        }
                    }
                    Thread.Sleep(500);
                    Console.WriteLine("");
                    /*
                     *Don't mind this counter stuff too much, it is merely used to generate the txt file. It is hacked together and I wanted
                     * the convoy to go for a bit before taking data so i put it to 200 loops around the track.
                     */
                    if(counter == 2000)
                    {
                        foreach(Vehicle vehicle in Program.vehicleList)
                        {
                            if (!Program.convoy.Contains(vehicle))
                            {
                                foreach (var element in vehicle.vehicleCalculationData)
                                {
                                    Console.WriteLine(vehicle.Id + "-" + element.convoyNumber + ": " + element.actualWorkDone + "+" + element.expectedWork);
                                }
                                Console.WriteLine("");
                            }
                        }
                        var array = textFileWriter.compileData();
                        textFileWriter.textWriter(array);
                    }
                    counter++;
                    Console.WriteLine(counter);
                    /*
                     *If there aren't any vehicles in the currentEntryPoint, 0, then we aren't going to bother with the whole adding process. However, if there are,
                     * we want to loop through all of the vehicles at that entry point and generate a random number to determine if we are going to add each of the
                     * vehicles at that entry point into our convoy. 
                     */
                    if (Program.circleTrack[0].Count != 0)
                    {
                        var totalAdded = 0;

                        //MOVE TO SEPERATE FUNCTION
                        //Loops through each vehicle in the entry point
                        for (int vehicleNumber = 0; vehicleNumber < Program.circleTrack[0].Count; vehicleNumber++)
                        {
                            //1/10 chance for us to add the specified vehicle.
                            if (NormalDistribution.probability() == 1)
                            {
                                Calculations.updateExpectedandActual();
                                Program.convoy.Add(Program.circleTrack[0][vehicleNumber]);
                                SelectLeader.resetLeader();
                                for (int index = 0; index < totalAdded; index++)
                                {
                                    ConvoyMovement.moveSingleConvoy(Program.circleTrack[0][vehicleNumber]);
                                }
                                Program.circleTrack[0].RemoveAt(vehicleNumber);
                                totalAdded++;
                            }
                        }
                        //When we don't add any vehicles
                        if (totalAdded == 0)
                        {
                            moveAndUpdateVehiclePosition(currentEntryPoint);
                        }
                    }
                    //If there aren't any vehicles at the entry point, 0
                    else
                    {
                        moveAndUpdateVehiclePosition(currentEntryPoint);
                    }
                }
                /*
                 *We looka at the index head and if it is divisble by 150, then we know that it is an entry point. This is because entry points are spaced
                 *out by 150 starting at 0. This is very similar to the last case and more or less, this is the if statement that will be executed rather than the
                 * other one because this encompasses all entry points besides 0.
                 */
                else if (((i + 1) % 150) == 0)
                {
                    //We set the current entry point as the index ahead because that is what we are looking at.
                    currentEntryPoint = i + 1;
                    /*foreach(Vehicle vehicle in Program.convoy)
                    {
                        if (vehicle.leader == true)
                        {
                            Console.WriteLine(vehicle.Id + ": " + "leader");
                        }
                        else
                        {
                            Console.WriteLine(vehicle.Id + ": " + "notleader");
                        }
                    }
                    Thread.Sleep(10);
                    Console.WriteLine("");*/

                if (Program.circleTrack[i + 1].Count != 0)
                {
                    var totalAdded = 0;
                    /*\
                     *Loops through the vehicles of the currentEntryPoint, i + 1. It decides wether or not to add based on a 1/10
                     * probability.
                     */
                    for (int vehicleNumber = 0; vehicleNumber < Program.circleTrack[i + 1].Count; vehicleNumber++)
                        {
                            if (NormalDistribution.probability() == 1)
                            {
                                Calculations.updateExpectedandActual();
                                Program.convoy.Add(Program.circleTrack[i + 1][vehicleNumber]);
                                SelectLeader.resetLeader();
                                for (int index = 0; index < totalAdded; index++)
                                {
                                    ConvoyMovement.moveSingleConvoy(Program.circleTrack[i + 1][vehicleNumber]);
                                }
                                Program.circleTrack[i + 1].RemoveAt(vehicleNumber);
                                totalAdded++;
                            }
                        }
                        if(totalAdded == 0)
                        {
                            moveAndUpdateVehiclePosition(currentEntryPoint);
                        }
                    }
                    else
                    {
                        moveAndUpdateVehiclePosition(currentEntryPoint);
                    }
                }
                /*
                 *Case after we pass an entry point. What happens is that it allows each vehicle in the convoy update and check trip duration and handles each vehicle
                 *individually. This means that when a specific vehicle is at an entry point, its trip duration is subtracted and its trip duration is checked as well.
                 */
                else if((Program.convoy.FindIndex(vehicle => vehicle.position == currentEntryPoint)) >= 0)
                {
                    moveAndUpdateVehiclePosition(currentEntryPoint);
                }
                /*
                 *This is just when
                 * the convoy is in between entry points and just moving and updating leader time and convoy positions.
                 */
                else
                {
                    ConvoyMovement.moveConvoyPosition();
                    ConvoyMovement.updateTime();
                }
            }
        }

        /*
         *This function moves the convoy position and tests if there is any vehicle at the entry point after moving. If there is, then we update and
         * check tripduration for the vehicle at the entry point.
         */
        public static void moveAndUpdateVehiclePosition(int currentEntryPoint)
        {
            ConvoyMovement.moveConvoyPosition();
            ConvoyMovement.updateTime();
            if ((Program.convoy.FindIndex(vehicle => vehicle.position == currentEntryPoint)) >= 0)
            {
                TripDuration.updateTripDuration(Program.convoy[(Program.convoy.FindIndex(vehicle => vehicle.position == currentEntryPoint))]);
                TripDuration.checkTripDuration(Program.convoy[(Program.convoy.FindIndex(vehicle => vehicle.position == currentEntryPoint))]);
            }
        }

    }
}
