/*
 *This class helps to just organize the properties of each vehicle.
 */
using System;
using System.Collections.Generic;

namespace SOCD_RealLifeApplication
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int position { get; set; }
        public int tripDuration { get; set; }
        public bool leader { get; set; }
        public double leaderTime { get; set; }
        public int totalVehiclesDuringLeader { get; set; }
        public int numberOfConvoysParticipated { get; set; }
        public double totalDistanceTraveled { get; set; }
        public List<VehicleExpectedAndActualData> vehicleCalculationData { get; set; }
        public int numberOfTimesCalled { get; set; }
    }
}
