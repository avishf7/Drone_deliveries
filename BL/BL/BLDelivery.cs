using IDAL;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BL
{
    public partial class BL : IBl
    {
        public void packageAssigning(int droneId)
        {
            var dr = droneLists.Find(d => d.Id == droneId);

            if (dr == null ? throw new IBL.NoNumberFoundException() : true &&
                dr.DroneStatus != DroneStatuses.Available ? throw new DroneNotAvailableException() : true)
            {
                var orderPackages = dal.GetPackages().Where(pck => (int)pck.Weight <= (int)dr.MaxWeight)
                                .OrderByDescending(pck => pck.Priority)
                                .ThenByDescending(pck => pck.Weight)
                                .ThenBy(pck => Distance(dr.LocationOfDrone, new()
                                {
                                    Lattitude = dal.GetCustomer(pck.SenderId).Lattitude,
                                    Longitude = dal.GetCustomer(pck.SenderId).Longitude
                                }));

                if (!orderPackages.Any())
                    throw new IBL.NoSuitablePackageForScheduledException("Packages weighing more than the drone's ability to carry");

                bool isFound = false;//for checking if there is a fit package
                bool isEnoughBattary = true;

                foreach (var pck in orderPackages)
                {
                    if (isFound = pck.Scheduled == null)
                    {
                        IDAL.DO.Customer sender = dal.GetCustomer(pck.SenderId),
                                         target = dal.GetCustomer(pck.TargetId);

                        Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                                 targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                        double minBattery = BatteryUsage(Distance(dr.LocationOfDrone, senderLocation))
                                          + BatteryUsage(Distance(senderLocation, targetLocation), (int)pck.Weight)
                                          + BatteryUsage(Distance(targetLocation, FindClosestStationLocation(targetLocation)));


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
                    throw new IBL.NoSuitablePackageForScheduledException("There is not enough battery", new NotEnoughBattery());

                if (!isFound)
                    throw new IBL.NoSuitablePackageForScheduledException("There are no packages waiting to be assigned");

                
            }
        }

        public void PickUp(int droneId)
        {
            var dr = droneLists.Find(d => d.Id == droneId);

            if (dr == default)
                throw new IBL.NoNumberFoundException("Drone ID not found");
            if (dr.PackageNumber == -1)
                throw new NoPackageAssociatedWithDrone();

            var doPackage = dal.GetPackage(dr.PackageNumber);

            if (doPackage.PickedUp != null)
                throw new PackageAlreadyCollectedException("Package ID - " + doPackage.Id);

            var sender = GetCustomer(doPackage.SenderId);

            if (dr.PackageNumber == doPackage.Id)
            {
                dr.BatteryStatus = dr.BatteryStatus - BatteryUsage(Distance(dr.LocationOfDrone, sender.CustomerLocation));
                dr.LocationOfDrone = sender.CustomerLocation;               
                dal.PickUp(doPackage.Id);
            }
        }

        public void Deliver(int droneId)
        {
            var dr = droneLists.Find(d => d.Id == droneId);

            if (dr == default)
                throw new IBL.NoNumberFoundException("Drone ID not found");
            if (dr.PackageNumber == -1)
                throw new NoPackageAssociatedWithDrone();

            var doPackage = dal.GetPackage(dr.PackageNumber);

            if (doPackage.PickedUp == null)
                throw new PackageNotCollectedException("Package ID - " + doPackage.Id);

            var target = GetCustomer(doPackage.TargetId);

            if (dr.PackageNumber == doPackage.Id)
            {
                dr.BatteryStatus = dr.BatteryStatus - BatteryUsage(Distance(dr.LocationOfDrone, target.CustomerLocation),
                    (int)doPackage.Weight);
                dr.LocationOfDrone = target.CustomerLocation;
                dr.DroneStatus = DroneStatuses.Available;
                dr.PackageNumber = -1;
                dal.PackageDeliver(doPackage.Id); ;
            }
        }
    }


}