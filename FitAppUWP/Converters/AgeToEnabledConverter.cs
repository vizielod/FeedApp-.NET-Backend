using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FitAppUWP.Converters
{
    public class AgeToEnabledConverter : IValueConverter
    {
        public  object Convert(object value, Type targetType, object parameter, string language)
        {
            var age = (int)value;
            return age > 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
