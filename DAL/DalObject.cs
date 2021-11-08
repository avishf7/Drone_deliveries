using IDAL.DO;
using System;
using System.Collections.Generic;
using IDAL;
using System.Linq;

namespace DalObject
{
    /// <summary>
    /// A class in which we will make the changes in all parts of the code
    /// </summary>
    public class DalObject : IDal
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
        /// Function of adding a drone  to dronesList.
        /// </summary>
        /// <param name="drone">Drone to add</param>
        public void AddDrone(Drone drone)
        {
            if (DataSource.dronesList.Exists(x => x.Id == drone.Id))
            {
                throw new ExistsNumberException();
            }
            DataSource.dronesList.Add(drone);
        }

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="drone">Drone to update</param>
        public void UpdateDrone(Drone drone)
        {
            int iU = DataSource.dronesList.FindIndex(dr => dr.Id == drone.Id);
            if (iU == -1)
            {
                throw new NoNumberFoundExeptions(" ");
            }

            DataSource.dronesList.Insert(iU, drone);
        }

        /// <summary>
        /// Function for displaying drone.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>
        public Drone GetDrone(int droneId)
        {
            Drone tmp = DataSource.dronesList.Find(dr => dr.Id == droneId);

            return tmp;         
        }

        /// <summary>
        /// Displays a list of drone's.
        /// </summary>
        /// <returns>The list of dronesList</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null)
        {
            return DataSource.dronesList.FindAll(i => predicate == null ? true : predicate(i)).ToList();

        }

        /// <summary>
        /// A function that implements a skimmer association mode for a package
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void AssigningSkimmerToPackage(int id)
        {
            Drone drone = DataSource.dronesList.Find(drn => drn.Id == id);
        }

        /// <summary>
        /// A function that implements a skimmer mode that has completed the shipment
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void DroneDeliverEnded(int id)
        {
            Drone drone = DataSource.dronesList.Find(drn => drn.Id == id);

            //    drone.Status = DroneStatuses.AVAILABLE;
        }

        /// <summary>
        /// A function that implements a mode of sending a skimmer to a charging station
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        public void SendDroneForCharge(int droneId)
        {
            Drone drone = DataSource.dronesList.Find(drn => drn.Id == droneId);
        }

        /// <summary>
        /// A function that implements a skimmer release mode from a charging position
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        public void RealeseDroneFromCharge(int droneId)
        {
            Drone drone = DataSource.dronesList.Find(drn => drn.Id == droneId);
        }

        public void DeleteDrone(int id)
        {
            int Id = DataSource.dronesList.FindIndex(dr => dr.Id == id);
            DataSource.dronesList.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundExeptions(" "));
        }

        #endregion

        #region Station functions

        /// <summary>
        /// Function of adding a drone station to the stations.
        /// </summary>
        /// <param name="station">Station to add</param>
        public void AddStation(Station station)
        {
            if (DataSource.stations.Exists(x => x.Id == station.Id))
            {
                throw new ExistsNumberException();
            }
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="station">Station to update</param>
        public void UpdateStation(Station station)
        {
            if (DataSource.stations.Exists(x => x.Id != station.Id))
            {
                throw new NoNumberFoundExeptions();
            }
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

            return tmp;     
        }


        /// <summary>
        /// Displays a list of base stations
        /// </summary>
        /// <returns>The list of stations</returns>
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null)
        {
            return DataSource.stations.FindAll(i => predicate == null ? true : predicate(i)).ToList();
            
           // return DataSource.stations.Take(DataSource.stations.Count).ToList();
             }

        /// <summary>
        /// Display of base stations with available charging stations
        /// </summary>
        /// <returns>The base stations with available charging stations</returns>
        public IEnumerable<Station> GetFreeStations(Predicate<Station> predicate = null)
        {
            return DataSource.stations.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        }

        /// <summary>
        /// A function that implements a state of perception of a charging position
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        public void UsingChargingStation(int stationId)
        {
            Station station = DataSource.stations.Find(st => st.Id == stationId);
            int indexStation = DataSource.stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots--;
            DataSource.stations[indexStation] = station;
        }

        /// <summary>
        /// A function that implements a state of releasing a charging position
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        public void RealeseChargingStation(int stationId)
        {
            Station station = DataSource.stations.Find(st => st.Id == stationId);

            int indexStation = DataSource.stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots++;
            DataSource.stations[indexStation] = station;
        }

        public void DeleteStation(int id)
        {
            int Id = DataSource.stations.FindIndex(st => st.Id == id);
            DataSource.stations.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundExeptions(" "));
        }
        #endregion

        #region Customer functions

        /// <summary>
        /// Function of adding a customer.
        /// </summary>
        /// <param name="customer">Customer to add</param>
        public void AddCustomer(Customer customer)
        {
            if (DataSource.customers.Exists(x => x.Id == customer.Id))
            {
                throw new ExistsNumberException();
            }
            DataSource.customers.Add(customer);
        }

        //public void ComparisonCustomer(Customer customer, int id)
        //{
        //    int find = DataSource.customers.FindIndex(cus => cus.Id == customer.Id);
        //    if (find != -1)
        //    {
        //        Console.WriteLine("Existing customer number, enter another number: ");
        //    }
        //}
        /// <summary>
        /// Function of updating a customer.
        /// </summary>
        /// <param name="customer">Customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            if (DataSource.customers.Exists(x => x.Id != customer.Id))
            {
                throw new NoNumberFoundExeptions();
            }
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

            return tmp;        
        }

        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <returns>The list of customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null)
        {
            return DataSource.customers.FindAll(i => predicate == null ? true : predicate(i)).ToList();
            
           // return DataSource.customers.Take(DataSource.customers.Count).ToList();

        }

        public void DeleteCustomer(int id)
        {
            int Id = DataSource.customers.FindIndex(cus => cus.Id == id);
            DataSource.customers.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundExeptions(" "));
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

            return tmp;
        }

        /// <summary>
        /// Displays a list of package's.
        /// </summary>
        /// <returns>The list of packages</returns>
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        {
            return DataSource.packages.FindAll(i => predicate == null ? true : predicate(i)).ToList();
            
            //return DataSource.packages.Take(DataSource.packages.Count).ToList();
        }

        /// <summary>
        /// Displays a list of packages not yet associated with the glider.
        /// </summary>
        /// <returns>List of packages not yet tied to the drone</returns>
        public IEnumerable<Package> GetNotScheduledPackages()
        {
            return DataSource.packages.FindAll(pck => pck.DroneId == -1).ToList();
            //List<Package> packages = new();

            //foreach (var pck in tmpPackages)
            //{
            //    packages.Add(new()
            //    {
            //        Id = pck.Id,
            //        SenderId = pck.SenderId,
            //        TargetId = pck.TargetId,
            //        Weight = pck.Weight,
            //        Priority = pck.Priority,
            //        Requested = pck.Requested,
            //        Scheduled = pck.Scheduled,
            //        PickedUp = pck.PickedUp,
            //        Delivered = pck.Delivered,
            //        DroneId = pck.DroneId
            //    });
            //}

            //return packages;
        }

        /// <summary>
        /// A function that implements a state of connecting a package to a skimmer
        /// </summary>
        /// <param name="id">The id of the package </param>
        /// <param name="droneId">The id of the drone</param>
        public void ConnectPackageToDrone(int id, int droneId)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);
            int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

            package.DroneId = droneId;
            package.Scheduled = DateTime.Now;

            DataSource.packages[indexPackage] = package;


        }

        /// <summary>
        /// A function that implements the state of a collected package
        /// </summary>
        /// <param name="id">The id of the package </param>
        public void PickUp(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);
            int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

            package.PickedUp = DateTime.Now;
            DataSource.packages[indexPackage] = package;

        }

        /// <summary>
        /// A function that implements the state of a delivered package
        /// </summary>
        /// <param name="id">The id of the package</param>
        public void PackageDeliver(int id)
        {
            Package package = DataSource.packages.Find(pck => pck.Id == id);
            int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

            package.Delivered = DateTime.Now;
            DataSource.packages[indexPackage] = package;
        }

        public void DeletePackage(int id)
        {
            int Id = DataSource.packages.FindIndex(pck => pck.Id == id);
            DataSource.packages.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundExeptions(" "));
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

            return tmp;
        }

        /// <summary>
        /// Displays a list of drone chrarges.
        /// </summary>
        /// <returns>The list of dronesList</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        {
            return DataSource.droneCharges.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        
        //    return DataSource.droneCharges.Take(DataSource.droneCharges.Count).ToList();
        }


        public void DeleteDronesCharge(int id)
        {
            int Id = DataSource.droneCharges.FindIndex(drc => drc.DroneId == id);
            DataSource.droneCharges.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundExeptions(" "));
        }


        #endregion


        public List<double> ChargingRequest()
        {
            List<double> ChargingRequests = new()
            {
                DataSource.Config.DroneAvailable,
                DataSource.Config.LightWeight,
                DataSource.Config.MediumWeight,
                DataSource.Config.HeavyWeight,
                DataSource.Config.ChargingRate
            };
            return ChargingRequests;
        }

    }
}
