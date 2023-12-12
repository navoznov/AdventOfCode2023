using Common;
using Day11;

var lines = File.ReadAllLines("input.txt");
var map = new MapParser().Parse(lines);
var mapPrinter = new MapPrinter();
Console.WriteLine(map.ToString(mapPrinter.Print, false));

var newMap = new MapTransformer().TransformEmptyLines(map);

var galaxyPoints = newMap.GetAllItems()
    .Where(item => item.Value == Cell.Galaxy)
    .Select(vp => vp.Point)
    .ToArray();
var galaxyPairs = GetGalaxyPairs(galaxyPoints).ToArray();
Console.WriteLine(galaxyPairs.Sum(p => ManhattanDistance(p.first, p.second)));


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