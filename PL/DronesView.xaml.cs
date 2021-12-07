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
        public DronesView(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeigth.ItemsSource = Enum.GetValues(typeof(Weight)); 


          DronesListView.ItemsSource = bl.GetDrones();

        }

        private void MaxWeigth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetDrones(dr => dr.MaxWeight == (Weight)((ComboBox)sender).SelectedItem);

        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetDrones(dr => dr.DroneStatus == (DroneStatuses)((ComboBox)sender).SelectedItem);
        }
    }
}
