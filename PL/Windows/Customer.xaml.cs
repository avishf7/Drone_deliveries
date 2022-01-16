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


        /// <summary>
        /// The window that opens this window.
        /// </summary>
        public Window Sender { get; set; }

        /// <summary>
        /// The variable from which the display opens
        /// </summary>
        public PO.Customer POCustomer { get; set; }

        /// <summary>
        ///Contains all the data needed for the display.
        /// </summary>
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// constructor to window add customer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Customer(Window sender)
        {
            this.Sender = sender;
            InitializeComponent();

            WindowStyle = WindowStyle.None;
            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            AddCustomerLocationGrid.Visibility = Visibility.Visible;
            customerId.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            phone.Visibility = Visibility.Visible;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="sender">The element that activates the function</param>

        /// <summary>
        ///  Consructor or customer display fwindow.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="customer">The customer intended for display</param>
        public Customer(Window sender, PO.Customer customer)
        {
            this.Sender = sender;
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
        /// Add customer button
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
        /// A button pressed opens the option to update elements in customer and changes the button to OK at the touch of a button.
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
        /// Confirmation button for updating the elements in customer.
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

                Model.UpdatePackages();

                MessageBox.Show("Updating the element was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                ((Button)sender).Content = "Update";
                updateElement.IsReadOnly = true;

                ((Button)sender).Click -= OK_Click;
                ((Button)sender).Click += Update_Click;
            }
            else
                MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Sets that by double-clicking a package from the list it will see the data on the packageAtCustomer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void PackageAtCustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {
                BO.Package BOPackage = bl.GetPackage((listView.SelectedItem as PackageAtCustomer).PackageId);
                PO.Package POPackage = Model.POPackages.Find(pck => pck.Id == BOPackage.Id);
                if (POPackage == null)
                    Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                new Package(this, POPackage).Show();
            }
        }


        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
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
        }
    }

