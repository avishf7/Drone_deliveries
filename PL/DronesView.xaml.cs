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
            Filtering();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtering();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((Button)this.sender.FindName("ShowDrones")).Visibility = Visibility.Visible;
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
            new Drone(bl, this).ShowDialog();

        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem != null)
                new Drone(bl, this, (DronesListView.SelectedItem as IBL.BO.DroneToList).Id).ShowDialog();
        }

        public void Filtering()
        {
            var weight = MaxWeigth.SelectedItem;
            var status = StatusSelector.SelectedItem;

            DronesListView.ItemsSource = bl.GetDrones(dr => (status != null ? dr.DroneStatus == (DroneStatuses)status : true) &&
                                                           (weight != null ? dr.MaxWeight == (Weight)weight : true));
        }
    }
}
