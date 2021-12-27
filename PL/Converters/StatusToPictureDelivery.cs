using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace PL.Converters
{
    /// Distribution of the delivery button display according to the drone mode.
    class StatusToPictureDelivery : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values[0] is DroneStatuses)
                switch ((DroneStatuses)values[0])
                {

                    case DroneStatuses.Available:
                        return new BitmapImage(new Uri("https://cdn2.iconfinder.com/data/icons/billing-shipping/100/Drone-512.png"));
                    case DroneStatuses.Maintenance:
                        return new BitmapImage(new Uri("https://deqn8kzyud5pf.cloudfront.net/assets/hamburgerX-blue-21400e6344ec76c3618c6ad6ae3d1f765d30715e49db6178b40fc1da45fded71.png"));
                    case DroneStatuses.Sendering:
                        if (values[1] != null)
                            if (((PackageInTransfer)values[1]).IsCollected)
                                return new BitmapImage(new Uri("https://cdn2.iconfinder.com/data/icons/drone-for-commercial-and-industrial-usage-and-appl/468/drone-commercial-11-512.png"));
                            else
                                return new BitmapImage(new Uri("https://cdn2.iconfinder.com/data/icons/drone-for-commercial-and-industrial-usage-and-appl/416/drone-commercial-10-512.png"));
                        break;
                }


            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
