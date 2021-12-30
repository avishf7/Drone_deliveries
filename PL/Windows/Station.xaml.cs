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
        DronesView sender;
        PO.Drone drone;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="bl">The variable of access to the logic layer</param>
        /// <param name="sender">The element that activates the function</param>
        public Drone(IBL bl, DronesView sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            droneId.Visibility = Visibility.Visible;
            model.Visibility = Visibility.Visible;
            maxWeight.Visibility = Visibility.Visible;
            stations.Visibility = Visibility.Visible;
            maxWeight.ItemsSource = Enum.GetValues(typeof(Weight));
            stations.ItemsSource = bl.GetStations();

        }

        /// <summary>
        /// constructor to window add drone.
        /// </summary>
        /// <param name="bl">The variable of access to the logic layer</param>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="droneId">The ID of the drone intended for display</param>
        public Drone(IBL bl, DronesView sender, PO.Drone drone)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;
            this.drone = drone;

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);


            DroneInfoDownGrid.Visibility = Visibility.Visible;
            DroneIdInfo.Visibility = Visibility.Visible;
            UpdateModelGrid.Visibility = Visibility.Visible;
            MaxWeightInfo.Visibility = Visibility.Visible;
            DroneLocationInfo.Visibility = Visibility.Visible;

            this.Height = 700;
            this.Width = 550;
            this.DataContext = drone;

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
                if (droneId.Text != "" && model.Text != "" && maxWeight.SelectedItem != null && stations.SelectedItem != null)
                {
                    bl.AddDrone(new()
                    {
                        Id = int.Parse(droneId.Text),
                        Model = model.Text,
                        MaxWeight = (Weight)maxWeight.SelectedItem,
                    }, ((StationToList)stations.SelectedItem).Id);

                    this.sender.Filtering();

                    MessageBox.Show("Adding the drone was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
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
            UpdateModel.IsReadOnly = false;
            UpdateModel.Text = "";

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
            if (UpdateModel.Text != "")
            {
                bl.UpdateDrone(drone.Id, UpdateModel.Text);
                this.sender.Filtering();
                MessageBox.Show("Updating the drone was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                Update.Content = "Update";
                UpdateModel.IsReadOnly = true;

                Update.Click -= OK_Click;
                Update.Click += Update_Click;
            }
            else
                MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Button for sending drone for charging and release from charging according to the status of the drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            switch (drone.DroneStatus)
            {
                case DroneStatuses.Available:
                    try
                    {
                        bl.SendDroneForCharge(drone.Id);
                        drone.CopyFromBODrone(bl.GetDrone(drone.Id));
                        this.sender.Filtering();
                        MessageBox.Show("Sent for charging", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (NotEnoughBattery ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;
                case DroneStatuses.Maintenance:
                    bl.RealeseDroneFromCharge(drone.Id);
                    drone.CopyFromBODrone(bl.GetDrone(drone.Id));
                    this.sender.Filtering();
                    MessageBox.Show("Released from charging", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);


                    break;
            }
        }
        /// <summary>
        /// A button that handles the delivery of the package according to the status of the drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Delivery_Click(object sender, RoutedEventArgs e)
        {
            switch (drone.DroneStatus)
            {
                case DroneStatuses.Available:
                    try
                    {
                        bl.packageAssigning(drone.Id);
                        drone.CopyFromBODrone(bl.GetDrone(drone.Id));
                        this.sender.Filtering();
                        MessageBox.Show("The package was successfully associated", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    catch (NoSuitablePackageForScheduledException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                    break;
                case DroneStatuses.Sendering:
                    if (drone.PackageInProgress.IsCollected)
                    {
                        bl.Deliver(drone.Id);
                        drone.CopyFromBODrone(bl.GetDrone(drone.Id));
                        this.sender.Filtering();
                        MessageBox.Show("The package was delivered to its destination, good day", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        bl.PickUp(drone.Id);
                        drone.CopyFromBODrone(bl.GetDrone(drone.Id));
                        this.sender.Filtering();
                        MessageBox.Show("The package was successfully collected by the drone", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    break;
            }
        }
    }
}
