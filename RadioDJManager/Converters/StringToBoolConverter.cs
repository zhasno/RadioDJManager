using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RadioDJManager.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var save = value as bool?;
            if(save ==null)
                return null;
            //return System.Convert.ToBoolean(save);
            return save.Value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var saveString = value as string;
            if(saveString == null)
                return null;
            return System.Convert.ToBoolean(saveString);
        }
    }
}
