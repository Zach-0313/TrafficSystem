using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int count, length, width, cs, ce, cw, exit = 0;
        float ts = 0;
        public SetupScreen()
        {
            InitializeComponent();
        }

        private void Update_Car_Count(object sender, TextChangedEventArgs e)
        {
            //load default values from a saved .txt file
            int.TryParse(Car_Num.Text, out int value);
            count = value;
        }

        private void Update_Highway_Length(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Highway_Length.Text, out int value);
            length = value;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            simulationConfig.VehicleCount = count;
            simulationConfig.HighwayLength = length;
            simulationConfig.HighwayWidth = width;
            simulationConfig.LaneClosureStart = cs;
            simulationConfig.LaneClosureEnd = ce;
            simulationConfig.LaneClosureWidth = cw;
            simulationConfig.VehicleExitIndex = exit;
            simulationConfig.Timestep = ts;
        }

        private void Update_Highway_Width(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Highway_Width.Text, out int value);
            width = value;
        }

        private void Update_LaneClosure_Start(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Lane_Close_Start.Text, out int value);
            cs = value;
        }

        private void Update_LaneClosure_End(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Lane_Close_End.Text, out int value);
            ce = value;
        }

        private void Update_LaneClosure_Width(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Lane_Close_Width.Text, out int value);
            cw = value;
        }
        private void Update_Exit(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Exit_Input.Text, out int value);
            exit = value;
        }

        private void Run_Clicked(object sender, RoutedEventArgs e)
        {
            ts = 0.25f;
            simulationConfig.Timestep = 0.25f;
            foreach (var member in simulationConfig.GetType().GetMembers())
            {
                if (member == null)
                {
                    Console.WriteLine("Error");
                }
            }
            MainWindow mainWindow = new MainWindow(simulationConfig);
            mainWindow.Show();
        }
    }
}
