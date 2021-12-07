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
                throw new NoNumberFoundException(" ");
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
            if (!DataSource.dronesList.Exists(x => x.Id == droneId))
            {
                throw new NoNumberFoundException(" ");
            }

            return DataSource.dronesList.Find(dr => dr.Id == droneId);
        }

        /// <summary>
        /// Displays a list of drone's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of dronesList</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null)
        {
            return DataSource.dronesList.FindAll(i => predicate == null ? true : predicate(i));

        }

        /// <summary>
        /// Delete a drone from the list
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void DeleteDrone(int id)
        {
            int Id = DataSource.dronesList.FindIndex(dr => dr.Id == id);
            DataSource.dronesList.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
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
        /// Function of updating a station.
        /// </summary>
        /// <param name="station">Station to update</param>
        public void UpdateStation(Station station)
        {
            if (!DataSource.stations.Exists(x => x.Id == station.Id))
            {
                throw new NoNumberFoundException();
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
            if (!DataSource.stations.Exists(x => x.Id == stationId))
            {
                throw new NoNumberFoundException();
            }

            return DataSource.stations.Find(st => st.Id == stationId);


        }


        /// <summary>
        /// Displays a list of base stations
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of stations</returns>
        public IEnumerable<Station> GetStations(Predicate<Station> predicate = null)
        {
            return DataSource.stations.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        }

        /// <summary>
        /// A function that implements a state of perception of a charging position
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        public void UsingChargingStation(int stationId)
        {
            Station station = GetStation(stationId);
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
            Station station = GetStation(stationId);
            int indexStation = DataSource.stations.FindIndex(st => st.Id == stationId);

            station.FreeChargeSlots++;
            DataSource.stations[indexStation] = station;
        }
        /// <summary>
        /// Delete a station from the list
        /// </summary>
        /// <param name="id">The id of the station</param>
        public void DeleteStation(int id)
        {
            int Id = DataSource.stations.FindIndex(st => st.Id == id);
            DataSource.stations.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
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

        /// <summary>
        /// Function of updating a customer.
        /// </summary>
        /// <param name="customer">Customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            if (!DataSource.customers.Exists(x => x.Id == customer.Id))
            {
                throw new NoNumberFoundException();
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
            if (!DataSource.customers.Exists(x => x.Id == customerId))
            {
                throw new NoNumberFoundException();
            }

            Customer tmp = DataSource.customers.Find(cus => cus.Id == customerId);

            return tmp;
        }


        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null)
        {
            return DataSource.customers.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        }

        /// <summary>
        /// Delete a customer from the list
        /// </summary>
        /// <param name="id">The id of the customer</param>
        public void DeleteCustomer(int id)
        {
            int Id = DataSource.customers.FindIndex(cus => cus.Id == id);
            DataSource.customers.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
        }

        #endregion

        #region Package functions

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="package">Package to add</param>
        public void AddPackage(Package package)
        {
            package.Id = DataSource.Config.PackageIdCounter++;
            DataSource.packages.Add(package);
        }

        /// <summary>
        /// Function of updating a package.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdatePackage(Package package)
        {
            if (!DataSource.packages.Exists(x => x.Id == package.Id))
            {
                throw new NoNumberFoundException();
            }

            int iU = DataSource.packages.FindIndex(pck => pck.Id == package.Id);
            DataSource.packages.Insert(iU, package);
        }

        /// <summary>
        ///Function for displaying package.
        /// </summary>
        /// <param name="packageId"> The id of package</param>
        /// <returns>A copy of the package function</returns>
        public Package GetPackage(int packageId)
        {
            if (!DataSource.packages.Exists(x => x.Id == packageId))
            {
                throw new NoNumberFoundException();
            }

            return DataSource.packages.Find(pck => pck.Id == packageId);
        }


        /// <summary>
        /// Displays a list of package's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of packages</returns>
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null)
        {
            return DataSource.packages.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        }

        /// <summary>
        /// A function that implements a state of connecting a package to a skimmer
        /// </summary>
        /// <param name="id">The id of the package </param>
        /// <param name="droneId">The id of the drone</param>
        public void ConnectPackageToDrone(int id, int droneId)
        {
            Package package = GetPackage(id);
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
            Package package = GetPackage(id);
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
            Package package = GetPackage(id);
            int indexPackage = DataSource.packages.FindIndex(pck => pck.Id == id);

            package.Delivered = DateTime.Now;
            package.DroneId = -1;
            DataSource.packages[indexPackage] = package;
        }


        /// <summary>
        /// Delete a package from the list
        /// </summary>
        /// <param name="id">The id of the package</param>
        public void DeletePackage(int id)
        {
            int Id = DataSource.packages.FindIndex(pck => pck.Id == id);
            DataSource.packages.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
        }

        #endregion

        #region Drone charge functions

        /// <summary>
        /// Function of adding a droneCharge.
        /// </summary>
        /// <param name="droneCharge">Drone charge to add</param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.droneCharges.Exists(x => x.DroneId == droneCharge.DroneId))
            {
                throw new ExistsNumberException();
            }

            DataSource.droneCharges.Add(droneCharge);
        }

        /// <summary>
        /// Function of updating a drone charge.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdateDroneCharge(DroneCharge droneCharge)
        {
            if (!DataSource.droneCharges.Exists(x => x.DroneId == droneCharge.DroneId))
            {
                throw new NoNumberFoundException();
            }

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
            if (!DataSource.droneCharges.Exists(x => x.DroneId == droneId))
            {
                throw new NoNumberFoundException();
            }

            return DataSource.droneCharges.Find(dr => dr.DroneId == droneId);
        }

        /// <summary>
        /// Displays a list of drone chrarges.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of dronesList</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null)
        {
            return DataSource.droneCharges.FindAll(i => predicate == null ? true : predicate(i)).ToList();
        }

        /// <summary>
        /// Delete a drone charge from the list
        /// </summary>
        /// <param name="id">The id of drone</param>
        public void DeleteDroneCharge(int id)
        {
            if (!DataSource.droneCharges.Exists(x => x.DroneId == id))
            {
                throw new NoNumberFoundException();
            }

            int Id = DataSource.droneCharges.FindIndex(drc => drc.DroneId == id);
            DataSource.droneCharges.RemoveAt(Id != -1 ? Id : throw new NoNumberFoundException(" "));
        }



        #endregion

        /// <summary>
        /// Information on power consumption and charging time
        /// </summary>
        /// <returns>A list of the charging requests</returns>
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
