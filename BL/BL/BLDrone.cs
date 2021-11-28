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
            catch (IDAL.NoNumberFoundException ex) { throw new IBL.NoNumberFoundException("Station ID not found", ex); }
            catch (IDAL.ExistsNumberException ex) { throw new IBL.ExistsNumberException("Drone already exists", ex); }
        }

        public void UpdateDrone(int droneId, string model)
        {
            try
            {
                IDAL.DO.Drone dr = dal.GetDrone(droneId);

                dr.Model = model;
                dal.UpdateDrone(dr);

                DroneLists.Find(drone => drone.Id == droneId).Model = model;
            }
            catch (IDAL.NoNumberFoundException ex) { throw new IBL.NoNumberFoundException("Drone ID not found", ex); }
        
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
                    throw new IBL.NoNumberFoundException();
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

            public IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null)
            {
                List<DroneToList> copyOfDroneLists = new();

                foreach (var item in DroneLists)
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
                throw new NotImplementedException();
            }
        }
    }
