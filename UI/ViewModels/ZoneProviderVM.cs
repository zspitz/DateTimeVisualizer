using NodaTime;
using System.Collections.Generic;
using System.Linq;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;

namespace DateTimeVisualizer.ViewModels {
    public class ZoneProviderVM : ViewModelBase<IDateTimeZoneProvider> {
        public ZoneProviderVM(IDateTimeZoneProvider model, HashSet<string> currentlySelected) : base(model) {
            AvailableZones = model.Ids.Select(id => {
                var vm = new ZoneVM(id) {
                    IsSelected = id.In(currentlySelected)
                };
                vm.PropertyChanged += (s, e) => {
                    if (e.PropertyName != nameof(vm.IsSelected)) { return; }
                    currentlySelected.AddRemove(vm.IsSelected, id);
                };
                return vm;
            }).ToArray();
        }

        public ZoneVM[] AvailableZones { get; }
    }
}
