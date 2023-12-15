using Common;

namespace Day14;

class PlatformTilter
{
    public static Map2d<Cell> TiltPlatform(Map2d<Cell> map)
    {
        var vector = Vector.N;

        for (var y = 1; y < map.YSize; y++)
        {
            for (var x = 0; x < map.XSize; x++)
            {
                var currentPoint = new Point(x, y);
                var cell = map[currentPoint];
                if (cell != Cell.Round)
                {
                    continue;
                }

                Roll(currentPoint, map);
            }
        }

        return map;
    }

    private static void Roll(Point startPoint, Map2d<Cell> map)
    {
        var point = startPoint;
        while (true)
        {
            var nextPoint = new Point(point.X, point.Y - 1);
            if (!map.Exists(nextPoint) || map[nextPoint] != Cell.Empty)
            {
                break;
            }

            map[point] = Cell.Empty;
            map[nextPoint] = Cell.Round;
            point = nextPoint;
        }
    }
}