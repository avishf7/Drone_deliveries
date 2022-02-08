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
        readonly IBL bl = BlFactory.GetBl();

        /// <summary>
        /// The window that opens this window.
        /// </summary>
        readonly MainWindow sender;

        //That they will not be able to close the window with the X button
        bool isCloseClick = true;

        /// <summary>
        ///Contains all the data needed for the display.
        /// </summary>
        public Model Model { get; } = PL.Model.Instance;


        /// <summary>
        ///  constructor to create window of customers view.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public CustomersView(MainWindow sender)
        {
            InitializeComponent();
            this.sender = sender;


            //If the window that opened the new window closes, the new window will also close.
            this.sender.Closing += Sender_Closing;

            // Causes the window that opens to be above the main window.
            this.sender.Activated += Sender_Activated;

            // When the main window does not open another window it will be TOP MOST.
            this.sender.Deactivated += Sender_Deactivated;
        }


        /// <summary>
        /// Makes the sender nut be up.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Sender_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = false;
        }

        /// <summary>
        ///Makes the sender be up.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Sender_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        /// <summary>
        /// If the window that opened the new window closes, the new window will also close.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
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
        /// A button that opens a window for adding a new customer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new Customer(this).ShowDialog();
        }

        /// <summary>
        ///  Sets that by double-clicking a customer from the list it will see the data on the customer.
        /// </summary>
        /// <param name="sender">>The element that activates the function</param>
        /// <param name="e"></param>
        private void CustomersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                BO.Customer BOCustomer = bl.GetCustomer((((ListView)sender).SelectedItem as BO.CustomerToList).CustomerId);
                PO.Customer POCustomer = Model.POCustomers.Find(dr => dr.Id == BOCustomer.Id);
                if (POCustomer == null)
                    Model.POCustomers.Add(POCustomer = new PO.Customer().CopyFromBOCustomer(BOCustomer));

                new Customer(this, POCustomer).Show();
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
