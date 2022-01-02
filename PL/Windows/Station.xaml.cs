using BlApi;
using BO;
using System;
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
    /// Interaction logic for Station.xaml 
    /// </summary>
    public partial class Station : Window
    {
        IBL bl;
        StationsView sender;
        PO.Station station;
        private StationsView stationView;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="bl">The variable of access to the logic layer</param>
        /// <param name="sender">The element that activates the function</param>
        public Station(IBL bl, StationsView stationView)
        {
            InitializeComponent();
            this.bl = bl;
            this.stationView = stationView;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            stationId.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            stationLocation.Visibility = Visibility.Visible;
            freeChargeSlots.Visibility = Visibility.Visible;

        }


        public Station(IBL bl, StationsView sender, PO.Station POStation)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;
            this.station = POStation;

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);

            StationInfoDownGrid.Visibility = Visibility.Visible;
            StationIdInfo.Visibility = Visibility.Visible;
            UpdateNameGrid.Visibility = Visibility.Visible;
            StationLocationInfo.Visibility = Visibility.Visible;
            UpdateNumOfChargeGrid.Visibility = Visibility.Visible;           
            ChargingDronesInfo.Visibility = Visibility.Visible;

            this.Height = 700;
            this.Width = 550;
            this.DataContext = station;

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
                if (stationId.Text != "" && name.Text != "" && stationLocation.Text != "" && freeChargeSlots.Text != null)
                {
                    bl.AddStation(new()
                    {
                        Id = int.Parse(stationId.Text),
                        Name = name.Text,
                        LocationOfStation = new Location(),
                        FreeChargeSlots = int.Parse(freeChargeSlots.Text),
                        ChargingDrones = new List<DroneCharge>()
                    });

                   // this.sender.Filtering();

                    MessageBox.Show("Adding the station was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("There are unfilled fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (ExistsNumberException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }


        }


        /// <summary>
        /// A button pressed opens the option to update the drone model and changes the button to OK at the touch of a button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Update.Content = "OK";
            UpdateName.IsReadOnly = false;
            UpdateName.Text = "";

            Update.Click -= Update_Click;
            Update.Click += OK_Click;
        }

        /// <summary>
        /// Confirmation button for updating the model.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateName.Text != ""  )
            {
                bl.UpdateStation(station.Id, UpdateName.Text, station.FreeChargeSlots);
              //  this.sender.Filtering();
                MessageBox.Show("Updating the element was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                Update.Content = "Update";
                UpdateName.IsReadOnly = true;

                Update.Click -= OK_Click;
                Update.Click += Update_Click;
            }
            else
                MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

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
