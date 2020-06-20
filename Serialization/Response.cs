using NodaTime;
using System;
using static NodaTime.DateTimeZoneProviders;
using static System.DateTimeKind;
using NodaTime.Text;

namespace DateTimeVisualizer.Serialization {
    [Serializable]
    public class Response {
        public Response(DateTime source, Config config) {
            Source = source;

            // TODO GetSystemDefault may throw an exception for nonstandard Windows time zones
            // https://nodatime.org/3.0.x/api/NodaTime.IDateTimeZoneProvider.html#NodaTime_IDateTimeZoneProvider_GetSystemDefault
            var localTimeZone = Tzdb.GetSystemDefault();
            LocalTimeZoneId = localTimeZone.Id;

            var utc = DateTime.SpecifyKind(source, Utc);
            UtcInstant = InstantPattern.ExtendedIso.Format(Instant.FromDateTimeUtc(utc));

            var local = LocalDateTime.FromDateTime(DateTime.SpecifyKind(source, Local));
            var (zonedDateTime1, zonedDateTime2) = localTimeZone.MapLocal(local);
            FirstDerivedInstant = zonedDateTime1.ToInstantString();
            LastDerivedInstant = zonedDateTime2.ToInstantString();

            Config = config;
        }

        public DateTime Source { get; }
        public string UtcInstant { get; }
        public string? FirstDerivedInstant { get; }
        public string? LastDerivedInstant { get; }
        public string? LocalTimeZoneId { get; }
        public Config Config { get; }
    }
}
