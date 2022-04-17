using Microsoft.UI.Xaml.Data;
using System;

namespace AirCombatAuswertung.Helpers
{
    public class FloatToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((float)value == 0f)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string strvalue = value as string;
                if (strvalue.Contains(".")) strvalue = strvalue.Replace(".", sep);
                else if (strvalue.Contains(",")) strvalue = strvalue.Replace(",", sep);
                if (strvalue == string.Empty)
                {
                    return 0f;
                }
                return float.Parse(strvalue);
            }
            catch (Exception)
            {
                return 0f;
            }
        }
    }
}
