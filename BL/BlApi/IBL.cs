﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        /// <summary>
        /// SImulator for drones
        /// </summary>
        /// <param name="SimulatorProgress">Function to update the view</param>
        /// <param name="IsRun">Function that tests the cancellation pending</param>
        /// <param name="DroneId">The id of the drone</param>
        public void StartSimulator(Action SimulatorProgress, Func<bool> IsRun, int DroneId);

        #region Drone functions

        /// <summary>
        /// Function of adding a drone  to dronesList.
        /// </summary>
        /// <param name="drone">Drone to add</param>   
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="ExistsNumberException"></exception>
        void AddDrone(Drone drone, int staionId);

        /// <summary>
        /// Function of updating a drone.
        /// </summary>
        /// <param name="drone">Drone to update</param>  
        /// <exception cref="NoNumberFoundException"></exception>
        void UpdateDrone(int droneId, string model);

        /// <summary>
        /// Function for displaying drone.
        /// </summary>
        /// <param name="droneId">The id of drone</param>
        /// <returns>A copy of the drone function</returns>  
        /// <exception cref="NoNumberFoundException"></exception>
        Drone GetDrone(int droneId);

        /// <summary>
        /// Displays a list of drone's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of dronesList</returns>
        IEnumerable<DroneToList> GetDrones(Predicate<DroneToList> predicate = null);



        #endregion

        #region Station functions

        /// <summary>
        /// Function of adding a drone station to the stations.
        /// </summary>
        /// <param name="station">Station to add</param>
        /// <exception cref="ExistsNumberException"></exception>
        void AddStation(Station station);

        /// <summary>
        /// Function of updating a station.
        /// </summary>
        /// <param name="station">Station to update</param>
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="TooSmallAmount"></exception>
        void UpdateStation(int stationId, string name, int numOfChargeStation);

        /// <summary>
        /// Function for displaying base station
        /// </summary>
        /// <param name="stationId">The id of the station</param>
        /// <returns>A copy of the station function</returns>
        /// <exception cref="NoNumberFoundException"></exception>
        Station GetStation(int stationId);

        /// <summary>
        /// Displays a list of base stations
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param> 
        /// <returns>The list of stations</returns>
        IEnumerable<StationToList> GetStations(Predicate<StationToList> predicate = null);



        #endregion

        #region Customer functions

        /// <summary>
        /// Function of adding a customer.
        /// </summary>
        /// <param name="customer">Customer to add</param>
        /// <exception cref="ExistsNumberException"></exception>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Function of updating a customer.
        /// </summary>
        /// <param name="customer">Customer to update</param>
        /// <exception cref="NoNumberFoundException"></exception>
        public void UpdateCustomer(int customerId, string name, string phone);

        /// <summary>
        /// Function for displaying customer.
        /// </summary>
        /// <param name="customerId">The id of customer</param>
        /// <returns>A copy of the customer function</returns>
        /// <exception cref="NoNumberFoundException"></exception>
        Customer GetCustomer(int customerId);

        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of customers</returns>
        IEnumerable<CustomerToList> GetCustomers(Predicate<CustomerToList> predicate = null);



        #endregion

        #region Package functions

        /// <summary>
        /// Function of adding a package.
        /// </summary>
        /// <param name="package">Package to add</param>
        /// <exception cref="ExistsNumberException"></exception>
        /// <exception cref="NotValidTargetException"></exception>
        public void AddPackage(Package package);

        /// <summary>
        ///Function for displaying package.
        /// </summary>
        /// <param name="packageId"> The id of package</param>
        /// <returns>A copy of the package function</returns>
        /// <exception cref="NoNumberFoundException"></exception>
        public Package GetPackage(int packageId);

        /// <summary>
        /// Displays a list of package's.
        /// </summary>
        /// <param name="predicate">The list will be filtered according to the conditions obtained</param>
        /// <returns>The list of packages</returns>
        public IEnumerable<PackageToList> GetPackages(Predicate<PackageToList> predicate = null);

        /// <summary>
        /// Delete a package from the list
        /// </summary>
        /// <param name="id">The id of the package</param>
        ///  <exception cref="NoNumberFoundException"></exception>
        ///   <exception cref="PakcageConnectToDroneException"></exception>
        public void DeletePackage(int id);

        #endregion

        #region Delivery functions

        /// <summary>
        /// A function that implements a state of Assigning drone to package 
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        /// <exception cref="NoSuitablePackageForScheduledException"></exception>
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="DroneNotAvailableException"></exception>
        public void PackageAssigning(int droneId);

        /// <summary>
        /// A function that implements the state of a collected package by a drone
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="NoPackageAssociatedWithDrone"></exception>
        /// <exception cref="PackageAlreadyCollectedException"></exception>
        public void PickUp(int droneId);

        /// <summary>
        /// A function that implements the state of a delivered package
        /// </summary>
        /// <param name="droneId">The id of the drone</param>
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="NoPackageAssociatedWithDrone"></exception>
        /// <exception cref="PackageNotCollectedException"></exception>
        public void Deliver(int droneId);

        #endregion

        #region Drone charging functions

        /// <summary>
        /// A function that implements a Drone charging
        /// </summary>
        /// <param name="DroneId">The id of the drone</param>
        /// <exception cref="NoNumberFoundException"></exception>
        /// <exception cref="DroneNotAvailableException"></exception>
        /// <exception cref="NotEnoughBattery"></exception>
        public void SendDroneForCharge(int DroneId);

        /// <summary>
        /// A function that implements a state of releasing a drone from a charge
        /// </summary>
        /// <param name="DroneId">The id of the drone</param>
        ///  <exception cref="NoNumberFoundException"></exception>
        ///   <exception cref="DroneNotMaintenanceException"></exception>
        public void RealeseDroneFromCharge(int DroneId, TimeSpan time);

        /// <summary>
        /// A function that implements a state of releasing a drone from a charge
        /// </summary>
        /// <param name="DroneId">The id of the drone</param>
        /// ///  <exception cref="NoNumberFoundException"></exception>
        ///   <exception cref="DroneNotMaintenanceException"></exception>
        public void RealeseDroneFromCharge(int DroneId);

        #endregion       

    }
}
