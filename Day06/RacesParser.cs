using Common;

namespace Day06;

class RacesParser
{
    public Race[] Parse(string text)
    {
        var (timeStr, distanceStr) = text.Split('\n');
        (_, timeStr) = timeStr.Split(":");
        var times = ParseNumbers(timeStr);

        (_, distanceStr) = distanceStr.Split(":");
        var distances = ParseNumbers(distanceStr);
        return Enumerable.Range(0, times.Length)
            .Select(i => new Race(times[i], distances[i]))
            .ToArray();
    }

    protected virtual long[] ParseNumbers(string numbersStr)
    {
        return numbersStr.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
    }
}

class RaceAdvancedParser : RacesParser
{
    protected override long[] ParseNumbers(string numbersStr)
    {
        var numberStrs = numbersStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var numberStr = string.Join(null, numberStrs);
        var number = long.Parse(numberStr);
        return new[] { number };
    }
}