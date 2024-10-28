using System.Windows;
using System.Windows.Controls;

namespace TrafficSystem
{
    public partial class SetupScreen : Page
    {
        SimulationConfig simulationConfig = new SimulationConfig();
        int count, length, width, cs, ce, cw, inc;
        int[] exit = [0];
        float ts = 0;
        string path;
        public SetupScreen()
        {
            InitializeComponent();
            count = 20;
            length = 50;
            width = 4;
            cs = 20;
            ce = 30;
            cw = 3;
            exit = [45];
            ts = .125f;
            path = "NONE";
            inc = 1;
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
            foreach (int exitIndex in exit)
            {
                if (cs < exitIndex && (ce + width) > exitIndex)
                {
                    Error_Popup.Visibility = Visibility.Visible;
                }
                else
                {
                    Error_Popup.Visibility = Visibility.Hidden;
                }
            }
            simulationConfig.VehicleCount = count;
            simulationConfig.HighwayLength = length;
            simulationConfig.HighwayWidth = width;
            simulationConfig.LaneClosureStart = cs;
            simulationConfig.LaneClosureEnd = ce;
            simulationConfig.LaneClosureWidth = cw;
            simulationConfig.VehicleExitIndex = exit;
            simulationConfig.Timestep = ts;
            simulationConfig.IncomingVehiclePattern = inc;
            simulationConfig.FileLocation = path;
        }

        private void Update_Highway_Width(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Highway_Width.Text, out int value);
            width = value;
        }

        private void OnFilepath(object sender, TextChangedEventArgs e)
        {
            path = File_Input.Text;
        }

        private void Update_Incoming(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Incoming_Input.Text, out int value);
            inc = value;
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
            string[] exitStrings = Exit_Input.Text.Split(",");
            exit = new int[exitStrings.Length];
            for(int i = 0; i < exitStrings.Length; i++) {
                int.TryParse(exitStrings[i], out int value);
                exit[i] = value;
            }
        }

        private void Run_Clicked(object sender, RoutedEventArgs e)
        {
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
