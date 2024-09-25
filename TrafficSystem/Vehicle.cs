namespace TrafficSystem
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
        private VehicleData _vehicleData;
        public struct VehicleData
        {
            public int vehicleNum;
            public int stepsWaiting;
            public int laneChanges;
            public int exit;
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

            if (reachedExit)
            {
                return;
            }
            if (y_current >= exit && x_current == _highway.x_size - 1)
            {
                Console.WriteLine("Reached Exit");
                currentLanePosition.thisState = LanePosition.LaneState.empty;
                reachedExit = true;

                ExitReached?.Invoke(this, _vehicleData);
                return;
            }
            int rightMost = Math.Clamp(x_current + 1, 0, _highway.x_size - 1);
            if ((x_current != _highway.x_size) && (_vehicleData.exit - y_current <= _highway.x_size + 1))
            {
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
                    _highway.lanePositions[data.newX, data.newY].thisState = LanePosition.LaneState.occupied;
                    x_current = data.newX;
                    y_current = data.newY;
                    _vehicleData.laneChanges++;
                    UpdateVehicleDisplay(this, data);
                }
                else
                {
                    _vehicleData.stepsWaiting++;
                }
                return;
            }
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
                _vehicleData.laneChanges++;
                UpdateVehicleDisplay(this, data);
                return;
            }
            if (getNextPosition() == LanePosition.LaneState.occupied)
            {
                _vehicleData.stepsWaiting++;
                return;
            }
            if (getNextPosition() == LanePosition.LaneState.closed)
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
                    UpdateVehicleDisplay(this, data);
                }
                else
                {
                    _vehicleData.stepsWaiting++;
                }
                return;
            }
        }
    }
}
