using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfAppDatagridGroupingHeader
{
    public class UnitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             
            var m = double.Parse(parameter.ToString());

            switch (value)
            {
                case  string s:
                    if (double.TryParse(s, out var res))
                    {
                        return  res * m;
                    }
                    else
                    {
                        return value;
                    }
                case double d:
                    return d * m;
                default:
                    return value;
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var m = double.Parse(parameter.ToString());

            switch (value)
            {
                case string s:
                    if (double.TryParse(s, out var res))
                    {
                        return res / m;
                    }
                    else
                    {
                        return value;
                    }
                case double d:
                    return d / m;
                default:
                    return value;
            } 
        }
    }
}
