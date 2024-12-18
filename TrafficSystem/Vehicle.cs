﻿namespace TrafficSystem
{
    public class Vehicle
    {
        private Highway _highway;
        LanePosition currentLanePosition;
        int x_current;
        int y_current;
        int exit = -1;
        int steps_waiting;
        bool reachedExit;
        int vehicleProfile;
        private VehicleData _vehicleData;
        public struct VehicleData
        {
            public int lifetime;
            public int vehicleNum;
            public int stepsWaiting;
            public int laneChanges;
            public int exit;
            public int positionsTraversed;
            public int VehicleBehavior;
        }
        public struct PositionChangeData
        {
            public int oldX;
            public int oldY;
            public int newX;
            public int newY;
        }
        public static event EventHandler<PositionChangeData> UpdateVehicleDisplay;
        public event EventHandler<VehicleData> ExitReached;
        public Vehicle(Highway h, int startX, int startY, int exitIndex, int num)
        {
            _vehicleData.vehicleNum = num;
            x_current = startX;
            y_current = startY;
            _highway = h;
            _vehicleData.exit = exitIndex;
            currentLanePosition = _highway.lanePositions[startX, startY];
            currentLanePosition.thisState = LanePosition.LaneState.occupied;
            vehicleProfile = _vehicleData.vehicleNum % 3;
            _vehicleData.VehicleBehavior = vehicleProfile;
            _highway.VehicleTimeStep += Step;
        }
        private bool isEmpty(int offX, int offY)
        {
            int x = Math.Clamp(x_current + offX, 0, _highway.x_size - 1);
            int y = Math.Clamp(y_current + offY, 0, _highway.y_size - 1);
            return _highway.lanePositions[x, y].thisState == LanePosition.LaneState.empty;
        }
        private LanePosition.LaneState getNextPosition()
        {
            return _highway.lanePositions[x_current, y_current + 1].thisState;
        }
        public void Step(object sender, EventArgs e)
        {
            CheckForArrival();
            if (reachedExit)
            {
                return;
            }
            _vehicleData.lifetime++;

            if (NeedToMoveRight())
            {
                MoveRight(true);
                return;
            }
            switch (vehicleProfile)
            {
                case 0:
                    if (ReachedLaneClosure())
                    {
                        MoveLeft(true);
                    }
                    else
                    {
                        if (!MoveLeft(false))
                        {
                            MoveForward();
                        }

                    }
                    break;
                case 1:
                    if (ReachedLaneClosure())
                    {
                        MoveLeft(true);
                    }
                    else
                    {
                        if (!MoveRight(false))
                        {
                            MoveForward();
                        }

                    }
                    break;
                case 2:
                    if (ReachedLaneClosure())
                    {
                        MoveLeft(true);
                    }
                    else
                    {
                        MoveForward();
                    }
                    break;
            }
        }

        private void CheckForArrival()
        {
            if (y_current == _vehicleData.exit)
            {
                Console.WriteLine("Reached Exit");
                currentLanePosition.thisState = LanePosition.LaneState.empty;
                reachedExit = true;

                _highway.VehicleTimeStep -= Step;
                ExitReached?.Invoke(this, _vehicleData);
                return;
            }
        }

        private bool NeedToMoveRight()
        {
            return (_vehicleData.exit - y_current <= _highway.x_size - x_current);
        }
        private bool ReachedLaneClosure()
        {
            return (getNextPosition() == LanePosition.LaneState.closed);
        }
        private bool MoveForward()
        {
            if (isEmpty(0, 1))
            {
                PositionChangeData data = new PositionChangeData
                {
                    oldX = x_current,
                    oldY = y_current,
                    newX = x_current,
                    newY = y_current + 1,
                };
                currentLanePosition.thisState = LanePosition.LaneState.empty;
                currentLanePosition = _highway.lanePositions[data.newX, data.newY];
                _highway.lanePositions[data.newX, data.newY].thisState = LanePosition.LaneState.occupied;
                y_current = data.newY;
                _vehicleData.positionsTraversed++;
                UpdateVehicleDisplay(this, data);
                return true;
            }
            else
            {
                _vehicleData.stepsWaiting++;
                return false;
            }
        }

        private bool MoveRight(bool wait)
        {
            int rightMost = Math.Clamp(x_current + 1, 0, _highway.x_size - 1);

            if (_highway.lanePositions[rightMost, y_current + 1].thisState == LanePosition.LaneState.empty)
            {
                PositionChangeData data = new PositionChangeData
                {
                    oldX = x_current,
                    oldY = y_current,
                    newX = rightMost,
                    newY = y_current + 1,
                };
                currentLanePosition.thisState = LanePosition.LaneState.empty;
                currentLanePosition = _highway.lanePositions[data.newX, data.newY];
                currentLanePosition.thisState = LanePosition.LaneState.occupied;
                x_current = data.newX;
                y_current = data.newY;
                if(rightMost != x_current)_vehicleData.laneChanges++;
                _vehicleData.positionsTraversed++;
                UpdateVehicleDisplay(this, data);
                return true;
            }
            else
            {
                if (wait) _vehicleData.stepsWaiting++;
                return false;
            }
        }

        private bool MoveLeft(bool wait)
        {
            if (isEmpty(-1, 0) && isEmpty(-1, -1))
            {
                PositionChangeData data = new PositionChangeData
                {
                    oldX = x_current,
                    oldY = y_current,
                    newX = x_current - 1,
                    newY = y_current,
                };
                currentLanePosition.thisState = LanePosition.LaneState.empty;
                currentLanePosition = _highway.lanePositions[data.newX, data.newY];
                _highway.lanePositions[data.newX, data.newY].thisState = LanePosition.LaneState.occupied;
                x_current = data.newX;
                _vehicleData.laneChanges++;
                _vehicleData.positionsTraversed++;
                UpdateVehicleDisplay(this, data);
                return true;
            }
            else
            {
                if(wait) _vehicleData.stepsWaiting++;
                return false;
            }
        }
    }
}
