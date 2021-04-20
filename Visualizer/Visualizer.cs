using Periscope;
using Periscope.Debuggee;
using System;
using DateTimeVisualizer.Serialization;
using System.Diagnostics;
using Microsoft.VisualStudio.DebuggerVisualizers;
using ZSpitz.Util.Wpf;

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

        // because we're using GetData to get back the debugged value (workaround for https://github.com/zspitz/DateTimeVisualizer/issues/7)
        // instead of the config key, we have to override the entire method
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            var config = Persistence.Get<Config>();
            config.Value = (DateTime)objectProvider.GetObject();
            var window = new VisualizerWindow();
            window.Initialize(objectProvider, config);
            if (window.IsOpen) {
                window.ShowDialog();
            }
        }
    }
}
