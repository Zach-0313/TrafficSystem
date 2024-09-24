using static TrafficSystem.LanePosition;

namespace TrafficSystem
{
    public class LanePosition
    {
        public enum State { empty, closed, occupied };
        public State thisState = State.empty;
        public LanePosition()
        {
            thisState = State.empty;
        }
    }
    public class Highway
    {
        public LanePosition[,] lanePositions;
        public int x_size;
        public int y_size;

        public event EventHandler VehicleTimeStep;
        public void Timestep()
        {
            VehicleTimeStep?.Invoke(this, EventArgs.Empty);
        }
        public Highway(int X, int Y)
        {
            x_size= X; y_size = Y;
            lanePositions = new LanePosition[x_size, y_size];
            foreach (var lane in lanePositions)
                for (int a = 0; a != x_size; a++)
                {
                    for (int b = 0; b != y_size; b++)
                    {
                        lanePositions[a, b] = new LanePosition();
                    }
                }
        }
        public void CloseLane(int start, int end, int depth)
        {
            for (int x = x_size - depth; x < x_size; x++)
            {
                for (int y = start; y < end; y++)
                {
                    lanePositions[x, y].thisState = State.closed;
                }
            }
        }
    }
}
