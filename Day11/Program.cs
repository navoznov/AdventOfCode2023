using Common;
using Day11;

var lines = File.ReadAllLines("input.txt");
var map = new MapParser().Parse(lines);

var emptyRowIndexes = GetEmptyLineIndexes(map.YSize, map.GetAllInRow);
var emptyColumnIndexes = GetEmptyLineIndexes(map.XSize, map.GetAllInColumn);
var galaxyPoints = map.GetAllItems()
    .Where(item => item.Value == Cell.Galaxy)
    .Select(vp => vp.Point)
    .ToArray();
var galaxyPairs = GetGalaxyPairs(galaxyPoints).ToArray();

// Part 1
var sum1 = galaxyPairs.Sum(gp => CalcDistancesSum(gp, emptyRowIndexes, emptyColumnIndexes, 2));
Console.WriteLine(sum1);

// Part 2
var sum2 = galaxyPairs.Sum(gp => CalcDistancesSum(gp, emptyRowIndexes, emptyColumnIndexes, 1_000_000));
Console.WriteLine(sum2);


long CalcDistancesSum((Point First, Point Second) galaxyPair, HashSet<int> emptyRowIndexes,
    HashSet<int> emptyColumnIndexes, int emptyLineWeight)
{
    var (first, second) = galaxyPair;
    var manhattanDistance = ManhattanDistance(first, second);
    var (x1, x2) = (int.Min(first.X, second.X), int.Max(first.X, second.X));
    var (y1, y2) = (int.Min(first.Y, second.Y), int.Max(first.Y, second.Y));
    var emptyLinesCount = emptyColumnIndexes.Count(x => x > x1 && x < x2)
                          + emptyRowIndexes.Count(y => y > y1 && y < y2);
    return manhattanDistance + (emptyLinesCount * (emptyLineWeight - 1));
}

HashSet<int> GetEmptyLineIndexes(int size, Func<int, IEnumerable<ValuePoint<Cell>>> itemsGetter)
{
    return Enumerable.Range(0, size)
        .Select(i => new { Points = itemsGetter(i), Index = i })
        .Where(x => x.Points.All(v => v.Value == Cell.Space))
        .Select(x => x.Index)
        .ToHashSet();
}

IEnumerable<(Point First, Point Second)> GetGalaxyPairs(Point[] points)
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