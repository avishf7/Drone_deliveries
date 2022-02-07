using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using System.Threading;
using static BL.BL;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json.Serialization;

namespace BL
{

    /// <summary>
    /// A class used by the program to operate the simulator.
    /// </summary>
    class Simulator
    {

        const int DELAY = 1000;
        const double KMS = 2.0;

        /// <summary>
        /// CTOR to active the simulator thread
        /// </summary>
        /// <param name="bl">Variable for access to the functions of the BL logic layer</param>
        /// <param name="SimulatorViewProgress">The process that changes the display in the drone window according to the simulator</param>
        /// <param name="IsRun">Checks if the simulator is still working</param>
        /// <param name="DroneId">The id of the drone</param>
        public Simulator(BL bl, Action SimulatorViewProgress, Func<bool> IsRun, int DroneId)
        {
            var drone = bl.dronesList.SingleOrDefault(x => x.Id == DroneId);

            while (IsRun())
            {
                switch (drone.DroneStatus)
                {
                    case DroneStatuses.Available:
                        SimulatorViewProgress();
                        Thread.Sleep(3 * DELAY);
                        try
                        {
                            bl.PackageAssigning(DroneId);//Try to assign a package to the drone
                            SimulatorViewProgress();
                        }
                        catch (NoSuitablePackageForScheduledException ex)
                        {
                            if (ex.InnerException is NotEnoughBattery || drone.BatteryStatus < 20)
                            {
                                try
                                {
                                    Location stLocation = bl.FindClosestStationLocation(drone.LocationOfDrone, x => x.FreeChargeSlots > 0);
                                    double distance = stLocation.Distance(drone.LocationOfDrone);


                                    if (BatteryUsage(distance) <= drone.BatteryStatus)
                                    {
                                        DO.Station chargeStation;

                                        lock (bl) lock (bl.dal)
                                            {
                                                chargeStation = bl.dal.GetStations(x => x.Lattitude == stLocation.Lattitude && x.Longitude == stLocation.Longitude).SingleOrDefault();
                                                bl.dal.UsingChargingStation(chargeStation.Id);
                                                drone.DroneStatus = DroneStatuses.Maintenance;
                                            }

                                        SimulatorViewProgress();
                                        
                                        DroneMoveSimulator(bl, drone, new() { Lattitude = chargeStation.Lattitude, Longitude = chargeStation.Longitude }, distance, SimulatorViewProgress);

                                        bl.dal.AddDroneCharge(new() { DroneId = DroneId, StationId = chargeStation.Id, ChargeStart = DateTime.Now });

                                        SimulatorViewProgress();
                                    }
                                }
                                catch (BlApi.NoNumberFoundException) { } //if not found charge station try to search again after 4 seconds(start the loop again)
                            }
                        }
                        break;
                    case DroneStatuses.Maintenance:
                        if (drone.BatteryStatus <= 90)
                        {
                            drone.BatteryStatus += ChargingRate / 3600;//charging rate per second
                        }
                        else
                        {
                            lock (bl) lock (bl.dal)
                                {
                                    DO.Station chargeStation = bl.dal.GetStations(x => drone.LocationOfDrone == new Location() { Lattitude = x.Lattitude, Longitude = x.Longitude }).SingleOrDefault();

                                    drone.BatteryStatus = 100;
                                    drone.DroneStatus = DroneStatuses.Available;
                                    bl.dal.RealeseChargingStation(chargeStation.Id);
                                    bl.dal.DeleteDroneCharge(DroneId);
                                }
                        }

                        SimulatorViewProgress();

                        break;
                    case DroneStatuses.Sendering:
                        PackageInTransfer packageInProgress = bl.GetDrone(DroneId).PackageInProgress;

                        if (!packageInProgress.IsCollected)
                        {
                            DroneMoveSimulator(bl, drone,
                                               new()
                                               {
                                                   Lattitude = packageInProgress.CollectionLocation.Lattitude,
                                                   Longitude = packageInProgress.CollectionLocation.Longitude
                                               },
                                               packageInProgress.DistanceToCollectionOrToDestination, SimulatorViewProgress);


                            bl.dal.PickUp(packageInProgress.Id);
                            SimulatorViewProgress();
                        }
                        else
                        {
                            DroneMoveSimulator(bl, drone,
                                               new()
                                               {
                                                   Lattitude = packageInProgress.DeliveryDestinationLocation.Lattitude,
                                                   Longitude = packageInProgress.DeliveryDestinationLocation.Longitude
                                               },
                                               packageInProgress.DistanceToCollectionOrToDestination, SimulatorViewProgress);

                            lock (bl) lock (bl.dal)
                                {
                                    drone.DroneStatus = DroneStatuses.Available;
                                    drone.PackageNumber = -1;
                                    bl.dal.PackageDeliver(packageInProgress.Id);
                                }

                            SimulatorViewProgress();
                        }
                        break;
                }

                Thread.Sleep(DELAY);
            }
        }

        /// <summary>
        /// Function that updates the drone position, distance from the destination and battery level
        /// </summary>
        /// <param name="bl">Variable for access to the functions of the BL logic layer</param>
        /// <param name="drone">The drone for which the test is required</param>
        /// <param name="destantion">The destination of the drone</param>
        /// <param name="KM">The distance between the drone and the destination</param>
        /// <param name="SimulatorViewProgress">The process that changes the display in the drone window according to the simulator</param>
        private static void DroneMoveSimulator(BL bl, DroneToList drone, Location destantion, double KM, Action SimulatorViewProgress)
        {
            double lattitudeMovePerKM = (destantion.Lattitude - drone.LocationOfDrone.Lattitude) / KM;
            double longitudeMovePerKM = (destantion.Longitude - drone.LocationOfDrone.Longitude) / KM;


            for (; KM > 0; KM -= KMS)
            {
                Thread.Sleep(DELAY);

                lock (bl)
                {
                    drone.BatteryStatus -= BatteryUsage(KMS);
                    drone.LocationOfDrone = new()
                    {
                        Lattitude = drone.LocationOfDrone.Lattitude + lattitudeMovePerKM * KMS,
                        Longitude = drone.LocationOfDrone.Longitude + longitudeMovePerKM * KMS
                    };
                }


                SimulatorViewProgress();
            }

            drone.LocationOfDrone = destantion;
        }
    }
}
