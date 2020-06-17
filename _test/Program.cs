using DateTimeVisualizer;
using DateTimeVisualizer.Debuggee;
using System;

namespace _test {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            var dte = new DateTime(2001, 1, 1, 1, 1, 1, DateTimeKind.Unspecified);
            var visualizerHost = new Microsoft.VisualStudio.DebuggerVisualizers.VisualizerDevelopmentHost(dte, typeof(Visualizer), typeof(VisualizerObjectSource));
            visualizerHost.ShowVisualizer();
            Console.ReadKey(true);
        }
    }
}
