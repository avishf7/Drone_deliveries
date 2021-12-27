using DalApi;
using BlApi;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dal;

namespace BL
{
    sealed partial class BL : IBL
    {
        internal static BL Instance { get; } = new BL();

        IDal dal = DalFactory.GetDal();

        Random rd = new Random();

        List<DroneToList> droneLists = new();


        internal static double DroneAvailable;
        internal static double LightWeight;
        internal static double MediumWeight;
        internal static double HeavyWeight;
        internal static double ChargingRate;

        private BL()
        {
            //Bring the cinfig from DAL
            List<double> tmp = dal.ChargingRequest();
            DroneAvailable = tmp[0];
            LightWeight = tmp[1];
            MediumWeight = tmp[2];
            HeavyWeight = tmp[3];
            ChargingRate = tmp[4];

            List<DO.Drone> drones = dal.GetDrones().ToList();
            List<DO.Package> senderingPackages = dal.GetPackages(x => x.DroneId != -1).ToList();
            List<DO.Package> deliveredPackages = dal.GetPackages(x => x.Delivered != null).ToList();
            List<DO.Station> stations = dal.GetStations().ToList();

            foreach (var drone in drones)
            {
                Location droneLocation = null;
                double minBattery = 0, maxBattery = 100;
                int iPck = senderingPackages.FindIndex(x => x.DroneId == drone.Id);
                DroneStatuses droneStatus = iPck != -1 ? DroneStatuses.Sendering : (DroneStatuses)rd.Next(2);


                switch (droneStatus)
                {
                    case DroneStatuses.Available:
                        DO.Customer randomCustomer = dal.GetCustomer(deliveredPackages[rd.Next(deliveredPackages.Count)].TargetId);

                        droneLocation = new() { Lattitude = randomCustomer.Lattitude, Longitude = randomCustomer.Longitude };
                        minBattery = BatteryUsage(Distance(droneLocation, FindClosestStationLocation(droneLocation)));

                        break;
                    case DroneStatuses.Maintenance:
                        DO.Station randomStation = stations[rd.Next(stations.Count)];

                        droneLocation = new() { Lattitude = randomStation.Lattitude, Longitude = randomStation.Longitude };
                        maxBattery = 20.0;

                        dal.AddDroneCharge(new()
                        {
                            DroneId = drone.Id,
                            StationId = randomStation.Id,
                            ChargeStart = DateTime.Now
                        });
                        break;
                    case DroneStatuses.Sendering:
                        DO.Customer sender = dal.GetCustomer(senderingPackages[iPck].SenderId),
                                         target = dal.GetCustomer(senderingPackages[iPck].TargetId);

                        Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                                 targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                        if (senderingPackages[iPck].PickedUp == null)
                        {
                            droneLocation = FindClosestStationLocation(senderLocation);
                            minBattery = BatteryUsage(Distance(droneLocation, senderLocation))
                                       + BatteryUsage(Distance(senderLocation, targetLocation), (int)senderingPackages[iPck].Weight)
                                       + BatteryUsage(Distance(targetLocation, FindClosestStationLocation(targetLocation)));
                        }
                        else
                        {
                            droneLocation = senderLocation;
                            minBattery = BatteryUsage(Distance(senderLocation, targetLocation), (int)senderingPackages[iPck].Weight)
                                       + BatteryUsage(Distance(targetLocation, FindClosestStationLocation(targetLocation)));
                        }

                        break;
                }


                droneLists.Add(new()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (Weight)(int)drone.MaxWeight,
                    BatteryStatus = rd.NextDouble() * rd.Next((int)(maxBattery - Math.Ceiling(minBattery))) + Math.Ceiling(minBattery),
                    DroneStatus = droneStatus,
                    LocationOfDrone = droneLocation,
                    PackageNumber = (iPck != -1) ? senderingPackages[iPck].Id : iPck
                });
            }
        }

        /// <summary>
        /// Calculate the distance (KM) between two received locations 
        /// according to their coordinates,
        /// Using a distance calculation formula.
        /// </summary>
        /// <param name="sLocation">Start location</param>
        /// <param name="eLocation">End location </param>
        /// <returns>distance (KM) between two received locations</returns>
        static double Distance(Location sLocation, Location eLocation)
        {
            //Converts decimal degrees to radians:
            var rlat1 = Math.PI * sLocation.Lattitude / 180;
            var rlat2 = Math.PI * eLocation.Lattitude / 180;
            var rLon1 = Math.PI * sLocation.Longitude / 180;
            var rLon2 = Math.PI * eLocation.Longitude / 180;
            var theta = sLocation.Longitude - eLocation.Longitude;
            var rtheta = Math.PI * theta / 180;

            //Formula for calculating the distance 
            //between two coordinates represented by radians:
            var dist = (Math.Sin(rlat1) * Math.Sin(rlat2)) + Math.Cos(rlat1) *
                      Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;  //Converts radians to decimal degrees
            dist = dist * 60 * 1.1515;

            return dist * 1.61081082288953;      //Converts to KM
        }

        /// <summary>
        /// Find the station closest to the shipped location
        /// </summary>
        /// <param name="location">Drone's location</param>
        /// <returns> the station closest</returns>
        /// <exception cref="BlApi.NoNumberFoundException"></exception>
        Location FindClosestStationLocation(Location location, Predicate<DO.Station> predicate = null)
        {
            List<DO.Station> stations = dal.GetStations(predicate).ToList();

            if (!stations.Any())
                throw new BlApi.NoNumberFoundException("No station that provided the predicate");

            double minDistance = stations.Min(x => Distance(location, new Location() { Lattitude = x.Lattitude, Longitude = x.Longitude }));

            DO.Station station = stations.Find(x => Distance(location, new Location() { Lattitude = x.Lattitude, Longitude = x.Longitude }) == minDistance);

            return new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude };
        }

        /// <summary>
        /// Calculator Battery consumption by km and weight of the package.
        /// </summary>
        /// <param name="distance">The number of miles the drone has made</param>
        /// <param name="status">What weight the drone carries</param>
        /// <returns>Battery consumption</returns>
        double BatteryUsage(double distance, int status = 3)
        {
            switch ((Weight)status)
            {
                case Weight.Light:
                    return distance * LightWeight;
                case Weight.Medium:
                    return distance * MediumWeight;
                case Weight.Heavy:
                    return distance * HeavyWeight;
                default:
                    return distance * DroneAvailable;
            }
        }
    }
}
