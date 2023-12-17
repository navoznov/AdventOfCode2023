using Common;
using Day16;

var lines = File.ReadAllLines("input.txt");
var map = MapParser.Parse(lines);


// Part 1
var startBeam = new Beam(new Point(0, 0), Vector.E);
var energizedPointsCount = GetEnergizedPointsCount(startBeam, map);
Console.WriteLine(energizedPointsCount);

// Part 2
var sStartBeams = Enumerable.Range(0, map.XSize)
    .Select(x => new Beam(new Point(x, 0), Vector.S));
var nStartBeams = Enumerable.Range(0, map.XSize)
    .Select(x => new Beam(new Point(x, map.YSize - 1), Vector.N));
var eStartBeams = Enumerable.Range(0, map.YSize)
    .Select(y => new Beam(new Point(0, y), Vector.E));
var wStartBeams = Enumerable.Range(0, map.YSize)
    .Select(y => new Beam(new Point(map.XSize - 1, y), Vector.W));
var allStartBeams = Array.Empty<Beam>()
    .Union(sStartBeams)
    .Union(nStartBeams)
    .Union(eStartBeams)
    .Union(wStartBeams)
    .ToArray();
var maxCount = allStartBeams.Max(b => GetEnergizedPointsCount(b, map));
Console.WriteLine(maxCount);

int GetEnergizedPointsCount(Beam startBeam1, Map2d<Cell> map2d)
{
    Queue<Beam> beamsToDiscover = new();
    HashSet<Beam> visitedBeams = new HashSet<Beam>();

    beamsToDiscover.Enqueue(startBeam1);

    while (beamsToDiscover.TryDequeue(out Beam beam))
    {
        if (visitedBeams.Contains(beam))
        {
            continue;
        }

        visitedBeams.Add(beam);

        var cell = map2d[beam.Point];
        var nextBeams = GetNextBeams(cell, beam)
            .Where(b => map2d.Exists(b.Point));
        foreach (var nextPoint in nextBeams)
        {
            beamsToDiscover.Enqueue(nextPoint);
        }
    }

    return visitedBeams.Select(b => b.Point).Distinct().Count();
}

Vector[] GetOutcomeDirections(Cell cell, Vector incomeDirection)
{
    switch (cell)
    {
        case Cell.Empty:
            return new[] { incomeDirection };

        case Cell.Mirror_NE_SW when incomeDirection == Vector.N:
            return new[] { Vector.E };
        case Cell.Mirror_NE_SW when incomeDirection == Vector.E:
            return new[] { Vector.N };
        case Cell.Mirror_NE_SW when incomeDirection == Vector.S:
            return new[] { Vector.W };
        case Cell.Mirror_NE_SW when incomeDirection == Vector.W:
            return new[] { Vector.S };

        case Cell.Mirror_NW_SE when incomeDirection == Vector.N:
            return new[] { Vector.W };
        case Cell.Mirror_NW_SE when incomeDirection == Vector.W:
            return new[] { Vector.N };
        case Cell.Mirror_NW_SE when incomeDirection == Vector.S:
            return new[] { Vector.E };
        case Cell.Mirror_NW_SE when incomeDirection == Vector.E:
            return new[] { Vector.S };

        case Cell.Splitter_H when incomeDirection == Vector.N || incomeDirection == Vector.S:
            return new[] { Vector.W, Vector.E };
        case Cell.Splitter_H when incomeDirection == Vector.E || incomeDirection == Vector.W:
            return new[] { incomeDirection };

        case Cell.Splitter_V when incomeDirection == Vector.N || incomeDirection == Vector.S:
            return new[] { incomeDirection };
        case Cell.Splitter_V when incomeDirection == Vector.E || incomeDirection == Vector.W:
            return new[] { Vector.N, Vector.S };
        default:
            throw new ArgumentOutOfRangeException(nameof(incomeDirection));
    }
}

IEnumerable<Beam> GetNextBeams(Cell cell, Beam beam)
{
    var outcomeDirections = GetOutcomeDirections(cell, beam.IncomeDirection);
    return outcomeDirections.Select(v => new Beam(beam.Point + v, v));
}

record Beam(Point Point, Vector IncomeDirection);

enum Cell
{
    Empty,
    Mirror_NW_SE,
    Mirror_NE_SW,
    Splitter_V,
    Splitter_H,
}