using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDal
    {
        #region Drone functions
        /// <summary>
        /// Function of adding a drone  to dronesList.
        /// </summary>
        /// <param name="drone">Drone to add</param>        
        void AddDrone(Drone drone);

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="drone">Drone to update</param>        
        void UpdateDrone(Drone drone);

        /// <summary>
        /// Function for displaying drone.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>        
        Drone GetDrone(int droneId);

        /// <summary>
        /// Displays a list of drone's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of dronesList</returns>
        IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null);

        /// <summary>
        /// Delete a drone from the list
        /// </summary>
        /// <param name="id">The id of the drone</param>
        void DeleteDrone(int id);

        #endregion

        #region Station functions

        /// <summary>
        /// Function of adding a drone station to the stations.
        /// </summary>
        /// <param name="station">Station to add</param>
        void AddStation(Station station);

        /// <summary>
        /// Function of updating a station.
        /// </summary>
        /// <param name="station">Station to update</param>
        void UpdateStation(Station station);

        /// <summary>
        /// Function for displaying base station
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        /// <returns>A copy of the station function</returns>
        Station GetStation(int stationId);

        /// <summary>
        /// Displays a list of base stations
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of stations</returns>
        IEnumerable<Station> GetStations(Predicate<Station> predicate = null);

        /// <summary>
        /// A function that implements a state of perception of a charging position
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        void UsingChargingStation(int stationId);

        /// <summary>
        /// A function that implements a state of releasing a charging position
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        void RealeseChargingStation(int stationId);

        /// <summary>
        /// Delete a station from the list
        /// </summary>
        /// <param name="id">The id of the station</param>
        public void DeleteStation(int id);

        #endregion

        #region Customer functions

        /// <summary>
        /// Function of adding a customer.
        /// </summary>
        /// <param name="customer">Customer to add</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Function of updating a customer.
        /// </summary>
        /// <param name="customer">Customer to update</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// Function for displaying customer.
        /// </summary>
        /// <param name="customerId">The id of customer</param>
        /// <returns>A copy of the customer function</returns>
        Customer GetCustomer(int customerId);

        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of customers</returns>
        IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null);

        /// <summary>
        /// Delete a customer from the list
        /// </summary>
        /// <param name="id">The id of the customer</param>
        public void DeleteCustomer(int id);

        #endregion

        #region package functions

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="package">Package to add</param>
        public void AddPackage(Package package);

        /// <summary>
        /// Function of updating a package.
        /// </summary>
        /// <param name="Package">Package to update</param>
        public void UpdatePackage(Package Package);

        /// <summary>
        ///Function for displaying package.
        /// </summary>
        /// <param name="packageId"> The id of package</param>
        /// <returns>A copy of the package function</returns>
        public Package GetPackage(int packageId);

        /// <summary>
        /// Displays a list of package's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of packages</returns>
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null);

        /// <summary>
        /// A function that implements the state of a collected package
        /// </summary>
        /// <param name="id">The id of the package </param>
        void PickUp(int id);

        /// <summary>
        /// A function that implements the state of a delivered package
        /// </summary>
        /// <param name="id">The id of the package</param>
        void PackageDeliver(int id);

        /// <summary>
        /// A function that implements a state of connecting a package to a skimmer
        /// </summary>
        /// <param name="id">The id of the package </param>
        /// <param name="droneId">The id of the drone</param>
        void ConnectPackageToDrone(int id, int droneId);

        /// <summary>
        /// Delete a package from the list
        /// </summary>
        /// <param name="id">The id of the package</param>
        void DeletePackage(int id);

        #endregion

        #region DroneCharge functions

        /// <summary>
        /// Function of adding a droneCharge.
        /// </summary>
        /// <param name="droneCharge">Drone charge to add</param>
        void AddDroneCharge(DroneCharge droneCharge);

        /// <summary>
        /// Function of updating a drone charge.
        /// </summary>
        /// <param name="Package">Package to update</param>
        void UpdateDroneCharge(DroneCharge droneCharge);

        /// <summary>
        /// Function for displaying drone charges.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>
        DroneCharge GetDroneCharge(int droneId);

        /// <summary>
        /// Displays a list of drone chrarges.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of dronesList</returns>
        IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null);

        /// <summary>
        /// Delete a drone charge from the list
        /// </summary>
        /// <param name="id">The id of drone</param>
        void DeleteDroneCharge(int id);

        #endregion

        /// <summary>
        /// Information on power consumption and charging time
        /// </summary>
        /// <returns>A list of the charging requests</returns>
        List<double> ChargingRequest();
    }
}
