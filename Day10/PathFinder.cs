using System.Diagnostics;
using Common;

namespace Day10;

class PathFinder
{
    private static readonly Vector[] AllVectors = { Vector.N, Vector.E, Vector.S, Vector.W, };

    private static readonly Dictionary<Cell, Vector[]> AllConnectors = new()
    {
        { Cell.Empty, Array.Empty<Vector>() },
        { Cell.Start, AllVectors },

        { Cell.WE, new[] { Vector.W, Vector.E, } },
        { Cell.NS, new[] { Vector.N, Vector.S, } },

        { Cell.SW, new[] { Vector.S, Vector.W, } },
        { Cell.NW, new[] { Vector.N, Vector.W, } },

        { Cell.NE, new[] { Vector.N, Vector.E, } },
        { Cell.SE, new[] { Vector.S, Vector.E, } },
    };

    private static readonly Vector[] AllDirections = { Vector.N, Vector.S, Vector.W, Vector.E, };

    public Point[] GetLoopPath(Map2d<Cell> map, Point startPoint)
    {
        var path = new List<Point>();

        var currentPoint = startPoint;
        var vector = GetPointDirection(currentPoint, map);

        while (true)
        {
            currentPoint += vector;
            path.Add(currentPoint);
            if (currentPoint == startPoint)
            {
                break;
            }

            var oppositeVector = vector * -1;
            var cell = map[currentPoint];
            vector = AllConnectors[cell].Single(x => x != oppositeVector);
        }

        return path.ToArray();
    }

    Vector GetPointDirection(Point point, Map2d<Cell> map)
    {
        foreach (var vector in AllDirections)
        {
            var neighbourPoint = point + vector;
            if (!map.Exists(neighbourPoint))
            {
                continue;
            }

            var cell = map[neighbourPoint];
            if (cell == Cell.Empty)
            {
                continue;
            }

            var oppositeVector = vector * -1;
            var connectors = AllConnectors[cell];
            if (connectors.Any(v => v == oppositeVector))
            {
                return vector;
            }
        }

        throw new Exception("Start point must have at least 2 connections");
    }

    public Point[] GetInnerPoints(Point[] pathPoints)
    {
        var maxX = pathPoints.Max(p => p.X);
        var maxY = pathPoints.Max(p => p.Y);
        var map = new Map2d<ColoredCell>(maxX * 2 + 1, maxY * 2 + 1, () => ColoredCell.Unknown);

        pathPoints = pathPoints.Select(p => new Point(p.X * 2, p.Y * 2)).ToArray();

        var prevPoint = pathPoints.Last();
        foreach (var point in pathPoints)
        {
            var vector = point - prevPoint;
            var normalizedVector = new Vector(Math.Sign(vector.X), Math.Sign(vector.Y));
            var currentPoint = prevPoint;
            while (currentPoint != point)
            {
                map[currentPoint] = ColoredCell.Path;
                currentPoint += normalizedVector;
            }

            prevPoint = point;
        }

        PaintLineByLine(map);

        var allInnerPoints = map.GetAllItems()
            .Where(vp => vp.Value == ColoredCell.Inner)
            .Where(vp => vp.Point.X % 2 == 0 && vp.Point.Y % 2 == 0)
            .Where(vp => AllVectors.Select(v => vp.Point + v).All(p => map.Exists(p) && map[p] == ColoredCell.Inner))
            .ToArray();
        return allInnerPoints.Select(x => x.Point).ToArray();
    }

    private static void PaintLineByLine(Map2d<ColoredCell> map)
    {
        for (int x = 0; x < map.XSize; x++)
        {
            var outer = true;
            var pathPointsCounter = 0;
            for (int y = 0; y < map.YSize; y++)
            {
                var point = new Point(x, y);

                if (map[point] == ColoredCell.Unknown)
                {
                    if (outer)
                    {
                        if (pathPointsCounter != 1)
                        {
                            map[point] = ColoredCell.Outer;
                        }
                        else
                        {
                            outer = false;
                            pathPointsCounter = 0;
                        }
                    }
                    else
                    {
                        if (pathPointsCounter != 1)
                        {
                            map[point] = ColoredCell.Inner;
                            PaintOuterNeighbours(map, point, ColoredCell.Inner);
                        }
                        else
                        {
                            outer = true;
                            pathPointsCounter = 0;
                        }
                    }
                }
                else if (map[point] == ColoredCell.Path)
                {
                    pathPointsCounter++;
                }
            }
        }
    }


    static void PaintOuterNeighbours(Map2d<ColoredCell> map, Point? paintingStartPoint = null,
        ColoredCell color = ColoredCell.Outer)
    {
        if (paintingStartPoint is null)
        {
            paintingStartPoint = map.GetAllItems()
                .Where(x => x.Value == ColoredCell.Unknown)
                .First(x => x.Point.X == 0 || x.Point.Y == 0)
                .Point;
            color = ColoredCell.Outer;
        }

        var points = new Queue<Point>();
        points.Enqueue(paintingStartPoint);

        while (points.TryDequeue(out var point))
        {
            map[point] = color;

            var unknownNeighbours = AllVectors.Select(v => point + v)
                .Where(p => map.Exists(p) && map[p] == ColoredCell.Unknown);
            foreach (var neighbour in unknownNeighbours)
            {
                if (!points.Contains(neighbour))
                {
                    points.Enqueue(neighbour);
                }
            }
        }
    }

    enum ColoredCell
    {
        Unknown,
        Path,
        Outer,
        Inner,
    }
}