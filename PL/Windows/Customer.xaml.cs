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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        IBL bl = BlFactory.GetBl();
        Window sender;

        public PO.Customer POCustomer { get; set; }
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// Consructor for drone display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Customer(Window sender)
        {
            InitializeComponent();
            this.sender = sender;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            AddCustomerLocationGrid.Visibility = Visibility.Visible;
            customerId.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            phone.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// constructor to window add customer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Customer(Window sender, PO.Customer customer)
        {
            this.sender = sender;
            this.POCustomer = customer;

            InitializeComponent();
           
            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);


            CustomerInfoDownGrid.Visibility = Visibility.Visible;
            CustomerIdInfo.Visibility = Visibility.Visible;
            UpdateNameGrid.Visibility = Visibility.Visible;
            UpdatePhoneGrid.Visibility = Visibility.Visible;
            CustomerLocationInfo.Visibility = Visibility.Visible;


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
        /// Add drone button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (customerId.Text != "" && name.Text != "" && phone.Text != "" && customerLocationLongitude.Text != "" && customerLocationLattitude.Text != "")
                {
                    bl.AddCustomer(new()
                    {
                        Id = int.Parse(customerId.Text),
                        Name = name.Text,
                        Phone = phone.Text,
                        CustomerLocation = new()
                        {
                            Lattitude = int.Parse(customerLocationLattitude.Text),
                            Longitude = int.Parse(customerLocationLongitude.Text)
                        },
                        PackageAtCustomerFromCustomer = new List<PackageAtCustomer>(),
                        PackageAtCustomerToCustomer = new List<PackageAtCustomer>()
                    });

                    Model.UpdateCustomers();

                    MessageBox.Show("Adding the customer was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
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
            var updateElement = ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0));

            ((Button)sender).Content = "OK";
            updateElement.IsReadOnly = false;
            updateElement.Text = "";

            ((Button)sender).Click -= Update_Click;
            ((Button)sender).Click += OK_Click;
        }

        /// <summary>
        /// Confirmation button for updating the model.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
           var updateElement = ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0));
 
            if (updateElement.Text != "")
            {
                bl.UpdateCustomer(POCustomer.Id, UpdateName.Text, UpdatePhone.Text);
                Model.UpdateCustomers();
                foreach (var pck in POCustomer.PackageAtCustomerFromCustomer)
                    Model.UpdatePOPackage(pck.PackageId);
                foreach (var pck in POCustomer.PackageAtCustomerToCustomer)
                    Model.UpdatePOPackage(pck.PackageId);
                Model.UpdatePackage();
                MessageBox.Show("Updating the element was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                ((Button)sender).Content = "Update";
                updateElement.IsReadOnly = true;

                ((Button)sender).Click -= OK_Click;
                ((Button)sender).Click += Update_Click;
            }
            else
                MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void PackageAtCustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).DataContext != null)
            {
                BO.Package BOPackage = bl.GetPackage((((ListView)sender).DataContext as BO.PackageAtCustomer).PackageId);
                PO.Package POPackage = Model.POPackages.Find(pck => pck.Id == BOPackage.Id);
                if (POCustomer == null)
                    Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                new Package(this, POPackage).Show();
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
