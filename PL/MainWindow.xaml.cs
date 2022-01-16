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


        public MainWindow()
        {
            InitializeComponent();
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

            new CustomersView( this).Show();
        }

        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            this.ShowDrones.IsEnabled = false;
            new DronesView(this).Show();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            this.ShowStations.IsEnabled = false;

            new StationsView( this).Show();
        }

        private void ShowPackages_Click(object sender, RoutedEventArgs e)
        {
            this.ShowPackages.IsEnabled = false;

            new PackagesView( this).Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var dr = bl.GetDrones(d => d.DroneStatus == DroneStatuses.Maintenance);
            foreach (var item in dr)
            {
                bl.RealeseDroneFromCharge(item.Id);
            }
        }
    }
}
