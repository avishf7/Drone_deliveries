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
using IBL;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBl bl;
        Window sender;
        IBL.BO.Drone drone;

        public Drone(IBl bl, Window sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            MainGrid.ShowGridLines = true;
            AddDownGrid.Visibility = Visibility.Visible;
            maxWeight.ItemsSource = Enum.GetValues(typeof(Weight));
            stations.ItemsSource = bl.GetStations();
            

        }

        public Drone(IBl bl, Window sender, int droneId)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;
            this.drone = bl.GetDrone(droneId);

            MainGrid.RowDefinitions[0].Height = new(50, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new(50, GridUnitType.Star);

            DroneInfoDownGrid.Visibility = Visibility.Visible;
            this.Height = 680;
            this.Width = 550;
            this.DataContext = drone;

           
        }

        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "")
                MessageBox.Show("Please enter only numbers.");

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (droneId.Text != "" && model.Text != "" && maxWeight.SelectedItem != null && stations.SelectedItem != null)
                    bl.AddDrone(new()
                    {
                        Id = int.Parse(droneId.Text),
                        Model = model.Text,
                        MaxWeight = (Weight)maxWeight.SelectedItem,
                    }, ((StationToList)stations.SelectedItem).Id);
                else
                    MessageBox.Show("There are unfilled fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NoNumberFoundException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (ExistsNumberException ex) { MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

            ((ListBox)this.sender.FindName("DronesListView")).ItemsSource = bl.GetDrones();

            MessageBox.Show("Adding the drone was completed successfully!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
