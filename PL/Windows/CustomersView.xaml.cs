using BlApi;
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
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : Window
    {
        IBL bl;
        MainWindow sender;

        bool isCloseClick = true;


        public CustomersView(IBL bl, MainWindow sender)
        {
            InitializeComponent();

            this.bl = bl;
            this.sender = sender;

            //StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //MaxWeigth.ItemsSource = Enum.GetValues(typeof(Weight));
            this.NormalView.DataContext = this.sender.Customers;
           this.GroupingView.DataContext = from drone in this.sender.Customers
                                            group drone by drone.DroneStatus;


            this.sender.Customers.CollectionChanged += Customers_CollectionChanged;
            this.sender.Closing += Sender_Closing;
            this.sender.Activated += Sender_Activated;
            this.sender.Deactivated += Sender_Deactivated;

        }

        private void Customers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //this.GroupingView.DataContext = from drone in this.sender.Drones
            //                                group drone by drone.DroneStatus;
        }


        private void Sender_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = false;
        }

        private void Sender_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        private void Sender_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
              Exit_Click(sender, null);
        }


        /// <summary>
        /// Closing the window
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ((Button)this.sender.FindName("ShowCustomers")).IsEnabled = true;
        }

        /// <summary>
        /// A button that resets the filters.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //MaxWeigth.Text = "";
            //MaxWeigth.SelectedIndex = -1;
            //MaxWeigth.SelectedItem = null;

            //StatusSelector.Text = "";
            //StatusSelector.SelectedIndex = -1;
            //StatusSelector.SelectedItem = null;

            //Filtering();
        }

        /// <summary>
        /// A button that opens a window for adding a new drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new Customer(bl, this).ShowDialog();
        }

        private void CustomersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                BO.Customer BOCustomer = bl.GetCustomer((((ListView)sender).SelectedItem as BO.CustomerToList).CustomerId);
                PO.Customer POCustomer = this.sender.POCustomers.Find(dr => dr.Id == BOCustomer.Id);
                if (POCustomer == null)
                    this.sender.POCustomers.Add(POCustomer = new PO.Customer().CopyFromBOCustomer(BOCustomer));

                new Customer(bl, this, POCustomer).Show();

            }

        }
        /// <summary>
        /// Window Close Button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            isCloseClick = false;
            this.Close();
        }


        /// <summary>
        /// Prevent the window from closing by a non-cancel button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = isCloseClick;
        }
    }
}
