using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    interface IBl
    {

        #region Drone functions

        /// <summary>
        /// Function of adding a drone  to dronesList.
        /// </summary>
        /// <param name="drone">Drone to add</param>        
        void AddDrone(Drone drone ,int staionId);

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="drone">Drone to update</param>        
        void UpdateDrone(int droneId, string model);

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
        IEnumerable<DroneToList> GetDrones(Predicate<Drone> predicate = null);

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
        void UpdateStation(int stationId, string name, int numOfChargeStation);

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
        IEnumerable<StationToList> GetStations(Predicate<Station> predicate = null);

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
        void UpdateCustomer(CustomerToList customer);

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
        IEnumerable<CustomerToList> GetCustomers(Predicate<Customer> predicate = null);

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
        public void UpdatePackage(PackageToList Package);

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
        public IEnumerable<PackageToList> GetPackages(Predicate<Package> predicate = null);

        /// <summary>
        /// Delete a package from the list
        /// </summary>
        /// <param name="id">The id of the package</param>
        void DeletePackage(int id);

        /// <summary>
        /// Displays a list of package's that no Requested.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<PackageToList> GetNoRequestedPackages(Predicate<Package> predicate = null)


        #endregion

        #region Delivery functions

        /// <summary>
        /// A function that implements a state of Assigning drone to package 
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        void packageAssigning(int droneId);

        /// <summary>
        /// A function that implements the state of a collected package by a drone
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        void PickUp(int droneId);

        /// <summary>
        /// A function that implements the state of a delivered package
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        void Deliver(int droneId);

        #endregion

        #region Drone charging functions

        /// <summary>
        /// A function that implements a Drone charging
        /// </summary>
        /// <param name="DroneId">The id of the drone</param>
        void SendDroneForCharge(int DroneId);

        /// <summary>
        /// A function that implements a state of releasing a drone from a charge
        /// </summary>
        /// <param name="DroneId">The id of the drone</param>
        void RealeseDroneFromCharge(int DroneId, TimeSpan time);

        #endregion 
    }
}
