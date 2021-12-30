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
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackageView : Window
    {
        IBL bl;
        MainWindow sender;

        bool isCloseClick = true;




        /// <summary>
        /// constructor to create window of drones view.
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="sender">The element that activates the function</param>
        public DronesView(IBL bl, MainWindow sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeigth.ItemsSource = Enum.GetValues(typeof(Weight));
            this.NormalView.DataContext = this.sender.Drones;
            this.GroupingView.DataContext = from drone in this.sender.Drones
                                            group drone by drone.DroneStatus;

            this.sender.Drones.CollectionChanged += Drones_CollectionChanged;
            this.sender.Closing += Sender_Closing;
            this.sender.Activated += Sender_Activated;
            this.sender.Deactivated += Sender_Deactivated;
        }

        private void Drones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.GroupingView.DataContext = from drone in this.sender.Drones
                                            group drone by drone.DroneStatus;
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
        /// Filter the display by weight in the combobox.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void MaxWeigth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtering();
        }

        /// <summary>
        /// Filter the display by status in the combobox
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtering();
        }

        /// <summary>
        /// Closing the window
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ((Button)this.sender.FindName("ShowDrones")).IsEnabled = true;
        }

        /// <summary>
        /// A button that resets the filters.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MaxWeigth.Text = "";
            MaxWeigth.SelectedIndex = -1;
            MaxWeigth.SelectedItem = null;

            StatusSelector.Text = "";
            StatusSelector.SelectedIndex = -1;
            StatusSelector.SelectedItem = null;

            Filtering();
        }

        /// <summary>
        /// A button that opens a window for adding a new drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl, this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a skimmer from the list it will see the data on the skimmer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem != null)
            {
                BO.Drone BODrone = bl.GetDrone((DronesListView.SelectedItem as BO.DroneToList).Id);
                PO.Drone PODrone = this.sender.PODrones.Find(dr => dr.Id == BODrone.Id);
                if (PODrone == null)
                    this.sender.PODrones.Add(PODrone = new PO.Drone().CopyFromBODrone(BODrone));

                new Drone(bl, this, PODrone).Show();

            }
        }

        /// <summary>
        /// Filter the display according to the combo boxs selection.
        /// </summary>
        public void Filtering()
        {
            var weight = MaxWeigth.SelectedItem;
            var status = StatusSelector.SelectedItem;

            this.sender.Drones.Clear();
            foreach (var drone in bl.GetDrones(dr => (status != null ? dr.DroneStatus == (DroneStatuses)status : true) &&
                                                     (weight != null ? dr.MaxWeight == (Weight)weight : true)))
                this.sender.Drones.Add(drone);
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
