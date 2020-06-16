using NodaTime;
using System;
using System.Linq;
using static NodaTime.DateTimeZoneProviders;
using static System.DateTimeKind;
using static System.Globalization.CultureInfo;
using ZSpitz.Util;

namespace DateTimeVisualizer.Serialization {
    public class Response {
        public Response(DateTime source, Config config) {
            var localTimeZone = Tzdb.GetSystemDefault();
            LocalTimeZoneId = localTimeZone.Id;

            if (source.Kind.In(Utc, Unspecified)) {
                UtcInstant = Instant.FromDateTimeUtc(source).ToString("g", InvariantCulture);
            }

            ZonedDateTime? zonedDateTime1 = null;
            ZonedDateTime? zonedDateTime2 = null;
            if (source.Kind.In(Local, Unspecified)) {
                var local = LocalDateTime.FromDateTime(source);
                try {
                    zonedDateTime1 = local.InZoneStrictly(localTimeZone);
                } catch (AmbiguousTimeException ambiguousTimeException) {
                    zonedDateTime1 = ambiguousTimeException.EarlierMapping;
                    zonedDateTime2 = ambiguousTimeException.LaterMapping;
                } catch (SkippedTimeException) {
                    // intentionally blank
                }
            }
            LocalInstants = new[] { zonedDateTime1, zonedDateTime2 }
                .Where(x => x is { })
                .Select(x => x!.ToInstantString())
                .ToArray();

            Config = config;
        }

        public string? UtcInstant { get; }
        public string[] LocalInstants { get; }
        public string? LocalTimeZoneId { get; }
        public Config Config { get; }
    }
}
