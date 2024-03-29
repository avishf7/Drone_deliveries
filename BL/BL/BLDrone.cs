﻿using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BL
{
    sealed partial class BL : IBL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone, int stationId)
        {
            try
            {
                lock (dal)
                {
                    DO.Station st = dal.GetStation(stationId);
                    dal.AddDrone(new DO.Drone
                    {
                        Id = drone.Id,
                        MaxWeight = (DO.Weight)drone.MaxWeight,
                        Model = drone.Model
                    });

                    dronesList.Add(new()
                    {
                        Id = drone.Id,
                        MaxWeight = drone.MaxWeight,
                        Model = drone.Model,
                        BatteryStatus = rd.NextDouble() * rd.Next(20) + 20,
                        DroneStatus = DroneStatuses.Maintenance,
                        LocationOfDrone = new() { Lattitude = st.Lattitude, Longitude = st.Longitude },
                        PackageNumber = -1
                    });

                    dal.UsingChargingStation(stationId);
                    dal.AddDroneCharge(new() { DroneId = drone.Id, StationId = stationId, ChargeStart = DateTime.Now });
                }
            }
            catch (DalApi.NoNumberFoundException ex) { throw new BlApi.NoNumberFoundException("Station ID not found", ex); }
            catch (DalApi.ExistsNumberException ex) { throw new BlApi.ExistsNumberException("Drone already exists", ex); }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model)
        {
            try
            {
                lock (dal)
                {
                    DO.Drone dr = dal.GetDrone(droneId);

                    dr.Model = model;
                    dal.UpdateDrone(dr);
                }

                dronesList.Find(drone => drone.Id == droneId).Model = model;
            }
            catch (DalApi.NoNumberFoundException ex) { throw new BlApi.NoNumberFoundException("Drone ID not found", ex); }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            var dr = dronesList.Find(x => x.Id == droneId);
            PackageInTransfer packageInTransfer = null;

            if (dr != null)
            {
                if (dr.DroneStatus == DroneStatuses.Sendering)
                {
                    lock (dal)
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
                            DistanceToCollectionOrToDestination = package.PickedUp != null? dr.LocationOfDrone.Distance(targetLocation): dr.LocationOfDrone.Distance(senderLocation)
                        };
                    }
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null)
        {
            lock (dal)
            {
                return dronesList.Where(dr => predicate == null || predicate(dr))
                             .Select(dr => new DroneToList()
                             {
                                 Id = dr.Id,
                                 Model = dr.Model,
                                 MaxWeight = dr.MaxWeight,
                                 BatteryStatus = dr.BatteryStatus,
                                 DroneStatus = dr.DroneStatus,
                                 LocationOfDrone = dr.LocationOfDrone,
                                 PackageNumber = dr.PackageNumber,
                             });
            }
        }
    }
}
