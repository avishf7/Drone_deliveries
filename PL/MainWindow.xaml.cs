using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;
using PL.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BlFactory.GetBl();

        public ObservableCollection<DroneToList> Drones { get; set; }
        public List<PO.Drone> PODrones { get; set; } = new();
        public ObservableCollection<StationToList> Stations { get; set; }
        public List<PO.Station> POStations { get; set; } = new();
        public ObservableCollection<CustomerToList> Customers { get; set; }
        public List<PO.Customer> POCustomers { get; set; } = new();
        public ObservableCollection<PackageToList> Packages { get; set; }
        public List<PO.Package> POPackages { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();

            Drones = new ObservableCollection<DroneToList>(bl.GetDrones());

            Stations = new ObservableCollection<StationToList>(bl.GetStations());

            Customers = new ObservableCollection<CustomerToList>(bl.GetCustomers());

            Packages = new ObservableCollection<PackageToList>(bl.GetPackages());
        }

        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            //  this.Visibility = Visibility.Hidden;
            this.ShowDrones.IsEnabled = false;

            new DronesView(bl, this).Show();

        }



        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            this.ShowCustomers.IsEnabled = false;

            new CustomersView(bl, this).Show();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            this.ShowStations.IsEnabled = false;

            new StationsView(bl, this).Show();
        }

        private void ShowPackages_Click(object sender, RoutedEventArgs e)
        {
            this.ShowPackages.IsEnabled = false;

            new PackagesView(bl, this).Show();
        }
    }
}
