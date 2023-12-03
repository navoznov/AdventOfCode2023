using System.Text.RegularExpressions;

internal class Engine
{
    private readonly string[] _lines;
    private static readonly Regex DetailNumberPattern = new Regex(@"\D?(\d+)\D?");

    public Engine(string[] lines)
    {
        _lines = lines;
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
        return IsSymbol(letter);
    }

    bool IsSymbol(char letter)
    {
        return letter != '.' && letter is < '0' or > '9';
    }

    internal record Part(string Number, int ColIndex, int Length, int RowIndex);
}