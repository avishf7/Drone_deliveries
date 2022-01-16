﻿using DO;
using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BL
{
    public partial class BL : IBL
    {
        public void PackageAssigning(int droneId)
        {
            var dr = dronesList.SingleOrDefault(d => d.Id == droneId);

            if (dr == null ? throw new BlApi.NoNumberFoundException("") : true &&
                dr.DroneStatus != DroneStatuses.Available ? throw new DroneNotAvailableException("") : true)
            {
                var orderPackages = dal.GetPackages().Where(pck => (int)pck.Weight <= (int)dr.MaxWeight)
                                .OrderByDescending(pck => pck.Priority)
                                .ThenByDescending(pck => pck.Weight)
                                .ThenBy(pck => dr.LocationOfDrone.Distance( new()
                                {
                                    Lattitude = dal.GetCustomer(pck.SenderId).Lattitude,
                                    Longitude = dal.GetCustomer(pck.SenderId).Longitude
                                }));

                if (!orderPackages.Any())
                    throw new BlApi.NoSuitablePackageForScheduledException("Packages weighing more than the drone's ability to carry");

                bool isFound = false;//for checking if there is a fit package
                bool isEnoughBattary = true;

                foreach (var pck in orderPackages)
                {
                    if (isFound = pck.Scheduled == null)
                    {
                        DO.Customer sender = dal.GetCustomer(pck.SenderId),
                                         target = dal.GetCustomer(pck.TargetId);

                        Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                                 targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                        double minBattery = BatteryUsage(dr.LocationOfDrone.Distance(senderLocation))
                                          + BatteryUsage(senderLocation.Distance(targetLocation), (int)pck.Weight)
                                          + BatteryUsage(targetLocation.Distance(FindClosestStationLocation(targetLocation)));


                        if (isEnoughBattary = dr.BatteryStatus >= minBattery)
                        {
                            dr.DroneStatus = DroneStatuses.Sendering;
                            dr.PackageNumber = pck.Id;
                            dal.ConnectPackageToDrone(pck.Id, droneId);
                            break;
                        }
                    }
                }

                if (!isEnoughBattary)
                    throw new BlApi.NoSuitablePackageForScheduledException("There is not enough battery", new NotEnoughBattery());

                if (!isFound)
                    throw new BlApi.NoSuitablePackageForScheduledException("There are no packages waiting to be assigned");

                
            }
        }

        public void PickUp(int droneId)
        {
            var dr = dronesList.SingleOrDefault(d => d.Id == droneId);

            if (dr == default)
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            if (dr.PackageNumber == -1)
                throw new NoPackageAssociatedWithDrone("");

            var doPackage = dal.GetPackage(dr.PackageNumber);

            if (doPackage.PickedUp != null)
                throw new PackageAlreadyCollectedException("Package ID - " + doPackage.Id);

            var sender = GetCustomer(doPackage.SenderId);

            if (dr.PackageNumber == doPackage.Id)
            {
                dr.BatteryStatus = dr.BatteryStatus - BatteryUsage(dr.LocationOfDrone.Distance(sender.CustomerLocation));
                dr.LocationOfDrone = sender.CustomerLocation;               
                dal.PickUp(doPackage.Id);
            }
        }

        public void Deliver(int droneId)
        {
            var dr = dronesList.SingleOrDefault(d => d.Id == droneId);

            if (dr == default)
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            if (dr.PackageNumber == -1)
                throw new NoPackageAssociatedWithDrone();

            var doPackage = dal.GetPackage(dr.PackageNumber);

            if (doPackage.PickedUp == null)
                throw new PackageNotCollectedException("Package ID - " + doPackage.Id);

            var target = GetCustomer(doPackage.TargetId);

            if (dr.PackageNumber == doPackage.Id)
            {
                dr.BatteryStatus = dr.BatteryStatus - BatteryUsage(dr.LocationOfDrone.Distance(target.CustomerLocation),
                    (int)doPackage.Weight);
                dr.LocationOfDrone = target.CustomerLocation;
                dr.DroneStatus = DroneStatuses.Available;
                dr.PackageNumber = -1;
                dal.PackageDeliver(doPackage.Id); ;
            }
        }
    }


}