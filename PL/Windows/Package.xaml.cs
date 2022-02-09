using System;
using BlApi;
using BO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for Package.xaml
    /// </summary>
    public partial class Package : Window
    {
        readonly IBL bl = BlFactory.GetBl();

        /// <summary>
        /// The window that opens this window.
        /// </summary>
        public Window Sender { get; set; }

        /// <summary>
        /// The variable from which the display opens
        /// </summary>
        public PO.Package POPackage { get; set; }

        /// <summary>
        ///Contains all the data needed for the display.
        /// </summary>
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// constructor to window add package.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Package(Window sender)
        {
            this.Sender = sender;
            InitializeComponent();

            WindowStyle = WindowStyle.None;
            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            senderCustomerInPackage.Visibility = Visibility.Visible;
            targetCustomerInPackage.Visibility = Visibility.Visible;
            Weight.Visibility = Visibility.Visible;
            priority.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Consructor for package display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="package">The package intended for display</param>
        public Package(Window sender, PO.Package package)
        {
            this.Sender = sender;
            this.POPackage = package;

            InitializeComponent();

            AddDownInfoGrid.Visibility = Visibility.Visible;
            PackageInfoDownGrid.Visibility = Visibility.Visible;
            SenderCustomerInPackageInfo.Visibility = Visibility.Visible;
            TargetCustomerInPackageInfo.Visibility = Visibility.Visible;
            WeightInfo.Visibility = Visibility.Visible;
            PriorityInfo.Visibility = Visibility.Visible;
            DeleteGrid.Visibility = Visibility.Visible;

            this.Height = 550;
            this.Width = 1300;

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
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "" && d <= 0)
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
        /// Add package button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (senderCustomerInPackage.SelectedItem != null && targetCustomerInPackage.SelectedItem != null && Weight.SelectedItem != null && priority.SelectedItem != null)
                {
                    int senderId = ((CustomerToList)senderCustomerInPackage.SelectedItem).CustomerId;
                    int targetId = ((CustomerToList)targetCustomerInPackage.SelectedItem).CustomerId;

                    try
                    {
                        bl.AddPackage(new()
                        {
                            SenderCustomerInPackage = new() { CustomerId = senderId },
                            TargetCustomerInPackage = new() { CustomerId = targetId },
                            Weight = (Weight)Weight.SelectedItem,
                            Priority = (Priorities)priority.SelectedItem,
                        });
                    }
                    catch (NotValidTargetException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }


                    Model.UpdatePackages();

                    Model.POCustomers.Find(cus => cus.Id == senderId)?.CopyFromBOCustomer(bl.GetCustomer(senderId));
                    Model.POCustomers.Find(cus => cus.Id == targetId)?.CopyFromBOCustomer(bl.GetCustomer(targetId));

                    Model.UpdateCustomers();

                    MessageBox.Show("Adding the package was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("There are unfilled fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (ExistsNumberException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }


        }

        /// <summary>
        /// Delete a package
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? ", "Notice", button: MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    bl.DeletePackage(POPackage.Id);

                    int senderId = POPackage.SenderCustomerInPackage.CustomerId;
                    int targetId = POPackage.TargetCustomerInPackage.CustomerId;
                    Model.UpdatePackages();


                    Model.POCustomers.Find(cus => cus.Id == senderId)?.CopyFromBOCustomer(bl.GetCustomer(senderId));
                    Model.POCustomers.Find(cus => cus.Id == targetId)?.CopyFromBOCustomer(bl.GetCustomer(targetId));

                    Model.UpdateCustomers();

                    MessageBox.Show("Delete the package was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (PakcageConnectToDroneException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

            this.Close();
        }

        private void CustomerInPackageInfo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                if (((TextBox)sender).DataContext != null)
                {
                    BO.Customer BOCustomer = bl.GetCustomer((((TextBox)sender).DataContext as BO.CustomerInPackage).CustomerId);
                    PO.Customer POCustomer = Model.POCustomers.Find(cus => cus.Id == BOCustomer.Id);
                    if (POCustomer == null)
                        Model.POCustomers.Add(POCustomer = new PO.Customer().CopyFromBOCustomer(BOCustomer));

                    new Customer(this, POCustomer).Show();
                }
                else
                    MessageBox.Show("No element exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DroneInPackageInfo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            { 
                TextBox textBox = (TextBox)sender;

                if (textBox.DataContext != null)
                {
                    if ((textBox.DataContext as BO.DroneInPackage) != null)
                    {
                        BO.Drone BODrone = bl.GetDrone((textBox.DataContext as DroneInPackage).Id);
                        PO.Drone PODrone = Model.PODrones.Find(dr => dr.Id == BODrone.Id);
                        if (PODrone == null)
                            Model.PODrones.Add(PODrone = new PO.Drone().CopyFromBODrone(BODrone));

                        new Drone(this, PODrone).Show();
                    }
                    else
                        MessageBox.Show("No element exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
