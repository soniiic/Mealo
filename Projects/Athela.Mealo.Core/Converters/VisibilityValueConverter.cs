using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.UI;

namespace Athela.Mealo.Core.Converters
{
    public class VisibilityValueConverter : MvxValueConverter<bool, MvxVisibility>
    {
        protected override MvxVisibility Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? MvxVisibility.Visible : MvxVisibility.Collapsed;
        }
    }
}