﻿using BlApi;
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


namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for StationView.xaml
    /// </summary>
    public partial class StationsView : Window
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
        /// constructor to create window of stations view.
        /// </summary>
        public StationsView(MainWindow sender)
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
            ((Button)this.sender.FindName("ShowStations")).IsEnabled = true;
        }



        /// <summary>
        /// A button that opens a window for adding a new station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new Station(this).ShowDialog();
        }

        /// <summary>
        /// Sets that by double-clicking a station from the list it will see the data on the station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                BO.Station BOStation = bl.GetStation((((ListView)sender).SelectedItem as BO.StationToList).Id);
                PO.Station POStation = Model.POStations.Find(st => st.Id == BOStation.Id);
                if (POStation == null)
                    Model.POStations.Add(POStation = new PO.Station().CopyFromBOStation(BOStation));

                new Station(this, POStation.CopyFromBOStation(BOStation)).Show();

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
