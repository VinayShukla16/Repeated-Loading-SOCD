/*
 *This class exists within each vehicle class as a type list and stores expedted, and actual work done for a certain
 *convoy that a vehicle participated in.
 */
using System;
namespace SOCD_RealLifeApplication
{
    public class VehicleExpectedAndActualData
    {
        public double expectedWork { get; set; }
        public double actualWorkDone { get; set; }
        public double calculatedRatio { get; set; }
    }
}
