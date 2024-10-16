using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public int[] VehicleExitIndex;

        public float Timestep;
        public int IncomingVehiclePattern;
    }

    public class Simulation
    {
        public static Simulation Instance;

        private SimulationConfig config;
        public static Action<int> Timer_Tick;

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

            int vehicleSpawnMode = config.IncomingVehiclePattern;
            int x = 0;
            int y = 0;
            switch (vehicleSpawnMode)
            {
                case 1:
                    Random random = new Random();
                    if (vehiclesSpawned < config.VehicleCount)
                    {
                        x = random.Next(config.HighwayWidth);
                        y = 0;
                        SpawnVehicleAtPosition(x, y);
                    }
                    break;
                case 2:
                    x = vehiclesSpawned % config.HighwayWidth;
                    y = 0;
                    SpawnVehicleAtPosition(x, y);
                    break;
                case 3:
                    y = 0;
                    for (int i = 0; i < config.HighwayWidth; i++)
                    {
                        SpawnVehicleAtPosition(i, y);
                    }
                    break;
            }
        }

        private void SpawnVehicleAtPosition(int x, int y)
        {
            if (vehiclesSpawned >= config.VehicleCount)
            {
                return;
            }
            if (_highway.lanePositions[x, y].thisState == LanePosition.LaneState.empty)
            {
                var newVehicle = new Vehicle(_highway, x, y, config.VehicleExitIndex[vehiclesSpawned % config.VehicleExitIndex.Length], vehiclesSpawned);
                newVehicle.ExitReached += RecordVehicleData;
                _vehicles.Add(newVehicle);
                vehiclesSpawned++;
            }
        }

        int vehiclesFinished = 0;
        List<Vehicle.VehicleData> data = new List<Vehicle.VehicleData>();
        private void RecordVehicleData(object? sender, Vehicle.VehicleData e)
        {
            vehiclesFinished++;
            data.Add(e);
            string fileName = @"C:\Users\Zach\Desktop\TestResult";


            if (vehiclesFinished == config.VehicleCount)
            {
                SendTimerTick();
                _timer.Stop();
                try
                {
                    // Check if file already exists. If yes, delete it.
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            foreach (var v in data)
                            {
                                sw.WriteLine($"{v.vehicleNum},{v.lifetime},{v.stepsWaiting},{v.exit},{v.laneChanges}\n");
                            }
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            foreach (var v in data)
                            {
                                sw.WriteLine($"{v.vehicleNum},{v.lifetime},{v.stepsWaiting},{v.exit},{v.laneChanges}\n");
                            }
                            sw.Close();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
            //Process data here... Export to comma seperated format for excel.
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(config.Timestep);
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
            Timer_Tick?.Invoke(vehiclesFinished);
        }
    }
}
