using Common;

namespace Day06;

static class RacesParser
{
    public static Race[] Parse(string text)
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

    static int[] ParseNumbers(string numbersStr)
    {
        return numbersStr.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}