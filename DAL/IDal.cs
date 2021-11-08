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

        void AddDrone(Drone drone);
        void UpdateDrone(Drone drone);
        Drone GetDrone(int droneId);
        IEnumerable<Drone> GetDrones(Predicate<Drone> predicate = null);
        void AssigningSkimmerToPackage(int id);
        void DroneDeliverEnded(int id);
        void SendDroneForCharge(int droneId);
        void RealeseDroneFromCharge(int droneId);
        void DeleteDrone(int id);
        #endregion

        #region Station functions

        void AddStation(Station station);
       
            void UpdateStation(Station station);
        Station GetStation(int stationId);
        IEnumerable<Station> GetStations(Predicate<Station> predicate = null);
        IEnumerable<Station> GetFreeStations(Predicate<Station> predicate = null);
        void UsingChargingStation(int stationId);
        void RealeseChargingStation(int stationId);
        public void DeleteStation(int id);

        #endregion

        #region Customer functions
        void AddCustomer(Customer customer);
      //void ComparisonCustomer(Customer customer, int id);
        void UpdateCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate = null);
        public void DeleteCustomer(int id);
        #endregion

        #region package functions
        public void AddPackage(Package package);
        public void UpdatePackage(Package Package);
        public Package GetPackage(int packageId);
        public IEnumerable<Package> GetPackages(Predicate<Package> predicate = null);
        IEnumerable<Package> GetNotScheduledPackages();
        void PickUp(int id);
        void PackageDeliver(int id);
        void ConnectPackageToDrone(int id, int droneId);
        void DeletePackage(int id);
        #endregion

        #region DroneCharge functions
        void AddDroneCharge(DroneCharge droneCharge);
        void RemoveDroneCharge(DroneCharge droneCharge);
        void UpdateDroneCharge(DroneCharge droneCharge);
        DroneCharge GetDroneCharge(int droneId);
        IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate = null);
        void DeleteDronesCharge(int id);
        #endregion



        List<double> ChargingRequest();
    }
}
