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
            var dr = DroneLists.Find(d => d.Id == droneId);

            if (dr == null ? throw new IBL.NoNumberFoundException() : true &&
                dr.DroneStatus != DroneStatuses.AVAILABLE ? throw new DroneNotAvailableException() : true)
            {
                var orderPackages = dal.GetPackages().Where(pck => (int)pck.Weight <= (int)dr.MaxWeight)
                                .OrderByDescending(pck => pck.Priority)
                                .ThenByDescending(pck => pck.Weight)
                                .ThenBy(pck => Distance(dr.LocationOfDrone, new()
                                {
                                    Lattitude = dal.GetCustomer(pck.SenderId).Lattitude,
                                    Longitude = dal.GetCustomer(pck.SenderId).Longitude
                                }));

                bool isFound = false;//for checking if there is a fit package

                foreach (var pck in orderPackages)
                {
                    IDAL.DO.Customer sender = dal.GetCustomer(pck.SenderId),
                                     target = dal.GetCustomer(pck.TargetId);

                    Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                             targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                    double minBattery = BatteryUsage(Distance(dr.LocationOfDrone, senderLocation))
                                      + BatteryUsage(Distance(senderLocation, targetLocation), (int)pck.Weight)
                                      + BatteryUsage(Distance(targetLocation, FindClosestStationLocation(targetLocation)));

                    if (isFound = dr.BatteryStatus >= minBattery)
                    {
                        dr.DroneStatus = DroneStatuses.SENDERING;
                        var scheduledPck = pck;
                        scheduledPck.DroneId = dr.Id;
                        scheduledPck.Scheduled = DateTime.Now;
                        dal.UpdatePackage(scheduledPck);
                        break;
                    }
                }

                if (!isFound)
                    throw new IBL.NoSuitablePackageForScheduledException();
            }
        }

        public void PickUp(int droneId)
        {
        //    var dr = DroneLists.Find(d => d.Id == droneId);

            int index= DroneLists.FindIndex(d => d.Id == droneId);

            var DoPackage = dal.GetPackages(p => p.DroneId == droneId && p.PickedUp == DateTime.MinValue).First();
            //   var PackageInDrone = DoPackage.Find(p => p.DroneId == droneId && p.PickedUp==DateTime.MinValue);

            if (DoPackage.DroneId==0)
            {
                throw new NoPackageAssociatedWithDrone();
            }
            var sender = dal.GetCustomer(DoPackage.SenderId);
            Location senderLocation = new()
            {
                Lattitude = sender.Lattitude,
                Longitude = sender.Longitude
            };

           double Km = Distance(DroneLists[index].LocationOfDrone, senderLocation);

            if (DroneLists[index].PackageNumber== DoPackage.Id)
            {
                DroneLists[index].LocationOfDrone = senderLocation;
                DroneLists[index].BatteryStatus = BatteryUsage(Km, 3);
                DoPackage.PickedUp = DateTime.Now;
                //כך מעדכנים את החבילה?
                dal.AddPackage(DoPackage);
            }
        }

        public void Deliver(int droneId)
        {
            throw new NotImplementedException();
        }
    }


}