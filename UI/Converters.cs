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

    public class DateTimeOnlyConverter : ReadOnlyConverterBase {
        // TODO Should we recreate the pattern for each call to Convert? There might be a different culture passed in each time
        private static ZonedDateTimePattern pattern = ZonedDateTimePattern.CreateWithCurrentCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFF '('o<g>')'", null);
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null) { return UnsetValue; }
            if (!(value is ZonedDateTime zdt)) { throw new NotImplementedException(); }
            return pattern.Format(zdt);
        }
    }
}
