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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        public Drone()
        {
            InitializeComponent();
        }

        private void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled = !(int.TryParse(e.Text, out int d)) && e.Text != "")
                MessageBox.Show("Please enter only numbers.");

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse((sender as TextBox).Text, out DateTime i) && !int.TryParse((sender as TextBox).Text, out int j))
            {
                (sender as TextBox).Text = "";
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
