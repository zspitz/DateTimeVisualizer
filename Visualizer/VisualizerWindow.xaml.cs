using DateTimeVisualizer.Serialization;
using DateTimeVisualizer.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DateTimeVisualizer {
    /// <summary>
    /// Interaction logic for VisualizerWindow.xaml
    /// </summary>
    public partial class VisualizerWindow : VisualizerWindowBase {
        public VisualizerWindow() {
            InitializeComponent();
        }

        protected override (object windowContext, object optionsContext, Config config) GetViewState(object r, ICommand? OpenInNewWindow) {
            var response = (Response)r;
            return (
                new ResponseVM(response, response.Config),
                new ConfigVM(response.Config),
                response.Config
            );
        }

        protected override void TransformConfig(Config config, object parameter) => throw new NotImplementedException();
    }
}
