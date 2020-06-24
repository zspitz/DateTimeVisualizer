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
            Config = config;

            var localTimeZone = Tzdb.GetSystemDefault();
            if (localTimeZone is { }) {
                Provider = BuiltInProvider.Tzdb;
            } else {
                localTimeZone = Bcl.GetSystemDefault();
                if (localTimeZone is { }) {
                    Provider = BuiltInProvider.Bcl;
                }
            }

            var utc = DateTime.SpecifyKind(source, Utc);
            UtcInstant = InstantPattern.ExtendedIso.Format(Instant.FromDateTimeUtc(utc));

            if (localTimeZone is null) { return; }

            LocalZoneId = localTimeZone.Id;

            var local = LocalDateTime.FromDateTime(DateTime.SpecifyKind(source, Local));
            var (zonedDateTime1, zonedDateTime2) = localTimeZone.MapLocal(local);
            FirstDerivedInstant = zonedDateTime1.ToInstantString();
            LastDerivedInstant = zonedDateTime2.ToInstantString();

        }

        public DateTime Source { get; }
        public string UtcInstant { get; }
        public string? FirstDerivedInstant { get; }
        public string? LastDerivedInstant { get; }
        public string? LocalZoneId { get; }
        public BuiltInProvider? Provider { get; }

        public Config Config { get; }
    }
}
