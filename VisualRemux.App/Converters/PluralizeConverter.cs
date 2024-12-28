using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace VisualRemux.App.Converters;

public class PluralizeConverter : IValueConverter
{
    public string? SingularFormat { get; set; }
    public string? PluralFormat { get; set; }
    public string? ZeroFormat { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            return count switch
            {
                0 => ZeroFormat ?? "No items",
                1 => SingularFormat != null ? string.Format(SingularFormat, count) : "1 item",
                _ => PluralFormat != null ? string.Format(PluralFormat, count) : $"{count} items"
            };
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}