using DateTimeVisualizer.Serialization;
using ZSpitz.Util.Wpf;
using System.Collections.Generic;
using System.Linq;
using ZSpitz.Util;
using static NodaTime.DateTimeZoneProviders;
using System;

namespace DateTimeVisualizer.UI {
    public class ConfigVM : ViewModelBase<Config> {
        public ConfigVM(Config model) : base(model) {
            AvailableBclZones = Bcl.Ids.Select(id => new ZoneNodeVM(id, Bcl, model.BclIds)).ToArray();

            var parents = new Dictionary<string, ZoneNodeVM>();

            (string parentPath, string last) parsePath(string s) {
                var lastIndex = s.LastIndexOf("/");
                if (lastIndex <0 ) { return ("", s); }
                return (
                    s.Substring(0, lastIndex),
                    s.Substring(lastIndex + 1)
                );
            }

            ZoneNodeVM? getParent(string s) {
                var (parentPath, last) = parsePath(s);
                if (parentPath =="") { return null; }
                if (!parents!.TryGetValue(parentPath, out var parent)) {
                    parent = new ZoneNodeVM(parentPath) {
                        Parent = getParent(parentPath)
                    };
                    parents.Add(parentPath, parent);
                }
                return parent;
            }

            foreach (var id in Tzdb.Ids) {
                var node = new ZoneNodeVM(id, Tzdb, model.TzdbIds) {
                    Parent = getParent(id)
                };
            }

            AvailableTzdbZones = parents.Where(kvp => kvp.Value.Parent is null).Values().ToArray();
        }

        public ZoneNodeVM[] AvailableBclZones { get; }
        public ZoneNodeVM[] AvailableTzdbZones { get; }

        private string _textFilter = "";
        public string TextFilter { 
            get => _textFilter; 
            set {
                if (_textFilter == value) { return; }
                NotifyChanged(ref _textFilter, value);

                Action<ZoneNodeVM> action =
                    _textFilter.IsNullOrWhitespace() ?
                        (Action<ZoneNodeVM>)(x => x.ResetFilter()) :
                        x => x.ApplyFilter(data => data.Match(s => s, zone => zone.Id).Contains(_textFilter, StringComparison.OrdinalIgnoreCase));

                AvailableBclZones.ForEach(action);
                AvailableTzdbZones.ForEach(action);
            }
        }
    }
}
