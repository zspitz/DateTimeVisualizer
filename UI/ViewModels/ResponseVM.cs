using DateTimeVisualizer.Serialization;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using ZSpitz.Util.Wpf;
using static NodaTime.DateTimeZoneProviders;

namespace DateTimeVisualizer.UI {
    public class ResponseVM : ViewModelBase<Response> {
        public ResponseVM(Response model, Config config) : base(model) {
            Local = LocalDateTime.FromDateTime(model.Source);
            if (model.LocalTimeZoneId is { }) {
                LocalZone = Tzdb.GetZoneOrNull(model.LocalTimeZoneId);
            }

            Derivations =
                config.TzdbIds.Select(x => (id: x, isTzdb: true))
                .Concat(
                    config.BclIds.Select(x => (id: x, isTzdb: false))
                )
                .OrderBy(x => x.id)
                .Select(x => new DateTimeZonedDerivations(x.id, x.isTzdb, model.UtcInstant, model.FirstDerivedInstant, model.LastDerivedInstant))
                .ToList();
        }

        
        public LocalDateTime Local { get; }
        public DateTimeZone? LocalZone { get; }
        public List<DateTimeZonedDerivations> Derivations { get; }
        
    }
}
