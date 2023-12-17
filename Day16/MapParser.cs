using Common;

namespace Day16;

static class MapParser
{
    public static Map2d<Cell> Parse(string[] lines)
    {
        var map = new Map2d<Cell>(lines[0].Length, lines.Length);
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                map[x, y] = ch switch
                {
                    '.' => Cell.Empty,
                    '\\' => Cell.Mirror_NW_SE,
                    '/' => Cell.Mirror_NE_SW,
                    '|' => Cell.Splitter_V,
                    '-' => Cell.Splitter_H,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        return map;
    }
}