using Common;

namespace Day14;

static class PlatformParser
{
    public static Map2d<Cell> Parse(string[] lines)
    {
        var initValueCreator = () => Cell.Empty;
        var map = new Map2d<Cell>(lines[0].Length, lines.Length, initValueCreator);
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                var cell = ParseCell(ch);
                if (cell != Cell.Empty)
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
            '.' => Cell.Empty,
            'O' => Cell.Round,
            '#' => Cell.Cube,
            _ => throw new ArgumentOutOfRangeException(nameof(ch), ch, null)
        };
    }
}