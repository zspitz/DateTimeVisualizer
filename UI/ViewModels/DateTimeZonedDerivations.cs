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
            (provider, ProviderKind) = isTzdb ? (Tzdb, "TZDB") : (Bcl, "BCL");
            var zone = provider.GetZoneOrNull(id);
            if (zone is null) { return; }

            if (utc is { }) {
                DerivedUtc = new ZonedDateTime(utc.Value, zone);
            }
            if (earlyMapping is { }) {
                DerivedEarlyMapping = new ZonedDateTime(earlyMapping.Value, zone);
            }
            if (laterMapping is { }) {
                DerivedLaterMapping = new ZonedDateTime(laterMapping.Value, zone);
            }
        }

        public string ID { get; }
        public string ProviderKind { get; }
        public ZonedDateTime? DerivedUtc { get; }
        public ZonedDateTime? DerivedEarlyMapping { get; }
        public ZonedDateTime? DerivedLaterMapping { get; }
    }
}
