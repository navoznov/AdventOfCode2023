using Common;

namespace Day10;

class MapParser
{
    private static readonly Dictionary<char, Cell> AllCells = new()
    {
        { '.', Cell.Empty },
        { 'S', Cell.Start },
        { '-', Cell.WE },
        { '|', Cell.NS },
        { '7', Cell.SW },
        { 'J', Cell.NW },
        { 'L', Cell.NE },
        { 'F', Cell.SE },
    };

    public static readonly Dictionary<Cell, char> ReversedCells = AllCells.ToDictionary(x => x.Value, x => x.Key); 

    public static char GetCharByValue(Cell cell)
    {
        return ReversedCells[cell];
    }

        public (Map2d<Cell> map, Point startPoint) Parse(string[] lines)
    {
        Point startPoint = default!;
        var map = new Map2d<Cell>(lines[0].Length, lines.Length);
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                var cell = AllCells[ch];
                map[x, y] = cell;
                if (cell == Cell.Start)
                {
                    startPoint = new Point(x, y);
                }
            }
        }

        return (map, startPoint);
    }
}