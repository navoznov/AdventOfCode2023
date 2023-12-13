using Common;

namespace Day12;

class ReportParser
{
    public Report Parse(string str)
    {
        var (springStr, lengthsStr) = str.Split(' ');
        var springs = springStr.ToCharArray()
            .Select(ParseCell)
            .ToArray();
        var lengths = lengthsStr.Split(',').Select(int.Parse).ToArray();
        return new Report(springs, lengths);
    }

    Cell ParseCell(char ch) => ch switch
    {
        '.' => Cell.Operational,
        '#' => Cell.Damaged,
        '?' => Cell.Unknown,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), ch, null)
    };
}