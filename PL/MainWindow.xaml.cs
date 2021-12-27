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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BlFactory.GetBl();
        

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            //  this.Visibility = Visibility.Hidden;
            this.ShowDrones.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            new DronesView(bl,this).ShowDialog();
            
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
