using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Programs_Starter.WPFClient.Converters
{
    public class StringColorToSolidBrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush BrushColor = (SolidColorBrush)new BrushConverter().ConvertFromString("White");
            if (value != null)
                BrushColor = (SolidColorBrush)new BrushConverter().ConvertFromString((string)value);
            return BrushColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
