using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using PO;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackagesView : Window
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
        /// constructor to create window of packages view.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public PackagesView(MainWindow sender)
        {
            InitializeComponent();

            this.sender = sender;

            Model.UpdatePackages();

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
            Model.PackageStatusFilter = null;
            ((Button)this.sender.FindName("ShowPackages")).IsEnabled = true;
        }

        /// <summary>
        /// Filter the display by status in the combobox
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.PackageStatusFilter = (PackageStatus?)StatusSelector.SelectedItem;
            Model.UpdatePackages();
        }


        /// <summary>
        /// A button that resets the filters.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedIndex = -1;
            StatusSelector.SelectedItem = null;
            StatusSelector.Text = "";

            StatusSelector.SelectedIndex = -1;
            StatusSelector.SelectedItem = null;
            StatusSelector.Text = "";           
        }

       
        /// <summary>
        /// A button that opens a window for adding a new package.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            new Package(this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a package from the list it will see the data on the package.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void PackagesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                BO.Package BOPackage = bl.GetPackage((((ListView)sender).SelectedItem as BO.PackageToList).Id);
                PO.Package POPackage = Model.POPackages.Find(dr => dr.Id == BOPackage.Id);
                if (POPackage == null)
                    Model.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

                new Package(this, POPackage).Show();

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

