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
                DroneStatuses.Available => @"Pictures\Charge.png",
                DroneStatuses.Maintenance => @"Pictures\Uncharge.png",
                DroneStatuses.Sendering => @"Pictures\X.png",
                _ => throw new NotImplementedException(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
