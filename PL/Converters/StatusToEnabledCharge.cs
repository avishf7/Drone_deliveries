using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BO;

namespace PL.Converters
{
    /// Defines whether the charge button can be pressed according to the status
    class StatusToEnabledCharge : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DroneStatuses)value switch
            {
                DroneStatuses.Available => true,
                DroneStatuses.Maintenance => true,
                DroneStatuses.Sendering => false,
                _ => throw new NotImplementedException(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
