using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;

namespace Reports.Utility
{
    public class StringToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var str = value as string;

            return !string.IsNullOrEmpty(str) ? new Bitmap(str) : new Bitmap(AssetLoader.Open(new Uri("avares://Reports/Assets/Images/user.png")));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
