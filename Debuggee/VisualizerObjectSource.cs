using DateTimeVisualizer.Serialization;
using Periscope.Debuggee;
using System;
using System.IO;

namespace DateTimeVisualizer.Debuggee {
    public class VisualizerObjectSource : VisualizerObjectSourceBase<DateTime, Config> {
        static VisualizerObjectSource() => SubfolderAssemblyResolver.Hook("DateTimeVisualizer");
        public override object GetResponse(DateTime target, Config config) => new Response(target, config);

        // workaround for https://github.com/zspitz/DateTimeVisualizer/issues/7
        public override void GetData(object target, Stream outgoingData) => Serialize(outgoingData, target);
        public override void TransferData(object? target, Stream incomingData, Stream outgoingData) {
            var config = (Config)Deserialize(incomingData);
            incomingData.Seek(0, SeekOrigin.Begin);
            base.TransferData(config.Value, incomingData, outgoingData);
        }
    }
}
