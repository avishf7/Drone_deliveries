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

        ObservableCollection<DroneToList> drones;
        public ObservableCollection<DroneToList> Drones 
        {
            get => drones;
            set
            {
                drones = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Drones"));
            } 
        }

        ObservableCollection<StationToList> stations;
        public ObservableCollection<StationToList> Stations
        {
            get => stations;
            set
            {
                stations = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Stations"));
            }
        }

        ObservableCollection<CustomerToList> customers;
        public ObservableCollection<CustomerToList> Customers
        {
            get => customers;
            set
            {
                customers = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
            }
        }

        ObservableCollection<PackageToList> packages;
        public ObservableCollection<PackageToList> Packages
        {
            get => packages;
            set
            {
                packages = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Packages"));
            }
        }


        public List<PO.Drone> PODrones { get; set; } = new();        
        public List<PO.Station> POStations { get; set; } = new();        
        public List<PO.Customer> POCustomers { get; set; } = new();       
        public List<PO.Package> POPackages { get; set; } = new();

        public Array Weight { get; set; } = Enum.GetValues(typeof(Weight));
        public Array DroneStatuses { get; set; } = Enum.GetValues(typeof(DroneStatuses));
        public Array Priorities { get; set; } = Enum.GetValues(typeof(Priorities));
        public Array PackageStatus { get; set; } = Enum.GetValues(typeof(PackageStatus));


        private Model()
        {
            Drones = new ObservableCollection<DroneToList>(bl.GetDrones());
            Stations = new ObservableCollection<StationToList>(bl.GetStations());
            Customers = new ObservableCollection<CustomerToList>(bl.GetCustomers());
            Packages = new ObservableCollection<PackageToList>(bl.GetPackages());
        }
    }
}
