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
    /// Interaction logic for Stationsview.xaml
    /// </summary>
    public partial class StationsView : Window
    {
        IBL bl;
        MainWindow sender;
        bool isCloseClick = true;


        /// <summary>
        /// constructor to create window of stations view.
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="sender">The element that activates the function</param>
        public StationsView(IBL bl, MainWindow sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            DataContext = this.sender.Stations;

        }

        /// <summary>
        /// Closing the window
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ((Button)this.sender.FindName("ShowStations")).Visibility = Visibility.Visible;
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
        /// A button that opens a window for adding a new station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new Station(bl, this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a skimmer from the list it will see the data on the station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationsListView.SelectedItem != null)
            {
                BO.Station BOStation = bl.GetStation((StationsListView.SelectedItem as BO.StationToList).Id);
                PO.Station POStation = this.sender.POStations.Find(st => st.Id == BOStation.Id);
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
    }}
