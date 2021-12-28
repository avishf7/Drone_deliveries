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

        public Station()
        {
            InitializeComponent();
        }

        public Station(IBL bl, StationsView sender)
        {
            this.bl = bl;
            this.sender = sender;
        }

        public Station(IBL bl, StationsView stationsview, PO.Station pOStation) : this(bl, stationsview)
        {
            this.POStation = pOStation;
        }
    }
}
