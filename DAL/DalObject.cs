using IDAL.DO;
using System;
using System.Collections.Generic;
using IDAL;


namespace DalObject
{
    /// <summary>
    /// A class in which we will make the changes in all parts of the code
    /// </summary>
    public class DalObject
    {
        /// <summary>
        /// 
        /// </summary>
        List<DroneCharge> droneCharges = new();
        /// <summary>
        /// CTOR
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }
        /// <summary>
        /// Function of adding a drone  to drones.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="status"></param>
        public void AddDrone(int id,string model,Weight maxWeight,DroneStatuses status)
        {
            DataSource.drones.Add(new() { Id = id, Model = model, MaxWeight = maxWeight, Status = status, Battery = 100 });
        }
        /// <summary>
        /// Function of adding a drone station to the stations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="numOfFreeStation"></param>
        /// <param name="Longitude"></param>
        /// <param name="lattitude"></param>
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
        /// <summary>
        ///  Function of adding a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="lattitude"></param>
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
        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="sendersId"></param>
        /// <param name="targetsId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
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
        /// <summary>
        /// Function to connect packag to drone .
        /// </summary>
        /// <param name="id"></param>
        /// <param name="droneId"></param>
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
