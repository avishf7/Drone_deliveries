using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using System.Windows.Data;
using System.Globalization;

namespace PL.Converters
{
    class StatusToEnableDelivery : IValueConverter
    {
        /// <summary>
        /// Defines whether the button can be pressed according to the status.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((DroneStatuses)value)
            {
                case DroneStatuses.Available:
                    return true;
                case DroneStatuses.Maintenance:
                    return false;
                case DroneStatuses.Sendering:
                    return true;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
