using IDAL.DO;
using System;
using System.Collections.Generic;
using IDAL;


namespace DalObject
{
    public class DalObject
    {
        List<DroneCharge> droneCharges = new();

        public DalObject()
        {
            DataSource.Initialize();
        }

        public void AddDrone(int id,string model,Weight maxWeight,DroneStatuses status)
        {
            DataSource.drones.Add(new() { Id = id, Model = model, MaxWeight = maxWeight, Status = status, Battery = 100 });
        }

        public void AddStation(int id, string name, int numOfFreeStation,double Longitude, double lattitude)
        {
            DataSource.stations.Add(new()
            {
                Id = id,
                Name = name,
                FreeChargeSlots = numOfFreeStation,
                Longitude = Longitude,
                Lattitude = lattitude
            });
        }

        public void AddCustomer(int id, string name, string phone,double longitude, double lattitude)
        {
            DataSource.customers.Add(new()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Lattitude = lattitude
            });
        }

        public void AddPackage(int sendersId, int targetsId, Weight weight, Priorities priority)
        {
            DataSource.packages.Add(new()
            {
                Id = DataSource.Config.PackageIdCounter++,
                SenderId = sendersId,
                TargetId = targetsId,
                Weight = weight,
                Priority = priority,
                Requested = DateTime.Now,

            });
        }

        public void ConnectedPackagToDrone(int id, int droneId)
        {
            Drone drone = DataSource.drones.Find(drn => drn.Id == droneId);
            Package package = DataSource.packages.Find(pck => pck.Id == id);
            package.DroneId = droneId;
            package.Scheduled = DateTime.Now;
            drone.Status = DroneStatuses.DELIVERY;

        }

    }
}
