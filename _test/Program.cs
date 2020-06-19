using DateTimeVisualizer;
using DateTimeVisualizer.Debuggee;
using System;

namespace _test {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            var dte = new DateTime(2019, 10, 27, 1, 30, 0, DateTimeKind.Unspecified);
            var visualizerHost = new Microsoft.VisualStudio.DebuggerVisualizers.VisualizerDevelopmentHost(dte, typeof(Visualizer), typeof(VisualizerObjectSource));
            visualizerHost.ShowVisualizer();
        }
    }
}
