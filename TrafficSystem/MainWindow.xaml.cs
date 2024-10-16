using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrafficSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Rectangle[,] rectangles;
        Highway highway;
        private int _timeSteps = 0;

        SimulationConfig simulationConfig = new SimulationConfig
        {
            HighwayWidth = 5,
            HighwayLength = 50,
            LaneClosureStart = 5,
            LaneClosureEnd = 20,
            LaneClosureWidth = 3,
            VehicleCount = 20,
            VehicleExitIndex = [25],
            Timestep = 0.5f,
            IncomingVehiclePattern = 1
        };

        public MainWindow(SimulationConfig config)
        {
            InitializeComponent();
            Simulation simulation = new Simulation(config);

            simulationConfig = config;
            highway = Simulation.Instance._highway;
            rectangles = new Rectangle[simulationConfig.HighwayWidth, simulationConfig.HighwayLength];

            DrawHighway(MyCanvas);

            Vehicle.UpdateVehicleDisplay += UpdateSingleRectangles;
            Simulation.Timer_Tick += UpdateLiveUI;
        }

        private void UpdateLiveUI(int arrivals)
        {
            _timeSteps++;
            TimeStepCounter.Text = _timeSteps.ToString();
            VehicleArrivalCounter.Text = arrivals.ToString();
            VehiclesInProgressCounter.Text = (simulationConfig.VehicleCount - arrivals).ToString();
        }
        public void DrawHighway(Canvas canvas)
        {
            int nWidth = (int)Application.Current.MainWindow.ActualWidth;

            int height = (int)(700 / simulationConfig.HighwayLength);
            int space = 0;


            for (int x = 0; x < highway.x_size; x++)
            {
                for (int y = 0; y < highway.y_size; y++)
                {
                    rectangles[x, y] = new Rectangle
                    {
                        Height = height,
                        Width = height,
                    };
                    switch (highway.lanePositions[x, y].thisState)
                    {
                        case LanePosition.LaneState.empty:
                            rectangles[x, y].Fill = Brushes.Gray;
                            break;
                        case LanePosition.LaneState.closed:
                            rectangles[x, y].Fill = Brushes.Red;
                            break;
                        case LanePosition.LaneState.occupied:
                            rectangles[x, y].Fill = Brushes.Green;
                            break;
                    }
                    if (simulationConfig.VehicleExitIndex.Contains(y))
                    {
                        rectangles[x, y].Fill = Brushes.Orange;
                    }
                    canvas.Children.Add(rectangles[x, y]);
                    Canvas.SetLeft(rectangles[x, y], y * (height + space));
                    Canvas.SetTop(rectangles[x, y], x * (height + space));
                }
            }
        }
        private void UpdateRectangles()
        {
            for (int x = 0; x < highway.x_size; x++)
            {
                for (int y = 0; y < highway.y_size; y++)
                {
                    switch (highway.lanePositions[x, y].thisState)
                    {
                        case LanePosition.LaneState.empty:
                            rectangles[x, y].Fill = Brushes.Gray;
                            break;
                        case LanePosition.LaneState.closed:
                            rectangles[x, y].Fill = Brushes.Red;
                            break;
                        case LanePosition.LaneState.occupied:
                            rectangles[x, y].Fill = Brushes.Green;
                            break;
                    }
                }
            }
        }
        public void UpdateSingleRectangles(object sender, Vehicle.PositionChangeData data)
        {
            rectangles[data.oldX, data.oldY].Fill = Brushes.Gray;
            if (!simulationConfig.VehicleExitIndex.Contains(data.newY))
            {
                rectangles[data.newX, data.newY].Fill = Brushes.Green;
            }
        }


    }
}