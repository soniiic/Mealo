using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MvvmCross.Platform.Converters;

namespace Athela.Mealo.Core.Converters
{
    public class TimeToMinutesValueConverter : MvxValueConverter<TimeSpan?, string>
    {
        public TimeToMinutesValueConverter()
        {
        }

        protected override TimeSpan? ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (value.All(char.IsNumber))
            {
                return TimeSpan.FromMinutes(double.Parse(value));
            }

            if (Regex.IsMatch(value, @"^\d{1,2}\:\d{2}$"))
            {
                var splitValue = value.Split(':');
                return new TimeSpan(0, int.Parse(splitValue[0]), int.Parse(splitValue[1]), 0);
            }

            return null;
        }

        protected override string Convert(TimeSpan? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return $"{value.Value.Hours}:{value.Value.Minutes}";

        }
    }
}
