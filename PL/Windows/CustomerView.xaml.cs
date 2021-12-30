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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        IBL bl;
        MainWindow sender;

        bool isCloseClick = true;
        /// <summary>
        /// 
        /// </summary>
        public StationView(IBL bl, MainWindow sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            this.NormalView.DataContext = this.sender.Stations;
            this.GroupingView.DataContext = from station in this.sender.Stations
                                            group station by station.SeveralAvailableChargingStations;

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

        }

        /// <summary>
        /// A button that opens a window for adding a new drone.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new Station(bl, this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a skimmer from the list it will see the data on the skimmer.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationsListView.SelectedItem != null)
            {
                BO.Station BOStation = bl.GetStation((StationsListView.SelectedItem as BO.StationToList).Id);
                PO.Station POStation = this.sender.POStations.Find(dr => dr.Id == BOStation.Id);
                if (POStation == null)
                    this.sender.POStations.Add(POStation = new PO.Station().CopyFromBOStation(BOStation));

                new Station(bl, this, POStation).Show();

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
