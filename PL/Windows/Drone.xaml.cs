using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        readonly IBL bl = BlFactory.GetBl();
        readonly BackgroundWorker worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        bool waitToExit = false;

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

            MainGrid.RowDefinitions[0].Height = new(60, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(40, GridUnitType.Star);


            DroneInfoDownGrid.Visibility = Visibility.Visible;
            DroneIdInfo.Visibility = Visibility.Visible;
            UpdateModelGrid.Visibility = Visibility.Visible;
            MaxWeightInfo.Visibility = Visibility.Visible;
            DroneLocationInfo.Visibility = Visibility.Visible;

            this.Height = 800;
            this.Width = 1200;

            //If the window that opened the new window closes, the new window will also close.
            this.Sender.Closed += Sender_Closed;

            this.worker.DoWork += Worker_DoWork;
            this.worker.ProgressChanged += Worker_ProgressChanged;
            this.worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        /// <summary>
        ///  If the window that opened the new window closes, the new window will also close.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Sender_Closed(object sender, EventArgs e)
        {
            Cancel_Click(sender, null);
        }

        /// <summary>
        /// A button that alerts if the user has entered characters rather than numbers.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Checks that entered numbers only
            if (e.Handled = !int.TryParse(e.Text, out int d) && e.Text != "" || d < 0)
                MessageBox.Show("Please enter only positive numbers.");
        }

        /// <summary>
        /// Window Close Button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add drone button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (droneId.Text != "" && model.Text != "" && maxWeight.SelectedItem != null && stations.SelectedItem != null)
                {
                    //Checks that the number entered can be entered into int32
                    if (e.Handled = !int.TryParse(droneId.Text, out int b))
                        MessageBox.Show("Invalid input", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        var stationToList = stations.SelectedItem as StationToList;

                        bl.AddDrone(new()
                        {
                            Id = b,
                            Model = model.Text,
                            MaxWeight = (Weight)maxWeight.SelectedItem,
                        }, stationToList.Id);

                        

                        Model.UpdateDrones();
                        Model.UpdateStations();
                        Model.UpdatePOStation(bl.GetStation(stationToList.Id).LocationOfStation);

                        MessageBox.Show("Adding the drone was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
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
                    string messege;

                    if (PODrone.PackageInProgress.IsCollected)
                    {
                        bl.Deliver(PODrone.Id);
                        messege = "The package was delivered to its destination, good day";
                    }
                    else
                    {
                        bl.PickUp(PODrone.Id);
                        messege = "The package was successfully collected by the drone";

                    }

                    var packageId = PODrone.PackageInProgress.Id;
                    PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                    Model.UpdateDrones();
                    Model.UpdatePackages();
                    Model.UpdatePOPackage(packageId);
                    Model.UpdateCustomers();

                    MessageBox.Show(messege, "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
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
                var BOPackage = bl.GetPackage((textBox.DataContext as PackageInTransfer).Id);
                PO.Package POPackage = Model.POPackages.Find(pck => pck.Id == BOPackage.Id);
                if (POPackage == null)
                    Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                new Package(this, POPackage).Show();
            }
            else
                MessageBox.Show("No element exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Changes in the drone window by pressing the simulator button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Content = "Stop simulator";

            //Change the button press function to Stop Simulator.
            Simulator.Click -= Simulator_Click;
            Simulator.Click += Stop_Click;

            //Disappearing action buttons in the window.
            Charge.Visibility = Visibility.Hidden;
            Delivery.Visibility = Visibility.Hidden;

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Changes in the drone window after the simulator has stopped working.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!waitToExit)
            {
                //Return the action buttons in the window.
                Charge.Visibility = Visibility.Visible;
                Delivery.Visibility = Visibility.Visible;
                Cursor = Cursors.Arrow;

                Simulator.Content = "Simulator";

                //Change the button press function to Simulator.
                Simulator.Click -= Stop_Click;
                Simulator.Click += Simulator_Click;
            }
            else
                this.Close();
        }

        /// <summary>
        /// The process that changes the display in the drone window according to the simulator.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (PODrone.DroneStatus)
            {
                case DroneStatuses.Available:
                    PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                    Model.UpdateDrones();
                    Cursor = Cursors.Wait;
                    break;
                case DroneStatuses.Maintenance:
                    Cursor = ((BackgroundWorker)sender).CancellationPending ? Cursors.Wait : Cursors.Arrow;
                    PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                    Model.UpdateDrones();
                    Model.UpdateStations();
                    Model.UpdatePOStation(PODrone.LocationOfDrone);
                    break;
                case DroneStatuses.Sendering:
                    Cursor = ((BackgroundWorker)sender).CancellationPending ? Cursors.Wait : Cursors.Arrow;
                    var packageId = PODrone.PackageInProgress.Id;
                    PODrone.CopyFromBODrone(bl.GetDrone(PODrone.Id));
                    Model.UpdateDrones();
                    Model.UpdatePackages();
                    Model.UpdatePOPackage(packageId);
                    Model.UpdateCustomers();
                    break;
            }

        }


        /// <summary>
        /// The procession ran in the background.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.StartSimulator(() => ((BackgroundWorker)sender).ReportProgress(0), () => !((BackgroundWorker)sender).CancellationPending, PODrone.Id);
        }


        /// <summary>
        /// Stops the procession.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (waitToExit = e.Cancel = worker.IsBusy)
                Stop_Click(sender, null);
        }
    }
}