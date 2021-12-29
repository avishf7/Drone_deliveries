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
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {

        IBL bl;
        StationsView sender;
        PO.Station station;
        private PO.Station POStation;


        public Station(IBL bl, StationsView sender)
        {
            InitializeComponent();
            this.bl = bl;
            this.sender = sender;
        }

        public Station(IBL bl, StationsView sender, PO.Station POStation) 
        {
            InitializeComponent();
            this.POStation = POStation;
        }
    }
}
