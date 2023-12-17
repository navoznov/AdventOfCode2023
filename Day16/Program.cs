using Common;
using Day16;

var lines = File.ReadAllLines("input.txt");
var map = MapParser.Parse(lines);

Queue<Beam> beamsToDiscover = new();
HashSet<Beam> visitedBeams = new HashSet<Beam>();

var startBeam = new Beam(new Point(0, 0), Vector.E);
beamsToDiscover.Enqueue(startBeam);

while (beamsToDiscover.TryDequeue(out Beam beam))
{
    if (visitedBeams.Contains(beam))
    {
        continue;
    }

    visitedBeams.Add(beam);

    var cell = map[beam.Point];
    var nextBeams = GetNextBeams(cell, beam)
        .Where(b => map.Exists(b.Point));
    foreach (var nextPoint in nextBeams)
    {
        beamsToDiscover.Enqueue(nextPoint);
    }
}

var visitedPoints = visitedBeams.Select(b=>b.Point).Distinct().ToArray();
Console.WriteLine(visitedPoints.Length);

IEnumerable<Beam> GetNextBeams(Cell cell, Beam beam)
{
    var outcomeDirections = GetOutcomeDirections(cell, beam.IncomeDirection);
    return outcomeDirections.Select(v => new Beam(beam.Point + v, v));
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

record Beam(Point Point, Vector IncomeDirection);

enum Cell
{
    Empty,
    Mirror_NW_SE,
    Mirror_NE_SW,
    Splitter_V,
    Splitter_H,
}