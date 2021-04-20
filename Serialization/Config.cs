using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;
#if VISUALIZER_DEBUGGEE
using Periscope.Debuggee;
#endif

namespace DateTimeVisualizer.Serialization {
    [Serializable]
#if VISUALIZER_DEBUGGEE
    public class Config : ConfigBase<Config> {
        public override Config Clone() => new Config(BclIds, TzdbIds);
        public override ConfigDiffStates Diff(Config baseline) =>
            BclIds.SetEquals(baseline.BclIds) && TzdbIds.SetEquals(baseline.TzdbIds) ? 
                ConfigDiffStates.NoAction : 
                ConfigDiffStates.NeedsWrite;
        public DateTime? Value { get; set; } // workaround for https://github.com/zspitz/DateTimeVisualizer/issues/7
#else
    public class Config {
#endif
        public HashSet<string> BclIds { get; }
        public HashSet<string> TzdbIds { get; }
        public Config(IEnumerable<string>? bclIds = null, IEnumerable<string>? tzdbIds = null) {
            BclIds = new HashSet<string>(bclIds ?? Empty<string>());
            TzdbIds = new HashSet<string>(tzdbIds ?? Empty<string>());
        }
    }
}
