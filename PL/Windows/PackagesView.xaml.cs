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
using PO;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackagesView : Window
    {
        IBL bl = BlFactory.GetBl();
        MainWindow sender;

        bool isCloseClick = true;
        public Model Model { get; } = PL.Model.Instance;



        /// <summary>
        /// constructor to create window of drones view.
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="sender">The element that activates the function</param>
        public PackagesView( MainWindow sender)
        {
            InitializeComponent();

            this.sender = sender;


            this.GroupingView.DataContext = from package in bl.GetPackages()
                                            group package by package.SenderName;


            Model.Packages.CollectionChanged += Packages_CollectionChanged;
            this.sender.Closing += Sender_Closing;
            this.sender.Activated += Sender_Activated;
            this.sender.Deactivated += Sender_Deactivated;

        }

        private void Packages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.GroupingView.DataContext = from package in bl.GetPackages()
                                            group package by package.SenderName;
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
            ((Button)this.sender.FindName("ShowPackages")).IsEnabled = true;
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
        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            new Package(bl, this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a skimmer from the list it will see the data on the skimmer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void PackagesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (((ListView)sender).SelectedItem != null)
            //{
            //    BO.Package BOPackage = bl.GetPackage((((ListView)sender).SelectedItem as BO.PackageToList).Id);
            //    PO.Package POPackage = this.sender.POPackages.Find(dr => dr.Id == BOPackage.Id);
            //    if (POPackage == null)
            //        this.sender.POPackages.Add(POPackage = new PO.Package().CopyFromBOPackage(BOPackage));

            //    new Package(bl, this, POPackage).Show();

            //}
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

