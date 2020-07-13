using NodaTime;
using OneOf;
using System.Collections.Generic;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;

namespace DateTimeVisualizer {
    public class ZoneNodeVM : TreeNodeVM<OneOf<string, DateTimeZone>> {
        public ZoneNodeVM(string id) {
            Data = id;
        }
        public ZoneNodeVM(string id, IDateTimeZoneProvider provider, HashSet<string> selectionStore) : this(id) {
            Data = provider.GetZoneOrNull(id)!;
            IsSelected = id.In(selectionStore);
            PropertyChanged += (s, e) => {
                if (e.PropertyName != nameof(IsSelected)) { return; }
                selectionStore.AddRemove(IsSelected.Value, id);
            };
        }

        public string Text => Data.Match(s => s, zone => zone.ToString());
        public override string ToString() => Text;
    }
}
