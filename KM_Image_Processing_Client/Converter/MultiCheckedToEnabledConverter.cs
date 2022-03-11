using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KM_Image_Processing_Client
{   
    /// <summary>
    /// Class <c>MultiCheckedToEnabledConverter</c> enables multibinding.
    /// If any of the values is true the binding target value will change accordingly.
    /// </summary>
    class MultiCheckedToEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null)
            {
                return values.OfType<bool>().Any(b => b);
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }
    }
}
