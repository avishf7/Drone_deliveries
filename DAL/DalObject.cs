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
        /// CTOR
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        #region Drone functions

        /// <summary>
        /// Function of adding a drone  to drones.
        /// </summary>
        /// <param name="drone">Drone to add</param>
        public void AddDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
        }

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="drone">Drone to update</param>
        public void UpdateDrone(Drone drone)
        {
           int iU = DataSource.drones.FindIndex(dr => dr.Id == drone.Id);
            DataSource.drones.Insert(iU, drone);
        }

        /// <summary>
        /// Function for displaying drone.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>
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
        /// Displays a list of drone's.
        /// </summary>
        /// <returns>The list of drones</returns>
        public List<Drone> GetDrones()
        {
            List<Drone> drones = new();


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
        /// Function for sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        /// <param name="stationId">The id of the station</param>
        public void SendDroneForCharge(int droneId, int stationId)
        {
            Drone drone = DataSource.drones.Find(drn => drn.Id == droneId);
            Station station = DataSource.stations.Find(st => st.Id == stationId);

            drone.Status = DroneStatuses.MAINTENANCE;
            DataSource.droneCharges.Add(new()
            {
                DroneId = droneId,
                StationId = stationId
            });

            station.FreeChargeSlots--;
        }

        /// <summary>
        /// Function for releasing a skimmer from charging at a base station
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        public void RealeseDroneFromCharge(int droneId)
        {
            Drone drone = DataSource.drones.Find(drn => drn.Id == droneId);
            DroneCharge droneCharge = DataSource.droneCharges.Find(drnCh => drnCh.DroneId == droneId);
            Station station = DataSource.stations.Find(st => st.Id == droneCharge.StationId);
            DataSource.droneCharges.Remove(DataSource.droneCharges.Find(drnCh => drnCh.DroneId == droneId));

            station.FreeChargeSlots++;
            drone.Status = DroneStatuses.AVAILABLE;
            drone.Battery = 100;
        }



        #endregion

        #region Station functions

        /// <summary>
        /// Function of adding a drone station to the stations.
        /// </summary>
        /// <param name="station">Station to add</param>
        public void AddStation(Station station)
        {
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="station">Station to update</param>
        public void UpdateStation(Station station)
        {
            int iU = DataSource.stations.FindIndex(st => st.Id == station.Id);
            DataSource.stations.Insert(iU, station);
        }

        /// <summary>
        /// Function for displaying base station
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        /// <returns>A copy of the station function</returns>
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
        /// Displays a list of base stations
        /// </summary>
        /// <returns>The list of stations</returns>
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
        /// Display of base stations with available charging stations
        /// </summary>
        /// <returns>The base stations with available charging stations</returns>
        public List<Station> GetFreeStations()
        {
            List<Station> tmpStations = DataSource.stations.FindAll(st => st.FreeChargeSlots != 0);
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

        #endregion

        #region Customer functions

        /// <summary>
        /// Function of adding a customer.
        /// </summary>
        /// <param name="customer">Customer to add</param>
        public void AddCustomer(Customer customer)
        {
            DataSource.customers.Add(customer);
        }

        /// <summary>
        /// Function of updating a customer.
        /// </summary>
        /// <param name="customer">Customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            int iU = DataSource.customers.FindIndex(cus => cus.Id == customer.Id);
            DataSource.customers.Insert(iU, customer);
        }

        /// <summary>
        /// Function for displaying customer.
        /// </summary>
        /// <param name="customerId">The id of customer</param>
        /// <returns>A copy of the customer function</returns>
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
        /// Displays a list of customers.
        /// </summary>
        /// <returns>The list of customers</returns>
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

        #endregion

        #region Package functions

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="sendersId">Identity card of the sender of the package</param>
        /// <param name="targetsId">Identity card of the recipient of the package</param>
        /// <param name="weight">The weight of the package</param>
        /// <param name="priority">Priority of shipment</param>
        public void AddPackage(Package package)
        {
            package.Id = DataSource.Config.PackageIdCounter++;
            DataSource.packages.Add(package);
        }

        /// <summary>
        /// Function of updating a package.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdatePackage(Package Package)
        {
            int iU = DataSource.packages.FindIndex(pck => pck.Id == Package.Id);
            DataSource.packages.Insert(iU, Package);
        }

        /// <summary>
        ///Function for displaying package.
        /// </summary>
        /// <param name="packageId"> The id of package</param>
        /// <returns>A copy of the package function</returns>
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
        /// Displays a list of package's.
        /// </summary>
        /// <returns>The list of packages</returns>
        public List<Package> GetPackages()
        {
            List<Package> packages = new();

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
        /// Displays a list of packages not yet associated with the glider.
        /// </summary>
        /// <returns>List of packages not yet tied to the drone</returns>
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
        /// Function to connect packag to drone .
        /// </summary>
        /// <param name="id">The id of the package </param>
        /// <param name="droneId">The id of the drone</param>
        public void ConnectPackageToDrone(int id, int droneId)
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
        /// <param name="id">The id of the package </param>
        public void PickUp(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);

            package.PickedUp = DateTime.Now;
        }

        /// <summary>
        /// Function for delivering package to customer
        /// </summary>
        /// <param name="id">The id of the package</param>
        public void Deliver(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);
            Drone drone = DataSource.drones.Find(drn => drn.Id == package.DroneId);

            package.Delivered = DateTime.Now;
            drone.Status = DroneStatuses.AVAILABLE;
        }

        #endregion

        #region Drone charge functions

        /// <summary>
        /// Function of remove a droneCharge.
        /// </summary>
        /// <param name="droneCharge">Drone charge to add</param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges.Add(droneCharge);
        }

        /// <summary>
        /// Function of remove a droneCharge.
        /// </summary>
        /// <param name="droneCharge">Drone charge to remove</param>
        public void RemoveDroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges.Remove(droneCharge);
        }

        /// <summary>
        /// Function of updating a drone charge.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdateDroneCharge(DroneCharge droneCharge)
        {
            int iU = DataSource.droneCharges.FindIndex(drCh => drCh.DroneId == droneCharge.DroneId);
            DataSource.droneCharges.Insert(iU, droneCharge);
        }

        /// <summary>
        /// Function for displaying drone charges.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            DroneCharge tmp = DataSource.droneCharges.Find(dr => dr.DroneId == droneId);

            return new()
            {
                DroneId = droneId,
                StationId = tmp.StationId
            };
        }

        /// <summary>
        /// Displays a list of drone chrarges.
        /// </summary>
        /// <returns>The list of drones</returns>
        public List<DroneCharge> GetDronesCharges()
        {
            List<DroneCharge> drones = new();


            foreach (var dr in DataSource.droneCharges)
            {
                drones.Add(new()
                {
                    DroneId = dr.DroneId,
                    StationId = dr.StationId
                });
            }

            return drones;
        }

        #endregion








    }
}
