using NodaTime;
using NodaTime.Text;
using NodaTime.TimeZones;

#if NET45
using System.Linq;
#endif

namespace DateTimeVisualizer.Serialization {
    public static class Extensions {
        public static void Deconstruct(this ZoneLocalMapping mapping, out ZonedDateTime? first, out ZonedDateTime? last) {
            first = mapping.Count > 0 ? mapping.First() : (ZonedDateTime?)null;
            last = mapping.Count == 2 ? mapping.Last() : (ZonedDateTime?)null;
        }

        public static Instant? ParseOrNull(this InstantPattern pattern, string? s) {
            if (s is null) { return null; }
            var parseResult = pattern.Parse(s);
            if (!parseResult.Success) { return null; }
            return parseResult.Value;
        }

        public static string? ToInstantString(this ZonedDateTime? zonedDateTime) {
            if (zonedDateTime is null) { return null; }
            return InstantPattern.ExtendedIso.Format(zonedDateTime.Value.ToInstant());
        }
    }
}

#if NET45
namespace ZSpitz.Util {
    internal static class Extensions {
        internal static bool In<T>(this T val, params T[] vals) => vals.Contains(val);
    }
}
#endif
