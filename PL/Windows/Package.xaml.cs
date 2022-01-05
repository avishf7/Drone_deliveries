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
        IBL bl = BlFactory.GetBl();
        PackagesView sender;

        public PO.Package POPackage { get; set; }
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Package(PackagesView sender)
        {
            InitializeComponent();
            this.sender = sender;            

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            senderCustomerInPackage.Visibility = Visibility.Visible;
            targetCustomerInPackage.Visibility = Visibility.Visible;
            Weight.Visibility = Visibility.Visible;
            priority.Visibility = Visibility.Visible;
        
        }

        /// <summary>
        /// constructor to window add drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Package(PackagesView sender, PO.Package package)
        {
            this.sender = sender;
            this.POPackage = package;

            InitializeComponent();
           


            PackageInfoDownGrid.Visibility = Visibility.Visible;           
            SenderCustomerInPackageInfo.Visibility = Visibility.Visible;
            TargetCustomerInPackageInfo.Visibility = Visibility.Visible;
            WeightInfo.Visibility = Visibility.Visible;
            PriorityInfo.Visibility = Visibility.Visible;
            Exit.Visibility = Visibility.Visible;

            this.Height = 550;
            this.Width = 1300;

        }

        /// <summary>
        /// A button that alerts if the user has entered characters rather than numbers.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Checks that entered numbers only
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "" && d <= 0)
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
                if (senderCustomerInPackage.Text != "" && targetCustomerInPackage.Text != "" && Weight.SelectedItem != null && priority.SelectedItem != null)
                {
                    bl.AddPackage(new()
                    {                       
                      SenderCustomerInPackage = new() {CustomerId = int.Parse (senderCustomerInPackage.Text)},
                        TargetCustomerInPackage = new() {CustomerId = int.Parse (targetCustomerInPackage.Text)},
                        Weight = (Weight)Weight.SelectedItem,
                        Priority = (Priorities)priority.SelectedItem,
                    });

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
        /// Sets that by double-clicking a skimmer from the list it will see the data on the skimmer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void DroneInPackageInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
                      
            if (((TextBox)sender).DataContext != null)
            {
                BO.Drone BODrone = bl.GetDrone((((TextBox)sender).DataContext as BO.DroneInPackage).Id);
                PO.Drone PODrone = Model.PODrones.Find(dr => dr.Id == BODrone.Id);
                if (PODrone == null)
                    Model.PODrones.Add(PODrone = new PO.Drone().CopyFromBODrone(BODrone));

                new Drone(this, PODrone).Show();
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
