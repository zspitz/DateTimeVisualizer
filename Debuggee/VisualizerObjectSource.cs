using DateTimeVisualizer.Serialization;
using Periscope.Debuggee;
using System;

namespace DateTimeVisualizer.Debuggee {
    public class VisualizerObjectSource : VisualizerObjectSourceBase<DateTime, Config> {
        static VisualizerObjectSource() => SubfolderAssemblyResolver.Hook("DateTimeVisualizer");
        public override object GetResponse(DateTime target, Config config) => new Response(target, config);
    }
}
