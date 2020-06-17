using DateTimeVisualizer.Serialization;
using Periscope.Debuggee;
using System;

namespace DateTimeVisualizer.Debuggee {
    public class VisualizerObjectSource : VisualizerObjectSourceBase<DateTime, Config> {
        public override object GetResponse(DateTime target, Config config) => new Response(target, config);
    }
}
