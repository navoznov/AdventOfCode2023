using System.Text.RegularExpressions;
using Common;

namespace Day03;

internal class Engine
{
    private readonly string[] _lines;
    private static readonly Regex DetailNumberPattern = new(@"\D?(\d+)\D?");
    private readonly int _xSize;
    private readonly int _ySize;
    
    public Engine(string[] lines)
    {
        if (lines == null)
        {
            throw new ArgumentNullException(nameof(lines));
        }

        if (lines.Length == 0)
        {
            throw new ArgumentException("Value cannot be an empty collection.", nameof(lines));
        }

        _lines = lines;
        _xSize = lines[0].Length;
        _ySize = lines.Length;
    }

    public IEnumerable<Part> GetDetails()
    {
        for (var rowIndex = 0; rowIndex < _lines.Length; rowIndex++)
        {
            var line = _lines[rowIndex];
            var matchCollection = DetailNumberPattern.Matches(line);

            foreach (Match match in matchCollection)
            {
                var groupCollection = match.Groups;
                var group = groupCollection[1];
                var numberStr = group.Value;
                var colIndex = group.Index;
                var numberLength = group.Length;
                var part = new Part(numberStr, colIndex, numberLength, rowIndex);
                if (IsPartNumber(part))
                    yield return part;
            }
        }
    }

    bool IsPartNumber(Part part)
    {
        var index = part.ColIndex;
        var y = part.RowIndex;
        var length = part.Length;

        var rowsCount = _lines.Length;
        var colsCount = _lines[0].Length;

        if (index > 0
            && (IsSymbol(index - 1, y)
                || (y > 0 && IsSymbol(index - 1, y - 1))
                || (y < rowsCount - 1 && IsSymbol(index - 1, y + 1))))
        {
            return true;
        }

        if (index + length - 1 < colsCount - 1
            && (IsSymbol(index + length, y)
                || (y > 0 && IsSymbol(index + length, y - 1))
                || (y < rowsCount - 1 && IsSymbol(index + length, y + 1))))
        {
            return true;
        }


        for (var x = index; x < index + length; x++)
        {
            if (y > 0)
            {
                if (IsSymbol(x, y - 1))
                {
                    return true;
                }
            }

            if (y < rowsCount - 1)
            {
                if (IsSymbol(x, y + 1))
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool IsSymbol(int x, int y)
    {
        var letter = _lines[y][x];
        return letter != '.' && !IsDigit(letter);
    }


    public IEnumerable<int> GetGearRatios()
    {
        const char GEAR_LETTER = '*';

        for (var y = 0; y < _lines.Length; y++)
        {
            var prevIndex = -1;
            var line = _lines[y];
            int currentIndex;
            while ((currentIndex = line.IndexOf(GEAR_LETTER, prevIndex+1)) > -1)
            {
                prevIndex = currentIndex;
                var point = new Point(currentIndex, y);
                Console.WriteLine(point);
                const int NEIGHBOUR_COUNT = 2;
                var neighboursResult = GetExactCountOfNeighbours(point, NEIGHBOUR_COUNT);
                if (neighboursResult.Result)
                {
                    yield return neighboursResult.Neighbours.Aggregate(1, (i, i1) => i * i1);
                }
            }
        }
    }

    (bool Result, int[] Neighbours) GetExactCountOfNeighbours(Point gearPoint, int count)
    {
        var neighbours = new List<int>();
        var deltaYs = new[] { -1, 0, 1 };

        foreach (var deltaY in deltaYs)
        {
            var neighbourValues = GetNeighbourValues(gearPoint, deltaY);
            neighbours.AddRange(neighbourValues);
            if (neighbours.Count > count)
            {
                return (false, default)!;
            }
        }

        if (neighbours.Count!= count)
        {
            return (false, default)!;
        }
        
        return (true, neighbours.ToArray());
    }

    int[] GetNeighbourValues(Point point, int deltaY)
    {
        var y = point.Y + deltaY;
        if (y < 0 || y >= _ySize)
        {
            return Array.Empty<int>();
        }

        var deltaXs = new[] { -1, 0, 1 };
        var numbers = new List<List<char>>();
        var middleIsDigit = false;
        foreach (var deltaX in deltaXs)
        {
            var numberDigits = new List<char>();
            var counter = 1;
            while (true)
            {
                var (isDigit, value) = GetDigitChar(point.X + deltaX * counter, y);
                if (!isDigit)
                {
                    break;
                }

                numberDigits.Add(value);
                
                if (deltaX == 0)
                {
                    middleIsDigit = isDigit;
                    break;
                }

                counter++;
            }

            if (deltaX == -1)
            {
                numberDigits.Reverse();
            }

            numbers.Add(numberDigits);
        }


        return NeighbourValues(middleIsDigit, numbers);
    }

    private int[] NeighbourValues(bool middleIsDigit, List<List<char>> numbers)
    {
        // No middle
        if (!middleIsDigit)
        {
            return numbers.Select(GetNumber).Where(x => x > 0).ToArray();
        }

        var (left, middle, right) = numbers;
        var middleChar = middle.Single();
        // middle + right
        if (left.Count == 0)
        {
            right.Insert(0, middleChar);
            return new[] { GetNumber(right) };
        }

        // left + middle
        if (right.Count == 0)
        {
            left.Add(middleChar);
            return new[] { GetNumber(left) };
        }

        left.Add(middleChar);
        left.AddRange(right);
        return new[] { GetNumber(left) };
    }

    int GetNumber(List<char> numberDigits)
    {
        if (numberDigits.Count == 0)
        {
            return 0;
        }

        return numberDigits.Select((t, i) => (t - '0') * (int)Math.Pow(10, numberDigits.Count - 1 - i)).Sum();
    }

    private static bool IsDigit(char letter)
    {
        return letter is >= '0' and <= '9';
    }

    private (bool, char) GetDigitChar(int x, int y)
    {
        if (x < 0 || x >= _xSize || y < 0 || y >= _ySize)
        {
            return (false, default);
        }

        var ch = _lines[y][x];
        return IsDigit(ch) ? (true, ch) : (false, default);
    }

    internal record Part(string Number, int ColIndex, int Length, int RowIndex);
}