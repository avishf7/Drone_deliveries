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
        IBL bl = BlFactory.GetBl();
        StationsView sender;        

        public PO.Station POStation { get; set; }
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Station(StationsView sender)
        {
            InitializeComponent();
            this.sender = sender;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            AddStationGrid.Visibility = Visibility.Visible;
            stationId.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            stationLocationLongitude.Visibility = Visibility.Visible;
            stationLocationLattitude.Visibility = Visibility.Visible;
            freeChargeSlots.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="POStation"></param>
        public Station(StationsView sender, PO.Station POStation)
        {

            this.sender = sender;
            this.POStation = POStation;
         
            InitializeComponent();

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);

            StationInfoDownGrid.Visibility = Visibility.Visible;
            StationIdInfo.Visibility = Visibility.Visible;
            UpdateNameGrid.Visibility = Visibility.Visible;
 
            StationLocationInfo.Visibility = Visibility.Visible;
            UpdateNumOfChargeGrid.Visibility = Visibility.Visible;           
            ChargingDronesInfo.Visibility = Visibility.Visible;
           // freeChargeSlotsInfo.Visibility = Visibility.Visible;

            this.Height = 700;
            this.Width = 550;
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
        /// Add station button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stationId.Text != "" && name.Text != "" && stationLocationLongitude.Text != "" && stationLocationLattitude.Text != "" && freeChargeSlots.Text != null)
                {
                    bl.AddStation(new()
                    {
                        Id = int.Parse(stationId.Text),
                        Name = name.Text,
                        LocationOfStation = new Location(),
                        FreeChargeSlots = int.Parse(freeChargeSlots.Text),
                        ChargingDrones = new List<DroneCharge>()
                    });

                    Model.UpdateStations();

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
        /// A button pressed opens the option to update the station and changes the button to OK at the touch of a button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "OK";
            ((TextBox) VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender),0)).IsReadOnly=false;
            ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).Text = "";

            ((Button)sender).Click -= Update_Click;
            ((Button)sender).Click += OK_Click;
        }

        /// <summary>
        /// Confirmation button for updating the station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {         

            if (((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).Text != "")
            {
                bl.UpdateStation(POStation.Id, UpdateName.Text, POStation.FreeChargeSlots);

                MessageBox.Show("Updating the element was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                ((Button)sender).Content = "Update";
                ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).IsReadOnly = true;

                ((Button)sender).Click -= OK_Click;
                ((Button)sender).Click += Update_Click;
            }
            else
                MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TooSmallAmount ex)
            {

                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
