using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    sealed class Model
    {
        internal static Model Instance { get; set; } = new();

        IBL bl = BlFactory.GetBl();

        internal ObservableCollection<DroneToList> Drones { get; set; }
        internal List<PO.Drone> PODrones { get; set; } = new();
        internal ObservableCollection<StationToList> Stations { get; set; }
        internal List<PO.Station> POStations { get; set; } = new();
        internal ObservableCollection<CustomerToList> Customers { get; set; }
        internal List<PO.Customer> POCustomers { get; set; } = new();
        internal ObservableCollection<PackageToList> Packages { get; set; }
        internal List<PO.Package> POPackages { get; set; } = new();

        private Model()
        {
            Drones = new ObservableCollection<DroneToList>(bl.GetDrones());
            Stations = new ObservableCollection<StationToList>(bl.GetStations());
            Customers = new ObservableCollection<CustomerToList>(bl.GetCustomers());
            Packages = new ObservableCollection<PackageToList>(bl.GetPackages());
        }
    }
}
