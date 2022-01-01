using System;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for Package.xaml
    /// </summary>
    public partial class Package : Window
    {
        IBL bl;
        PackagesView sender;
        PO.Package package;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="bl">The variable of access to the logic layer</param>
        /// <param name="sender">The element that activates the function</param>
        public Package(IBL bl, PackagesView sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            packageId.Visibility = Visibility.Visible;
            senderCustomerInPackage.Visibility = Visibility.Visible;
            targetCustomerInPackage.Visibility = Visibility.Visible;
            Weight.ItemsSource = Enum.GetValues(typeof(Weight));
            priority.ItemsSource = Enum.GetValues(typeof(Priorities));
            droneInPackage.Visibility = Visibility.Visible;
            requested.Visibility = Visibility.Visible;
            scheduled.Visibility = Visibility.Visible;
            pickedUp.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor to window add drone.
        /// </summary>
        /// <param name="bl">The variable of access to the logic layer</param>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="droneId">The ID of the drone intended for display</param>
        public Package(IBL bl, PackagesView sender, PO.Package package)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;
            this.package = package;

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);


            //DroneInfoDownGrid.Visibility = Visibility.Visible;
            //DroneIdInfo.Visibility = Visibility.Visible;
            //UpdateModelGrid.Visibility = Visibility.Visible;
            //MaxWeightInfo.Visibility = Visibility.Visible;
            //DroneLocationInfo.Visibility = Visibility.Visible;

            this.Height = 700;
            this.Width = 550;
            this.DataContext = package;

        }

        /// <summary>
        /// A button that alerts if the user has entered characters rather than numbers.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Checks that entered numbers only
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "")
                MessageBox.Show("Please enter only numbers.");

        }

        /// <summary>
        /// Window Close Button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add drone button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (packageId.Text != "" && senderCustomerInPackage.Text != "" && targetCustomerInPackage.Text != "" && Weight.SelectedItem != null && priority.SelectedItem != null)
                {
                    bl.AddPackage(new()
                    {
                        Id = int.Parse(packageId.Text),
                      //  SenderCustomerInPackage = senderCustomerInPackage.Text,
                        //TargetCustomerInPackage = targetCustomerInPackage.Text,
                        Weight = (Weight)Weight.SelectedItem,
                        Priority = (Priorities)priority.SelectedItem,
                    });

                  //  this.sender.Filtering();

                    MessageBox.Show("Adding the drone was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("There are unfilled fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (ExistsNumberException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }


        }

    

        ///// <summary>
        ///// Button for sending drone for charging and release from charging according to the status of the drone.
        ///// </summary>
        ///// <param name="sender">The element that activates the function</param>
        ///// <param name="e"></param>
        //private void Charge_Click(object sender, RoutedEventArgs e)
        //{
        //    switch (drone.DroneStatus)
        //    {
        //        case DroneStatuses.Available:
        //            try
        //            {
        //                bl.SendDroneForCharge(drone.Id);
        //                drone.CopyFromBODrone(bl.GetDrone(drone.Id));
        //                this.sender.Filtering();
        //                MessageBox.Show("Sent for charging", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

        //            }
        //            catch (NotEnoughBattery ex)
        //            {
        //                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }

        //            break;
        //        case DroneStatuses.Maintenance:
        //            bl.RealeseDroneFromCharge(drone.Id);
        //            drone.CopyFromBODrone(bl.GetDrone(drone.Id));
        //            this.sender.Filtering();
        //            MessageBox.Show("Released from charging", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);


        //            break;
        //    }
        //}
        ///// <summary>
        ///// A button that handles the delivery of the package according to the status of the drone.
        ///// </summary>
        ///// <param name="sender">The element that activates the function</param>
        ///// <param name="e"></param>
        //private void Delivery_Click(object sender, RoutedEventArgs e)
        //{
        //    switch (drone.DroneStatus)
        //    {
        //        case DroneStatuses.Available:
        //            try
        //            {
        //                bl.packageAssigning(drone.Id);
        //                drone.CopyFromBODrone(bl.GetDrone(drone.Id));
        //                this.sender.Filtering();
        //                MessageBox.Show("The package was successfully associated", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);


        //            }
        //            catch (NoSuitablePackageForScheduledException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        //            break;
        //        case DroneStatuses.Sendering:
        //            if (drone.PackageInProgress.IsCollected)
        //            {
        //                bl.Deliver(drone.Id);
        //                drone.CopyFromBODrone(bl.GetDrone(drone.Id));
        //                this.sender.Filtering();
        //                MessageBox.Show("The package was delivered to its destination, good day", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

        //            }
        //            else
        //            {
        //                bl.PickUp(drone.Id);
        //                drone.CopyFromBODrone(bl.GetDrone(drone.Id));
        //                this.sender.Filtering();
        //                MessageBox.Show("The package was successfully collected by the drone", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

        //            }
        //            break;
        //    }
        //}
    }
}
