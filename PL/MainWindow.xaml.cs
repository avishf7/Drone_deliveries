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
        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prevents the main window from resizing when double-clicking
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Window Close Button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Button for open the customers view
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            this.ShowCustomers.IsEnabled = false;

            new CustomersView(this).Show();
        }

        /// <summary>
        /// Button for open the drones view
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            this.ShowDrones.IsEnabled = false;
            new DronesView(this).Show();
        }

        /// <summary>
        /// Button for open the stations view
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            this.ShowStations.IsEnabled = false;

            new StationsView(this).Show();
        }

        /// <summary>
        /// Button for open the packages view
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void ShowPackages_Click(object sender, RoutedEventArgs e)
        {
            this.ShowPackages.IsEnabled = false;

            new PackagesView(this).Show();
        }
    }
}
