using System.Text;

namespace Common;

public class Map2d<T>
{
    public static readonly Vector[] AllVectors = { Vector.N, Vector.E, Vector.S, Vector.W, };

    public int XSize { get; init; }
    public int YSize { get; init; }

    private readonly T[,] _map;
    public int XMin { get; init; }
    public int XMax { get; init; }
    public int YMin { get; init; }
    public int YMax { get; init; }

    public Map2d(int xSize, int ySize, Func<T>? initValueCreator = null)
    {
        XSize = xSize;
        YSize = ySize;

        _map = new T[xSize, ySize];

        for (var i = 0; i < xSize; i++)
        {
            for (var j = 0; j < ySize; j++)
            {
                _map[i, j] = (initValueCreator != null ? initValueCreator() : default)!;
            }
        }
    }

    public Map2d(int xMin, int xMax, int yMin, int yMax, Func<T>? initValueCreator = null)
        : this(xMax - xMin + 1, yMax - yMin + 1, initValueCreator)
    {
        XMin = xMin;
        XMax = xMax;
        YMin = yMin;
        YMax = yMax;
    }


    public T this[Point point]
    {
        get => _map[point.X - XMin, point.Y - YMin];
        set => _map[point.X - XMin, point.Y - YMin] = value;
    }

    public T this[int x, int y]
    {
        get => _map[x - XMin, y - YMin];
        set => _map[x - XMin, y - YMin] = value;
    }

    public IEnumerable<ValuePoint<T>> GetAllItems()
    {
        for (var x = 0; x < XSize; x++)
        {
            for (var y = 0; y < YSize; y++)
            {
                yield return new ValuePoint<T>(_map[x, y], new Point(x, y));
            }
        }
    }

    public IEnumerable<ValuePoint<T>> GetAllInRow(int y)
    {
        for (var x = 0; x < XSize; x++)
        {
            yield return new ValuePoint<T>(_map[x, y], new Point(x, y));
        }
    }
    public IEnumerable<ValuePoint<T>> GetAllInColumn(int x)
    {
        for (var y = 0; y < YSize; y++)
        {
            yield return new ValuePoint<T>(_map[x, y], new Point(x, y));
        }
    }

    public bool Exists(Point point)
    {
        return Exists(point.X, point.Y);
    }

    private bool Exists(int x, int y)
    {
        return x - XMin < XSize
               && x - XMin > -1
               && y - YMin < YSize
               && y - YMin > -1;
    }

    public string ToString(Func<T, string> valuePrinter, bool addSpaces = true, bool addRowNumbers = false)
    {
        var stringBuilder = new StringBuilder();
        for (var y = 0; y < YSize; y++)
        {
            if (addRowNumbers)
            {
                stringBuilder.Append(y.ToString().PadLeft(3)).Append(' ');
            }

            for (var x = 0; x < XSize; x++)
            {
                var cellValue = _map[x, y];
                var str = valuePrinter(cellValue);
                stringBuilder.Append(str);
                if (addSpaces)
                {
                    stringBuilder.Append(' ');
                }
            }

            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

    public string ToString(Func<T, string> valuePrinter, Point point, int offset, bool addSpaces = true,
        bool addRowNumbers = false)
    {
        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(offset)} must not be negative.");
        }

        var (xMin, xMax) = (point.X - offset, point.X + offset);
        var (yMin, yMax) = (point.Y - offset, point.Y + offset);

        var stringBuilder = new StringBuilder();
        for (var y = yMin; y <= yMax; y++)
        {
            if (addRowNumbers)
            {
                stringBuilder.Append(y.ToString().PadLeft(3)).Append(' ');
            }

            for (var x = xMin; x <= xMax; x++)
            {
                if (!Exists(x, y))
                {
                    continue;
                }

                var cellValue = this[x, y];
                var str = valuePrinter(cellValue);
                stringBuilder.Append(str);
                if (addSpaces)
                {
                    stringBuilder.Append(' ');
                }
            }

            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }
}