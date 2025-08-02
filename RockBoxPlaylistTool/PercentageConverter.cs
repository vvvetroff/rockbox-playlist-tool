using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RockBoxPlaylistTool
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targettype, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null) {
                var width = Double.Parse(value.ToString());
                var percent = Double.Parse(parameter.ToString());
                return width * percent;
            }
            return value;
        }
        public object ConvertBack(object value, Type targettype, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("blah");
        }
    }
}
