using Common;

namespace Day11;

public class MapParser
{
    public Map2d<Cell> Parse(string[] lines)
    {
        var map = new Map2d<Cell>(lines[0].Length, lines.Length, () => Cell.Space);
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                if (ch == '#')
                {
                    map[x, y] = Cell.Galaxy;
                }
            }
        }

        return map;
    }
}