using NodaTime;
using System;
using System.Globalization;
using ZSpitz.Util.Wpf;
using static System.Windows.DependencyProperty;
using static NodaTime.DateTimeZoneProviders;
using NodaTime.Text;

namespace DateTimeVisualizer {
    public class InstantToStringConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Instant instant;
            switch (value) {
                case Instant instant1:
                    instant = instant1;
                    break;
                case string s:
                    var parseResult = InstantPattern.ExtendedIso.Parse(s);
                    if (!parseResult.Success) { return UnsetValue; }
                    instant = parseResult.Value;
                    break;
                default:
                    return UnsetValue;
            }

            var zone = parameter as DateTimeZone ?? Tzdb.GetSystemDefault();
            var zonedDateTime = new ZonedDateTime(instant, zone);
            return $"{instant.ToString("g", culture)}\n{zonedDateTime.ToString("F", culture)}";
        }
    }
}
