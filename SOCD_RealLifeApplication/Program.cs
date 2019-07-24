/*
 *Main program to help direct the flow. It creates the random list and then starts the game.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections;
using SOCD_RealLifeApplication;

namespace SOCD_Algorith_System
{
    class Program
    {
        public static int numConvoy = 100;
        public static int convoysInPositions = 2;
        public static int numAvailablePosition = 150000;
        public static List<Vehicle> convoy = new List<Vehicle>(100);
        public static Vehicle[] vehicleList = new Vehicle[100];
        public static List<Vehicle>[] circleTrack = new List<Vehicle>[150000];

        /*
         *Main function that calls create list and then starts game.
         */
        static void Main(string[] args)
        {
            createRandomList();
            startGame();
        }

        /*
         *This creates the random list of vehicles.
         */
        private static void createRandomList()
        {
            RandomList.createRandomList();
            RandomList.createEntryPoints();
        }
        //start the game
        private static void startGame()
        {
            var startingPosition = SelectLeader.selectFirstLeader();
            RepeatedLoading.startRepeatedLoading(startingPosition);
        }
    }
}
