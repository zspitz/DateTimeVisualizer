using NodaTime;
using System.Diagnostics.CodeAnalysis;
using static System.Globalization.CultureInfo;

#if NET45
using System.Linq;
#endif

namespace DateTimeVisualizer.Serialization {
    internal static class Extensions {
        internal static string ToInstantString(this ZonedDateTime x) => x.ToInstant().ToString("g", InvariantCulture);

        [return: NotNullIfNotNull("x")]
        internal static string? ToInstantString(this ZonedDateTime? x) => x?.ToInstantString();
    }
}

#if NET45
namespace ZSpitz.Util {
    internal static class Extensions {
        internal static bool In<T>(this T val, params T[] vals) => vals.Contains(val);
    }
}
#endif
