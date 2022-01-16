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

            if (values[0] is DroneStatuses statuses)
                switch (statuses)
                {

                    case DroneStatuses.Available:
                        return new BitmapImage(new Uri( @"Pictures\Assignment.png", UriKind.Relative));
                    case DroneStatuses.Maintenance:
                        return new BitmapImage(new Uri(@"Pictures\X.png", UriKind.Relative));
                    case DroneStatuses.Sendering:
                        if (values[1] != null)
                            if (((PackageInTransfer)values[1]).IsCollected)
                                return new BitmapImage(new Uri(@"Pictures\Supply.png", UriKind.Relative));
                            else
                                return new BitmapImage(new Uri(@"Pictures\Pick.png", UriKind.Relative));
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
