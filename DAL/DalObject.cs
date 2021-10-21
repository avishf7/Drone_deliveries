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
        public void AddDrone(int id, string model, Weight maxWeight, DroneStatuses status)
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
        public void AddStation(int id, string name, int numOfFreeStation, double Longitude, double lattitude)
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
        public void AddCustomer(int id, string name, string phone, double longitude, double lattitude)
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
                Scheduled = DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Delivered = DateTime.MinValue,
                DroneId = -1
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

        /// <summary>
        /// Function for collecting a package by skimmer
        /// </summary>
        /// <param name="id"></param>
        public void PickUp(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);

            package.PickedUp = DateTime.Now;
        }

        /// <summary>
        /// Function for delivering package to customer
        /// </summary>
        /// <param name="id"></param>
        public void Deliver(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);

            package.Delivered = DateTime.Now;
        }

        /// <summary>
        /// Function for sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void SendDroneForCharge(int droneId, int stationId)
        {
            Drone drone = DataSource.drones.Find(drn => drn.Id == droneId);
            Station station = DataSource.stations.Find(st => st.Id == stationId);

            drone.Status = DroneStatuses.MAINTENANCE;
            droneCharges.Add(new()
            {
                DroneId = droneId,
                StationId = stationId
            });
        }

        /// <summary>
        /// Function for releasing a skimmer from charging at a base station
        /// </summary>
        /// <param name="droneId"></param>
        public void RealeseDroneFromCharge(int droneId)
        {
            Drone drone = DataSource.drones.Find(drn => drn.Id == droneId);
            droneCharges.Remove(droneCharges.Find(drnCh => drnCh.DroneId == droneId));

            drone.Status = DroneStatuses.AVAILABLE;
            drone.Battery = 100;
        }
        /*איפה הפונקציה של הוספת והסרת העמדה שנתפסה*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public Station GetStation(int stationId)
        {
            Station tmp = DataSource.stations.Find(st => st.Id == stationId);

            return new()
            {
                Id = tmp.Id,
                Name = tmp.Name,
                FreeChargeSlots = tmp.FreeChargeSlots,
                Longitude = tmp.Longitude,
                Lattitude = tmp.Lattitude,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Drone GetDrone(int droneId)
        {
            Drone tmp = DataSource.drones.Find(dr => dr.Id == droneId);

            return new()
            {
                Id = tmp.Id,
                MaxWeight = tmp.MaxWeight,
                Model = tmp.Model,
                Status = tmp.Status,
                Battery = tmp.Battery
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomer(int customerId)
        {
            Customer tmp = DataSource.customers.Find(cus => cus.Id == customerId);

            return new()
            {
                Id = tmp.Id,
                Name = tmp.Name,
                Phone = tmp.Phone,
                Longitude = tmp.Longitude,
                Lattitude = tmp.Lattitude,

            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public Package GetPackage(int packageId)
        {
            Package tmp = DataSource.packages.Find(pck => pck.Id == packageId);

            return new()
            {
                Id = tmp.Id,
                SenderId = tmp.SenderId,
                TargetId = tmp.TargetId,
                Weight = tmp.Weight,
                Priority = tmp.Priority,
                Requested = tmp.Requested,
                Scheduled = tmp.Scheduled,
                PickedUp = tmp.PickedUp,
                Delivered = tmp.Delivered,
                DroneId = tmp.DroneId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Station> GetStations()
        {
            List<Station> stations = new();

            foreach (var st in DataSource.stations)
            {
                stations.Add(new()
                {
                    Id = st.Id,
                    Name = st.Name,
                    FreeChargeSlots = st.FreeChargeSlots,
                    Longitude = st.Longitude,
                    Lattitude = st.Lattitude,
                });
            }

            return stations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDrones()
        {
            List < Drone > drones = new();


            foreach (var dr in DataSource.drones)
            {
                drones.Add(new()
                {
                    Id = dr.Id,
                    MaxWeight = dr.MaxWeight,
                    Model = dr.Model,
                    Status = dr.Status,
                    Battery = dr.Battery
                });
            }

            return drones;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new();

            foreach (var cus in DataSource.customers)
            {
                customers.Add(new()
                {
                    Id = cus.Id,
                    Name = cus.Name,
                    Phone = cus.Phone,
                    Longitude = cus.Longitude,
                    Lattitude = cus.Lattitude,
                });
            }

            return customers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Package> GetPackages()
        {
            List <Package> packages = new();

            foreach (var pck in DataSource.packages)
            {
                packages.Add(new()
                {
                    Id = pck.Id,
                    SenderId = pck.SenderId,
                    TargetId = pck.TargetId,
                    Weight = pck.Weight,
                    Priority = pck.Priority,
                    Requested = pck.Requested,
                    Scheduled = pck.Scheduled,
                    PickedUp = pck.PickedUp,
                    Delivered = pck.Delivered,
                    DroneId = pck.DroneId
                });
            }

            return packages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Package> GetNotScheduledPackages()
        {
            List<Package> tmpPackages = DataSource.packages.FindAll(pck => pck.DroneId == -1);
            List<Package> packages = new();

            foreach (var pck in tmpPackages)
            {
                packages.Add(new()
                {
                    Id = pck.Id,
                    SenderId = pck.SenderId,
                    TargetId = pck.TargetId,
                    Weight = pck.Weight,
                    Priority = pck.Priority,
                    Requested = pck.Requested,
                    Scheduled = pck.Scheduled,
                    PickedUp = pck.PickedUp,
                    Delivered = pck.Delivered,
                    DroneId = pck.DroneId
                });
            }

            return packages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Station> GetFreeStations()
        {
            List < Station> tmpStations = DataSource.stations.FindAll(st => st.FreeChargeSlots != 0);
            List<Station> stations = new();

            foreach (var st in tmpStations)
            {
                stations.Add(new()
                {
                    Id = st.Id,
                    Name = st.Name,
                    FreeChargeSlots = st.FreeChargeSlots,
                    Longitude = st.Longitude,
                    Lattitude = st.Lattitude,
                });
            }

            return stations;
        }
    }
}
