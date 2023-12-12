using Common;
using Day11;

var lines = File.ReadAllLines("input.txt");
var map = new MapParser().Parse(lines);
var mapPrinter = new MapPrinter();
Console.WriteLine(map.ToString(mapPrinter.Print, false));

Map2d<Cell> newMap = map;
newMap = new MapTransformer().TransformEmptyLines(map);
Console.WriteLine(newMap.ToString(mapPrinter.Print, false));

// .#...
// ....#
// #....

var galaxyPoints = newMap.GetAllItems()
    .Where(item => item.Value == Cell.Galaxy)
    .Select(vp => vp.Point)
    .ToArray();

Console.WriteLine($"Galaxies count is {galaxyPoints.Length}");
Console.WriteLine();

var galaxyPairs = GetGalaxyPairs(galaxyPoints).ToArray();

var aStar = new AStar();
Func<Cell, bool> isEmpty = x => x == Cell.Space;
var distances = new List<long>();
foreach (var tuple in galaxyPairs)
{
    Console.WriteLine(distances.Count + " " + galaxyPairs.Length);    
    var distance = ManhattanDistance(tuple.first, tuple.second);
    distances.Add(distance);
}

Console.WriteLine(distances.Sum());         // 9727458- high

IEnumerable<(Point first, Point second)> GetGalaxyPairs(Point[] points)
{
    for (var i = 0; i < points.Length - 1; i++)
    {
        for (var j = i + 1; j < points.Length; j++)
        {
            yield return (points[i], points[j]);
        }
    }
}

    static long ManhattanDistance(Point a, Point b) 
        => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
