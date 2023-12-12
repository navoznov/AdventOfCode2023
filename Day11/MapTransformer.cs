using Common;

namespace Day11;

public class MapTransformer
{
    public Map2d<Cell> TransformEmptyLines(Map2d<Cell> map)
    {
        var emptyRowIndexes = Enumerable.Range(0, map.YSize)
            .Select(i => new { Points = map.GetAllInRow(i), RowIndex = i })
            .Where(x => x.Points.All(v => v.Value == Cell.Space))
            .Select(x => x.RowIndex)
            .ToHashSet();
        var emptyColumnIndexes = Enumerable.Range(0, map.XSize)
            .Select(i => new { Points = map.GetAllInColumn(i), ColumnIndex = i })
            .Where(x => x.Points.All(v => v.Value == Cell.Space))
            .Select(x => x.ColumnIndex)
            .ToHashSet();

        var newMap = new Map2d<Cell>(map.XSize + emptyColumnIndexes.Count, map.YSize + emptyRowIndexes.Count);

        var yOffset = 0;
        for (var y = 0; y < map.YSize; y++)
        {
            if (emptyRowIndexes.Contains(y))
            {
                yOffset++;
            }

            var xOffset = 0;
            for (var x = 0; x < map.XSize; x++)
            {
                if (map[x, y] == Cell.Galaxy)
                {
                    newMap[x + xOffset, y + yOffset] = Cell.Galaxy;
                }
                else if (emptyColumnIndexes.Contains(x))
                {
                    xOffset++;
                }
            }
        }

        return newMap;
    }
}