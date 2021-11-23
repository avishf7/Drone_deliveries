using IDAL;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBl
    {
        public void AddDrone(Drone drone, int staionId)
        {
            try
            {
                List<IDAL.DO.Station> stations = dal.GetStations().ToList();
                IDAL.DO.Station st = stations.Find(x => x.Id == staionId);
                dal.AddDrone(new IDAL.DO.Drone
                {
                    Id = drone.Id,
                    MaxWeight = (IDAL.DO.Weight)drone.MaxWeight,
                    Model = drone.Model
                });

                DroneLists.Add(new()
                {
                    Id = drone.Id,
                    MaxWeight = drone.MaxWeight,
                    Model = drone.Model,
                    BatteryStatus = rd.NextDouble() * rd.Next(20) + 20,
                    DroneStatus = DroneStatuses.MAINTENANCE,
                    LocationOfDrone = new() { Lattitude = st.Lattitude, Longitude = st.Longitude },
                    PackageNumber = -1
                });

                dal.UsingChargingStation(st.Id);

            }
            catch (IDAL.NoNumberFoundException ex) { throw new NoNumberFoundException("Station ID not found",ex);}
            catch (ExistsNumberException ex) { throw new ExistsNumberException("Drone already exists", ex); }
        }

        public void UpdateDrone(int droneId, string model)
        {
            try
            {
                List<IDAL.DO.Drone> DroneT = dal.GetDrones().ToList();
                IDAL.DO.Drone dr = DroneT.Find(x => x.Id == droneId);

                dr.Model = model;
                dal.UpdateDrone(dr);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Drone GetDrone(int droneId)
        { 
            var dr = DroneLists.Find(x => x.Id == droneId);
            PackageInTransfer packageInTransfer = null;

            if (dr != null)
            {
                if (dr.DroneStatus == DroneStatuses.SENDERING)
                {
                    IDAL.DO.Package package = dal.GetPackage(dr.PackageNumber);
                    IDAL.DO.Customer sender = dal.GetCustomer(package.SenderId),
                                     target = dal.GetCustomer(package.TargetId);
                    Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                             targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                    packageInTransfer = new PackageInTransfer()
                    {
                        Id = package.Id,
                        IsCollected = package.PickedUp != DateTime.MinValue,
                        Priority = (Priorities)package.Priority,
                        SenderCustomerInPackage = new() { CustomerId = sender.Id, CustomerName = sender.Name },
                        TargetCustomerInPackage = new() { CustomerId = target.Id, CustomerName = target.Name },
                        CollectionLocation = senderLocation,
                        DeliveryDestinationLocation = targetLocation,
                        DistanceCollectionToDestination = Distance(senderLocation, targetLocation)
                    };
                }
            }
            else
            {
                //לא צריך לשלוח חריגה אלאלשלוח את כל הרחפן ללא הישות או עם ישות מאותחלת לNULL
                throw new NoNumberFoundException();
            }

            return new()
            {
                Id = dr.Id,
                Model = dr.Model,
                MaxWeight = dr.MaxWeight,
                BatteryStatus = dr.BatteryStatus,
                DroneStatus = dr.DroneStatus,
                DeliveryInProgress = packageInTransfer,
                LocationOfDrone = dr.LocationOfDrone
            };
        }

        public IEnumerable<DroneToList> GetDrones(Predicate<Drone> predicate = null)
        {
            var dr = DroneLists;
            List<DroneToList> BoDronesLists = new();

            foreach (var item in DroneLists)
            {
                BoDronesLists.Add(new()
                {
                    Id=item.Id,
                    Model=item.Model,
                    MaxWeight=item.MaxWeight,
                    BatteryStatus=item.BatteryStatus,
                    DroneStatus=item.DroneStatus,
                    LocationOfDrone=item.LocationOfDrone,
                    PackageNumber=

                });
            }
            return BoDronesLists;

        }

        public void DeleteDrone(int id)
        {
            throw new NotImplementedException();
        }
    }
}
