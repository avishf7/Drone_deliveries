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
    /// Interaction logic for DronesView.xaml
    /// </summary>
    public partial class DronesView : Window
    {
        IBl bl;
        Window sender;

        public DronesView(IBl bl, Window sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeigth.ItemsSource = Enum.GetValues(typeof(Weight));


            DronesListView.ItemsSource = bl.GetDrones();

        }

        private void MaxWeigth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var weight = ((ComboBox)sender).SelectedItem;
            var status = StatusSelector.SelectedItem;

            DronesListView.ItemsSource = bl.GetDrones(dr => (status != null ? dr.DroneStatus == (DroneStatuses)status : true) &&
                                                            (weight != null ? dr.MaxWeight == (Weight)weight : true));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var weight = MaxWeigth.SelectedItem;
            var status = ((ComboBox)sender).SelectedItem;

            DronesListView.ItemsSource = bl.GetDrones(dr => (status != null ? dr.DroneStatus == (DroneStatuses)status : true) &&
                                                            (weight != null ? dr.MaxWeight == (Weight)weight : true));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.sender.IsEnabled = true;
            this.sender.Opacity = 1;
            this.sender.WindowStyle = WindowStyle.ThreeDBorderWindow;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MaxWeigth.Text = "";
            MaxWeigth.SelectedIndex = -1;
            MaxWeigth.SelectedItem = null;

            StatusSelector.Text = "";
            StatusSelector.SelectedIndex = -1;
            StatusSelector.SelectedItem = null;

            DronesListView.ItemsSource = bl.GetDrones();
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl, this).Show();
            this.IsEnabled = false;
            WindowStyle = WindowStyle.None;
            Topmost = false;
            ResizeMode = ResizeMode.NoResize;
        }
    }
}
