using Common;

namespace Day13;

static class PatternParser
{
    public static Map2d<Cell> Parse(string str)
    {
        var lines = str.Split('\n');
        var map = new Map2d<Cell>(lines[0].Length, lines.Length, () => Cell.Ash);
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                var cell = ParseCell(ch);
                if (cell != Cell.Ash)
                {
                    map[x, y] = cell;
                }
            }
        }

        return map;
    }

    static Cell ParseCell(char ch)
    {
        return ch switch
        {
            '.' => Cell.Ash,
            '#' => Cell.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(ch), ch, null)
        };
    }
}