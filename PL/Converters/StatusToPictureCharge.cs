using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL.Converters
{
    /// Distribution of the charge button display according to the drone mode.
    class StatusToPictureCharge : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DroneStatuses)value switch
            {
                DroneStatuses.Available => "https://www.pngrepo.com/png/161328/512/battery.png",
                DroneStatuses.Maintenance => "https://cdn0.iconfinder.com/data/icons/ios-edge-line-1/25/Battery-Dead-512.png",
                DroneStatuses.Sendering => "https://deqn8kzyud5pf.cloudfront.net/assets/hamburgerX-blue-21400e6344ec76c3618c6ad6ae3d1f765d30715e49db6178b40fc1da45fded71.png",
                _ => throw new NotImplementedException(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
