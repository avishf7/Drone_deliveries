using DalApi;
using BlApi;
using BlApi.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {

        public void AddDrone(Drone drone, int staionId)
        {
            try
            {

                DO.Station st = dal.GetStation(staionId);
                dal.AddDrone(new DO.Drone
                {
                    Id = drone.Id,
                    MaxWeight = (DO.Weight)drone.MaxWeight,
                    Model = drone.Model
                });

                droneLists.Add(new()
                {
                    Id = drone.Id,
                    MaxWeight = drone.MaxWeight,
                    Model = drone.Model,
                    BatteryStatus = rd.NextDouble() * rd.Next(20) + 20,
                    DroneStatus = DroneStatuses.Maintenance,
                    LocationOfDrone = new() { Lattitude = st.Lattitude, Longitude = st.Longitude },
                    PackageNumber = -1
                });

                dal.UsingChargingStation(staionId);

            }
            catch (DalApi.NoNumberFoundException ex) { throw new BlApi.NoNumberFoundException("Station ID not found", ex); }
            catch (DalApi.ExistsNumberException ex) { throw new BlApi.ExistsNumberException("Drone already exists", ex); }
        }

        public void UpdateDrone(int droneId, string model)
        {
            try
            {
                DO.Drone dr = dal.GetDrone(droneId);

                dr.Model = model;
                dal.UpdateDrone(dr);

                droneLists.Find(drone => drone.Id == droneId).Model = model;
            }
            catch (DalApi.NoNumberFoundException ex) { throw new BlApi.NoNumberFoundException("Drone ID not found", ex); }
        
        }

            public Drone GetDrone(int droneId)
            {
                var dr = droneLists.Find(x => x.Id == droneId);
                PackageInTransfer packageInTransfer = null;

                if (dr != null)
                {
                    if (dr.DroneStatus == DroneStatuses.Sendering)
                    {
                        DO.Package package = dal.GetPackage(dr.PackageNumber);
                        DO.Customer sender = dal.GetCustomer(package.SenderId),
                                         target = dal.GetCustomer(package.TargetId);
                        Location senderLocation = new() { Lattitude = sender.Lattitude, Longitude = sender.Longitude },
                                 targetLocation = new() { Lattitude = target.Lattitude, Longitude = target.Longitude };

                        packageInTransfer = new PackageInTransfer()
                        {
                            Id = package.Id,
                            Weight = (Weight)package.Weight,
                            IsCollected = package.PickedUp != null,
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
                    throw new BlApi.NoNumberFoundException("");
                }

                return new()
                {
                    Id = dr.Id,
                    Model = dr.Model,
                    MaxWeight = dr.MaxWeight,
                    BatteryStatus = dr.BatteryStatus,
                    DroneStatus = dr.DroneStatus,
                    PackageInProgress = packageInTransfer,
                    LocationOfDrone = dr.LocationOfDrone
                };
            }

            public IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null)
            {
                List<DroneToList> copyOfDroneLists = new();

                foreach (var item in droneLists)
                {
                    if (predicate != null ? predicate(item) : true)
                        copyOfDroneLists.Add(new()
                        {
                            Id = item.Id,
                            Model = item.Model,
                            MaxWeight = item.MaxWeight,
                            BatteryStatus = item.BatteryStatus,
                            DroneStatus = item.DroneStatus,
                            LocationOfDrone = item.LocationOfDrone,
                            PackageNumber = item.PackageNumber,
                        });
                }

                return copyOfDroneLists;
            }


            public void DeleteDrone(int id)
            {
            try
            {
                dal.DeleteDrone(id);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            }
              
        }
    }
