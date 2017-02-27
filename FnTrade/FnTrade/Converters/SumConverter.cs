using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using static Core.Entities;

namespace FnTrade.Converters
{
    public class SumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var investments = value as IEnumerable<SharesInfo>;
            return investments.Sum(x => x.PricePerShare * x.Shares.Quantity);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}