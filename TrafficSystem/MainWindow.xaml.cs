using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TrafficSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int vehicleCount = 20;
        int vehiclesSpawned = 0;
        public static Highway highway = new Highway(5, 100);
        List<Vehicle> vehicles = new List<Vehicle>();
        Rectangle[,] rectangles = new Rectangle[highway.x_size, highway.y_size];

        private DispatcherTimer _timer;
        private int _timeSteps = 0;

        public MainWindow()
        {
            InitializeComponent();
            highway.CloseLane(7, 15, 3);
            StartTimer();
            DrawHighway(MyCanvas);
        }
        void SpawnVehicles()
        {
            Random random = new Random();
            if (vehiclesSpawned < vehicleCount)
            {
                int x = random.Next(highway.x_size-1);
                int y = random.Next(3);

                if (highway.lanePositions[x,y].thisState == LanePosition.State.empty)
                {
                    vehicles.Add(new Vehicle(highway, x, y, 30));
                    vehicleCount++;
                }
            }
        }
        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(.5);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            SpawnVehicles();

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Step();
            }
            _timeSteps++;
            MyTextBlock.Text = _timeSteps.ToString();
            UpdateRectangles();

        }
        public void DrawHighway(Canvas canvas)
        {
            int height = 10;
            int space = 2;


            for (int x = 0; x < highway.x_size; x++)
            {
                for (int y = 0; y < highway.y_size; y++)
                {
            rectangles[x, y] = new Rectangle
            {
                Height = height,
                Width = height,
            };
                    switch (highway.lanePositions[x,y].thisState)
                    {
                        case LanePosition.State.empty:
                            rectangles[x, y].Fill = Brushes.Gray;
                            break;
                        case LanePosition.State.closed:
                            rectangles[x, y].Fill = Brushes.Red;
                            break;
                        case LanePosition.State.occupied:
                            rectangles[x, y].Fill = Brushes.Green;
                            break;
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
                        case LanePosition.State.empty:
                            rectangles[x, y].Fill = Brushes.Gray;
                            break;
                        case LanePosition.State.closed:
                            rectangles[x, y].Fill = Brushes.Red;
                            break;
                        case LanePosition.State.occupied:
                            rectangles[x, y].Fill = Brushes.Green;
                            break;
                    }
                }
            }
        }
    }
}