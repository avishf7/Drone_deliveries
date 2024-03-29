﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Windows.Data;
using System.Globalization;

namespace PL.Converters
{
    /// Defines whether the delivery button can be pressed according to the status.
    class StatusToEnableDelivery : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DroneStatuses)value switch
            {
                DroneStatuses.Available => true,
                DroneStatuses.Maintenance => false,
                DroneStatuses.Sendering => true,
                _ => throw new NotImplementedException(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
