﻿using BlApi;
using BO;
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
    /// Interaction logic for Station.xaml 
    /// </summary>
    public partial class Station : Window
    {
        readonly IBL bl = BlFactory.GetBl();
   
        /// <summary>
        /// The window that opens this window.
        /// </summary>
        public StationsView Sender { get; set; }

        /// <summary>
        /// The variable from which the display opens
        /// </summary>
        public PO.Station POStation { get; set; }

        /// <summary>
        ///Contains all the data needed for the display.
        /// </summary>
        public Model Model { get; } = PL.Model.Instance;

        /// <summary>
        /// constructor to window add station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        public Station(StationsView sender)
        {

            this.Sender = sender;
            InitializeComponent();


            WindowStyle = WindowStyle.None;
            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            AddStationGrid.Visibility = Visibility.Visible;
            stationId.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            stationLocationLongitude.Visibility = Visibility.Visible;
            stationLocationLattitude.Visibility = Visibility.Visible;
            freeChargeSlots.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Consructor for station display window.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="POStation">The station intended for display</param>
        public Station(StationsView sender, PO.Station POStation)
        {


            this.Sender = sender;
            this.POStation = POStation;

            InitializeComponent();

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);

            StationInfoDownGrid.Visibility = Visibility.Visible;
            StationIdInfo.Visibility = Visibility.Visible;
            UpdateNameGrid.Visibility = Visibility.Visible;

            StationLocationInfo.Visibility = Visibility.Visible;
            UpdateNumOfChargeGrid.Visibility = Visibility.Visible;

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
            if (e.Handled = !int.TryParse(e.Text, out int d) && e.Text != "" || d < 0)
                MessageBox.Show("Please enter only numbers.");
        }
        

            private void DoubleTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Checks that entered numbers only
            if (e.Handled = !int.TryParse(e.Text, out int d) && e.Text != "" && e.Text != "." || d < 0)
                MessageBox.Show("Please enter only numbers.");
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
        /// Add station button
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stationId.Text != "" && name.Text != "" && stationLocationLongitude.Text != "" && stationLocationLattitude.Text != "" && freeChargeSlots.Text != null)
                {
                    double c = 0, d = 0;

                    //Checks that the number entered can be entered into int32
                    if (e.Handled = !int.TryParse(stationId.Text, out int b) || !double.TryParse(stationLocationLattitude.Text, out c) || !double.TryParse(stationLocationLongitude.Text, out d))
                        MessageBox.Show("Invalid input", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                    else
                    {
                        bl.AddStation(new()
                        {
                            Id = b,
                            Name = name.Text,
                            LocationOfStation = new()
                            {
                                Lattitude = c,
                                Longitude = d
                            },
                            FreeChargeSlots = int.Parse(freeChargeSlots.Text),
                        });

                        Model.UpdateStations();

                        MessageBox.Show("Adding the station was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("There are unfilled fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (ExistsNumberException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }


        }


        /// <summary>
        /// A button pressed opens the option to update the station and changes the button to OK at the touch of a button.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "OK";
            ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).IsReadOnly = false;

            ((Button)sender).Click -= Update_Click;
            ((Button)sender).Click += OK_Click;
        }

        /// <summary>
        /// Confirmation button for updating the station.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).Text != "")
                {
                    bl.UpdateStation(POStation.Id, UpdateName.Text, POStation.FreeChargeSlots);
                    Model.UpdateStations();
                    MessageBox.Show("Updating the element was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                    ((Button)sender).Content = "Update";
                    ((TextBox)VisualTreeHelper.GetChild(VisualTreeHelper.GetParent((Button)sender), 0)).IsReadOnly = true;

                    ((Button)sender).Click -= OK_Click;
                    ((Button)sender).Click += Update_Click;
                }
                else
                    MessageBox.Show("empty field", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TooSmallAmount ex)
            {

                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        /// <summary>
        /// Sets that by double-clicking a drone that charge in the station from the list it will see the data on the DroneCharge.
        /// </summary>
        /// <param name="sender">The element that activates the function</param>
        /// <param name="e"></param>
        private void ChargingDronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItem is DroneCharge droneCharge)
            {
                BO.Drone BODrone = bl.GetDrone(droneCharge.DroneId);
                PO.Drone PODrone = Model.PODrones.Find(dr => dr.Id == BODrone.Id);
                if (PODrone == null)
                    Model.PODrones.Add(PODrone = new PO.Drone().CopyFromBODrone(BODrone));

                new Drone(this, PODrone).Show();
            }
            else
            {
                MessageBox.Show("No element to see ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

