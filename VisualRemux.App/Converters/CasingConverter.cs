using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace VisualRemux.App.Converters;

public class CasingConverter : IValueConverter
{
    public enum CasingType
    {
        Upper,
        Lower,
        Title
    }
    
    public CasingType Casing { get; set; } = CasingType.Upper;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string text)
        {
            return value;
        }

        return Casing switch
        {
            CasingType.Upper => text.ToUpperInvariant(),
            CasingType.Lower => text.ToLowerInvariant(),
            CasingType.Title => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text),
            _ => text
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}