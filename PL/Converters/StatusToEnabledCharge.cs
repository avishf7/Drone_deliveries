using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using IBL.BO;

namespace PL.Converters
{
    class StatusToEnabledCharge : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((DroneStatuses)value)
            {
                case DroneStatuses.Available:
                    return true;
                case DroneStatuses.Maintenance:
                    return true;
                case DroneStatuses.Sendering:
                    return false;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
