using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TrafficSystem
{
    public struct SimulationConfig 
    {
        public int HighwayWidth;
        public int HighwayLength;
        public int LaneClosureStart;
        public int LaneClosureEnd;
        public int LaneClosureWidth;

        public int VehicleCount;
        public int VehicleExitIndex;

        public float Timestep;
    }

    public class Simulation
    {
        public static Simulation Instance;

        private SimulationConfig config;
        public static Action Timer_Tick;

        private int vehiclesSpawned;
        private List<Vehicle> _vehicles = new List<Vehicle>();

        public Highway _highway;
        private DispatcherTimer _timer;
        private int _timeSteps = 0;

        private bool highwaySetup;
        public Simulation(SimulationConfig simulationConfig)
        {
            config = simulationConfig;
            InitalizeHighway();
        }
        public void InitalizeHighway()
        {
            if (Instance == null) Instance = this;

            _highway = new Highway(config.HighwayWidth, config.HighwayLength);
            _highway.CloseLane(config.LaneClosureStart, config.LaneClosureEnd, config.LaneClosureWidth);
            highwaySetup = true;
            StartTimer();
        }
        void SpawnVehicles()
        {
            if (!highwaySetup) return;

            Random random = new Random();
            if (vehiclesSpawned < config.VehicleCount)
            {
                int x = random.Next(config.HighwayWidth - 1);
                int y = random.Next(3);

                if (_highway.lanePositions[x, y].thisState == LanePosition.LaneState.empty)
                {
                    _vehicles.Add(new Vehicle(_highway, x, y, config.VehicleExitIndex, vehiclesSpawned));
                    vehiclesSpawned++;
                }
            }
        }
        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(.25);
            _timer.Tick += TimeElapsed;
            _timer.Start();
        }

        public void TimeElapsed(object sender, EventArgs e)
        {
            SendTimerTick();
            SpawnVehicles();
            _highway.Timestep();
            _timeSteps++;
        }
        public void SendTimerTick()
        {
            Timer_Tick?.Invoke();
        }
    }
}
