using System.Diagnostics;
using Common;

namespace Day10;

class PathFinder
{
    private static readonly Dictionary<Cell, Vector[]> AllConnectors = new()
    {
        { Cell.Empty, Array.Empty<Vector>() },
        { Cell.Start, new[] { Vector.N, Vector.W, Vector.S, Vector.E, } },

        { Cell.WE, new[] { Vector.W, Vector.E, } },
        { Cell.NS, new[] { Vector.N, Vector.S, } },

        { Cell.SW, new[] { Vector.S, Vector.W, } },
        { Cell.NW, new[] { Vector.N, Vector.W, } },

        { Cell.NE, new[] { Vector.N, Vector.E, } },
        { Cell.SE, new[] { Vector.S, Vector.E, } },
    };

    private static readonly Vector[] AllDirections = { Vector.N, Vector.S, Vector.W, Vector.E, };

    public int GetLoopStepsCount(Map2d<Cell> map, Point startPoint)
    {
        var stepsCounter = 0;

        // debug
        var history = new List<char>();
        
        var currentPoint = startPoint;
        var vector = GetPointDirection(currentPoint, map);

        history.Add(MapParser.GetCharByValue(map[currentPoint]));

        while (true)
        {
            // debug
            string MapValuePrinter(Cell x) => MapParser.GetCharByValue(x).ToString();
            Debug.WriteLine($"Step = {stepsCounter}, ");
            Debug.WriteLine($"Current point {MapParser.GetCharByValue(map[currentPoint])} = {currentPoint}");
            Debug.Write(map.ToString(MapValuePrinter, currentPoint, 1, false, false));

            currentPoint += vector;
            stepsCounter++;
            
            // debug
            history.Add(MapParser.GetCharByValue(map[currentPoint]));
            
            if (currentPoint == startPoint)
            {
                break;
            }

            var oppositeVector = vector * -1;
            var cell = map[currentPoint];
            vector = AllConnectors[cell].Single(x => x != oppositeVector);
            
            // debug
            Debug.WriteLine($"Next step vector {VectorPrinter(vector)} = {vector}");
            Debug.WriteLine("");
        }

        return stepsCounter;
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

    string VectorPrinter(Vector v)
    {
        return v switch
        {
            { X: 0, Y: 1 } => "↓",
            { X: 0, Y: -1 } => "↑",
            { X: 1, Y: 0 } => "→",
            { X: -1, Y: 0 } => "←",
            _ => throw new ArgumentOutOfRangeException(nameof(v)),
        };
    }
}