using DateTimeVisualizer.Serialization;
using DateTimeVisualizer.ViewModels;
using ZSpitz.Util.Wpf;
using NodaTime;

namespace DateTimeVisualizer.UI {
    public class ConfigVM : ViewModelBase<Config> {
        public ConfigVM(Config model) : base(model) {
            Tzdb = new ZoneProviderVM(DateTimeZoneProviders.Tzdb, model.TzdbIds);
            Bcl = new ZoneProviderVM(DateTimeZoneProviders.Bcl, model.BclIds);
        }

        public ZoneProviderVM Tzdb { get; }
        public ZoneProviderVM Bcl { get; }
    }
}
