using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi.BO;



namespace PL
{
    /// <summary>
    /// Interaction logic for DronesView.xaml
    /// </summary>
    public partial class DronesView : Window
    {
        IBL bl;
        Window sender;
        bool isCloseClick = true;
        internal ObservableCollection<DroneToList> Drones { get; set; }

        

        /// <summary>
        /// constructor to create window of drones view.
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="sender">The element that activates the function</param>
        public DronesView(IBL bl, Window sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;

            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            MaxWeigth.ItemsSource = Enum.GetValues(typeof(Weight));

            
            Drones = new ObservableCollection<DroneToList>(bl.GetDrones());
            DataContext = Drones;

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
            ((Button)this.sender.FindName("ShowDrones")).Visibility = Visibility.Visible;
            this.sender.WindowStyle = WindowStyle.ThreeDBorderWindow;
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

            Drones = new ObservableCollection<DroneToList>(bl.GetDrones());
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
                new Drone(bl, this, (DronesListView.SelectedItem as BlApi.BO.DroneToList).Id).ShowDialog();
        }

        /// <summary>
        /// Filter the display according to the combo boxs selection.
        /// </summary>
        public void Filtering()
        {
            var weight = MaxWeigth.SelectedItem;
            var status = StatusSelector.SelectedItem;

            Drones.Clear();
            foreach (var drone in bl.GetDrones(dr => (status != null ? dr.DroneStatus == (DroneStatuses)status : true) &&
                                                     (weight != null ? dr.MaxWeight == (Weight)weight : true)))
                Drones.Add(drone);
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
