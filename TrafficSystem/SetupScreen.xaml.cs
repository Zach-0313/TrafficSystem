using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrafficSystem
{
    /// <summary>
    /// Interaction logic for SetupScreen.xaml
    /// </summary>
    public partial class SetupScreen : Page
    {
            SimulationConfig simulationConfig = new SimulationConfig();
        public SetupScreen()
        {
            InitializeComponent();
            
        }

        private void Update_Car_Count(object sender, TextChangedEventArgs e)
        {
            simulationConfig.VehicleCount = int.Parse(Car_Num.Text);
        }

        private void Update_Highway_Length(object sender, TextChangedEventArgs e)
        {
            simulationConfig.HighwayLength = int.Parse(Highway_Length.Text);

        }

        private void Update_Highway_Width(object sender, TextChangedEventArgs e)
        {
            simulationConfig.HighwayWidth = int.Parse(Highway_Width.Text);

        }

        private void Update_LaneClosure_Start(object sender, TextChangedEventArgs e)
        {
            simulationConfig.LaneClosureStart = int.Parse(Lane_Close_Start.Text);

        }

        private void Update_LaneClosure_End(object sender, TextChangedEventArgs e)
        {
            simulationConfig.LaneClosureEnd = int.Parse(Lane_Close_End.Text);

        }

        private void Update_LaneClosure_Width(object sender, TextChangedEventArgs e)
        {
            simulationConfig.LaneClosureWidth = int.Parse(Lane_Close_Width.Text);
        }
        private void Update_Exit(object sender, TextChangedEventArgs e)
        {
            simulationConfig.VehicleExitIndex = int.Parse(Exit_Input.Text);

        }

        private void Run_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
