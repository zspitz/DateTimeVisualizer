using Periscope;
using Periscope.Debuggee;
using System;
using DateTimeVisualizer.Serialization;
using System.Diagnostics;

[assembly: DebuggerVisualizer(
    visualizer: typeof(DateTimeVisualizer.Visualizer),
    visualizerObjectSource: typeof(DateTimeVisualizer.Debuggee.VisualizerObjectSource),
    Target = typeof(DateTime),
    Description = "DateTime Visualizer")]

namespace DateTimeVisualizer {
    public abstract class VisualizerWindowBase : VisualizerWindowBase<VisualizerWindow, Config> { }
    public class Visualizer : VisualizerBase<VisualizerWindow, Config> {
        static Visualizer() => SubfolderAssemblyResolver.Hook("DateTimeVisualizer");
        public Visualizer() : base(new GithubProjectInfo("zspitz", "datetimevisualizer")) { }
    }
}
