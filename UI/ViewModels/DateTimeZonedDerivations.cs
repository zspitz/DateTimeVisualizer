using DateTimeVisualizer.Serialization;
using NodaTime;
using NodaTime.Text;
using static NodaTime.DateTimeZoneProviders;
using static NodaTime.Text.InstantPattern;

namespace DateTimeVisualizer.UI {
    public class DateTimeZonedDerivations {
        public DateTimeZonedDerivations(string id, bool isTzdb, string? utc, string? earlyMapping, string? laterMapping)
            : this(
                  id, 
                  isTzdb, 
                  ExtendedIso.ParseOrNull(utc),
                  ExtendedIso.ParseOrNull(earlyMapping),
                  ExtendedIso.ParseOrNull(laterMapping)
            ) { }

        public DateTimeZonedDerivations(string id, bool isTzdb, Instant? utc, Instant? earlyMapping, Instant? laterMapping) {
            ID = id;
            
            IDateTimeZoneProvider provider;
            (provider, Provider) = isTzdb ? (Tzdb, BuiltInProvider.Tzdb) : (Bcl, BuiltInProvider.Bcl);
            Zone = provider.GetZoneOrNull(id);
            if (Zone is null) { return; }

            if (utc is { }) {
                DerivedUtc = new ZonedDateTime(utc.Value, Zone);
            }
            if (earlyMapping is { }) {
                DerivedEarlyMapping = new ZonedDateTime(earlyMapping.Value, Zone);
            }
            if (laterMapping is { }) {
                DerivedLaterMapping = new ZonedDateTime(laterMapping.Value, Zone);
            }
        }

        public string ID { get; }
        public DateTimeZone? Zone { get; }
        public BuiltInProvider Provider { get; }
        public ZonedDateTime? DerivedUtc { get; }
        public ZonedDateTime? DerivedEarlyMapping { get; }
        public ZonedDateTime? DerivedLaterMapping { get; }
        public LocalDateTime? DerivedUtcSorter => DerivedUtc?.LocalDateTime;
        public LocalDateTime? DerivedEarlyMappingSorter => DerivedEarlyMapping?.LocalDateTime;
        public LocalDateTime? DerivedLaterMappingSorter => DerivedLaterMapping?.LocalDateTime;
    }
}
