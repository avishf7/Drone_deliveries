using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public sealed class Model : INotifyPropertyChanged
    {
        internal static Model Instance { get; set; } = new();

        IBL bl = BlFactory.GetBl();

        public event PropertyChangedEventHandler PropertyChanged;

        #region List View collections
       
        IEnumerable<DroneToList> drones;
        /// <summary>
        /// A collection that represents a list view of drones
        /// </summary>
        public IEnumerable<DroneToList> Drones
        {
            get => drones;
            set
            {
                drones = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }

        IEnumerable<StationToList> stations;
        /// <summary>
        ///  A collection that represents a list view of stations.
        /// </summary>
        public IEnumerable<StationToList> Stations
        {
            get => stations;
            set
            {
                stations = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Stations)));
            }
        }

        IEnumerable<CustomerToList> customers;
        /// <summary>
        ///  A collection that represents a list view of customers.
        /// </summary>
        public IEnumerable<CustomerToList> Customers
        {
            get => customers;
            set
            {
                customers = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Customers)));
            }
        }

        IEnumerable<PackageToList> packages;
        /// <summary>
        ///  A collection that represents a list view of packages.
        /// </summary>
        public IEnumerable<PackageToList> Packages
        {
            get => packages;
            set
            {
                packages = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Packages)));
            }
        }

       
        #endregion

        #region Grouping collections

        public IEnumerable<IGrouping<DroneStatuses, BO.DroneToList>> groupingDrones;

        public IEnumerable<IGrouping<DroneStatuses, BO.DroneToList>> GroupingDrones
        {
            get => groupingDrones;
            set
            {
                groupingDrones = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GroupingDrones"));
            }
        }

        public IEnumerable<IGrouping<string, BO.PackageToList>> groupingPackages { get; set; }
        public IEnumerable<IGrouping<string, BO.PackageToList>> GroupingPackages
        {
            get => groupingPackages;
            set
            {
                groupingPackages = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GroupingPackages"));
            }
        }

        IEnumerable<IGrouping<int, BO.StationToList>> groupingStations;
        public IEnumerable<IGrouping<int, BO.StationToList>> GroupingStations
        {
            get => groupingStations;
            set
            {
                groupingStations = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GroupingStations"));
            }
        }
        #endregion

        #region PO collections
        /// <summary>
        /// A collection representing a view of an entity PODrone.
        /// </summary>
        public List<PO.Drone> PODrones { get; set; } = new();
        /// <summary>
        /// A collection representing a view of an entity POStation.
        /// </summary>
        public List<PO.Station> POStations { get; set; } = new();
        /// <summary>
        /// A collection representing a view of an entity POCustomer.
        /// </summary>
        public List<PO.Customer> POCustomers { get; set; } = new();
        /// <summary>
        /// A collection representing a view of an entity POPackage.
        /// </summary>
        public List<PO.Package> POPackages { get; set; } = new();
        #endregion

        #region Enumerations

        public Array Weight { get; } = Enum.GetValues(typeof(Weight));
        public Array DroneStatuses { get; } = Enum.GetValues(typeof(DroneStatuses));
        public Array Priorities { get; } = Enum.GetValues(typeof(Priorities));
        public Array PackageStatus { get; } = Enum.GetValues(typeof(PackageStatus));
        #endregion

        #region Filters

        public BO.Weight? maxWeightFilter = null;
        public BO.DroneStatuses? DroneStatusesFilter = null;
        public BO.PackageStatus? PackageStatusFilter = null;
        #endregion

        private Model()
        {
            Drones =bl.GetDrones();
            Stations = bl.GetStations();
            Customers = bl.GetCustomers();
            Packages = bl.GetPackages();

            GroupingDrones = from drone in bl.GetDrones()
                             group drone by drone.DroneStatus;
            GroupingPackages = from package in bl.GetPackages()
                               group package by package.SenderName;
            GroupingStations = from station in bl.GetStations()
                               group station by station.SeveralAvailableChargingStations;


        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateDrones()
        {
            Drones = bl.GetDrones(dr => (DroneStatusesFilter != null ? dr.DroneStatus == DroneStatusesFilter : true) &&
                               (maxWeightFilter != null ? dr.MaxWeight == maxWeightFilter : true));

            GroupingDrones = from drone in bl.GetDrones()
                             group drone by drone.DroneStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateStations()
        {
            Stations = bl.GetStations();

            GroupingStations = from station in bl.GetStations()
                               group station by station.SeveralAvailableChargingStations;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdatePackages()
        {
            Packages = bl.GetPackages(pck => (PackageStatusFilter != null ? pck.PackageStatus == PackageStatusFilter : true));

            GroupingPackages = from package in bl.GetPackages()
                               group package by package.SenderName;          

        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateCustomers()
        {
            Customers = bl.GetCustomers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void UpdatePOPackage(int id)
        {
            var poPackage = POPackages.Where(pck => pck.Id == id).SingleOrDefault();
            poPackage?.CopyFromBOPackage(bl.GetPackage(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void UpdatePOStation(Location location)
        {
            var poStation = POStations.Where(st => st.LocationOfStation == location).SingleOrDefault();
            poStation?.CopyFromBOStation(bl.GetStation(poStation.Id));

         
        }
    }
}
