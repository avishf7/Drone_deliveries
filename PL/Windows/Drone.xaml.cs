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
using BlApi;
using BO;



namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL bl = BlFactory.GetBl();


        /// <summary>
        /// The window that opens this window.
        /// </summary>
        public Window Sender { get; set; }

        /// <summary>
        /// The variable from which the display opens
        /// </summary>
        public PO.Drone PODrone { get; set; }

        /// <summary>
        ///Contains all the data needed for the display.
        /// </summary>
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// constructor to window add drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Drone(Window sender)
        {
            this.Sender = sender;
            InitializeComponent();



            WindowStyle = WindowStyle.None;
            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            droneId.Visibility = Visibility.Visible;
            model.Visibility = Visibility.Visible;
            maxWeight.Visibility = Visibility.Visible;
            stations.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///  Consructor for drone display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="drone">The drone intended for display</param>
        public Drone(Window sender, PO.Drone drone)
        {
            this.Sender = sender;
            this.PODrone = drone;

            InitializeComponent();

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);


            DroneInfoDownGrid.Visibility = Visibility.Visible;
            DroneIdInfo.Visibility = Visibility.Visible;
            UpdateModelGrid.Visibility = Visibility.Visible;
            MaxWeightInfo.Visibility = Visibility.Visible;
            DroneLocationInfo.Visibility = Visibility.Visible;

            this.Height = 800;
            this.Width = 600;

            //If the window that opened the new window closes, the new window will also close.
            this.Sender.Closed += Sender_Closed;
        }

        /// <summary>
        ///  If the window that opened the new window closes, the new window will also close.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Sender_Closed(object sender, EventArgs e)
        {
            cancel_Click(sender, null);
        }

        /// <summary>
        /// A button that alerts if the user has entered characters rather than numbers.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Checks that entered numbers only
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "" || d <= 0)
                MessageBox.Show("Please enter only positive numbers.");

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


                    Model.UpdateDrones();

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
                bl.UpdateDrone(PODrone.Id, UpdateModel.Text);

                Model.UpdateDrones();

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
            switch (PODrone.DroneStatus)
            {
                case DroneStatuses.Available:
                    try
                    {
                        bl.SendDroneForCharge(PODrone.Id);
                        PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                        Model.UpdateDrones();
                        Model.UpdateStations();
                        Model.UpdatePOStation(PODrone.LocationOfDrone);
                        MessageBox.Show("Sent for charging", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (NotEnoughBattery ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;
                case DroneStatuses.Maintenance:
                    bl.RealeseDroneFromCharge(PODrone.Id);
                    PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                    Model.UpdateDrones();
                    Model.UpdateStations();
                    Model.UpdatePOStation(PODrone.LocationOfDrone);
                   
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
            switch (PODrone.DroneStatus)
            {
                case DroneStatuses.Available:
                    try
                    {
                        bl.PackageAssigning(PODrone.Id);
                        PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                        Model.UpdateDrones();
                        Model.UpdatePackages();
                        Model.UpdatePOPackage(PODrone.PackageInProgress.Id);
                        Model.UpdateCustomers();
                        MessageBox.Show("The package was successfully associated", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    catch (NoSuitablePackageForScheduledException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                    break;
                case DroneStatuses.Sendering:
                    if (PODrone.PackageInProgress.IsCollected)
                    {
                        int providedPckageId = PODrone.PackageInProgress.Id;
                        bl.Deliver(PODrone.Id);
                        PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                        Model.UpdateDrones();
                        Model.UpdatePackages();
                        Model.UpdatePOPackage(providedPckageId);
                        Model.UpdateCustomers();
                        MessageBox.Show("The package was delivered to its destination, good day", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        bl.PickUp(PODrone.Id);
                        PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                        Model.UpdateDrones();
                        Model.UpdatePackages();
                        Model.UpdatePOPackage(PODrone.PackageInProgress.Id);
                        Model.UpdateCustomers();
                        MessageBox.Show("The package was successfully collected by the drone", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    break;
            }
        }


        /// <summary>
        /// Sets that by double-clicking a package from the list it will see the data on the PackageInTransfer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void PackageInProgress_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.DataContext != null)
            {
                    var BOPackage = bl.GetPackage((textBox.DataContext as BO.PackageInTransfer).Id);
                    PO.Package POPackage = Model.POPackages.Find(pck => pck.Id == BOPackage.Id);
                    if (POPackage == null)
                        Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                    new Package(this, POPackage).Show();                
            }
            else
                MessageBox.Show("No element exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void PackageInProgress_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                TextBox textBox = (TextBox)sender;

                if (textBox.DataContext != null)
                {
                    var BOPackage = bl.GetPackage((textBox.DataContext as BO.PackageInTransfer).Id);
                    PO.Package POPackage = Model.POPackages.Find(pck => pck.Id == BOPackage.Id);
                    if (POPackage == null)
                        Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                    new Package(this, POPackage).Show();
                }
                else
                    MessageBox.Show("No element exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
