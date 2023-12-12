namespace Common;

public class AStar
{
    // May be invalid
    public Point[] GetPath<T>(Map2d<T> map, Point startPoint, Point finishPoint, Func<T, bool> isEmpty)
    {
        const int STEP_COST = 1;

        var startTile = new Tile<T>(map[startPoint], startPoint) { Cost = 0, };
        var reachable = new PriorityQueue<Tile<T>, int>();
        var reached = new HashSet<Tile<T>> { startTile };
        reachable.Enqueue(startTile, 0);
        var explored = new HashSet<Tile<T>> { startTile };


        while (reachable.Count > 0)
        {
            var tile = reachable.Dequeue();
            reached.Remove(tile);

            explored.Add(tile);

            var adjacentTiles = GetAdjacentTiles(tile, map, isEmpty);
            foreach (var adjacentTile in adjacentTiles)
            {
                adjacentTile.Parent = tile;
                adjacentTile.Cost = tile.Cost + STEP_COST;

                if (finishPoint == adjacentTile)
                {
                    return GetPathPointsToTile(adjacentTile);
                }

                if (reached.Contains(adjacentTile) || explored.Contains(adjacentTile))
                {
                    continue;
                }

                if (!isEmpty(map[adjacentTile]))
                {
                    continue;
                }


                reachable.Enqueue(adjacentTile, adjacentTile.Cost);
                reached.Add(adjacentTile);
            }
        }

        return Array.Empty<Point>();
    }

    IEnumerable<Tile<T>> GetAdjacentTiles<T>(Tile<T> tile, Map2d<T> map, Func<T, bool> isEmpty)
    {
        return Map2d<T>.AllVectors
            .Select(v => tile + v)
            .Where(map.Exists)
            .Select(p => new Tile<T>(map[p], p));
    }

    private Point[] GetPathPointsToTile<T>(Tile<T> finishTile)
    {
        var path = new List<Point> { finishTile };
        var tile = finishTile;
        while ((tile = tile.Parent) != null)
        {
            path.Add(tile);
        }

        path.Reverse();
        return path.ToArray();
    }

    record Tile<T>(T Value, int X, int Y) : Point(X, Y)
    {
        public T Value { get; init; } = Value;
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile<T> Parent { get; set; } = null!;

        public Tile(T Value, Point point) : this(Value, point.X, point.Y)
        {
        }

        public Tile(ValuePoint<T> valuePoint) : this(valuePoint.Value, valuePoint.Point)
        {
        }

        public void SetDistance(int targetX, int targetY)
        {
            Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }

        public void SetDistance(Point target)
        {
            SetDistance(target.X, target.Y);
        }

        public virtual bool Equals(Tile<T>? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }
}