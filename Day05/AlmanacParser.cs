using Common;

namespace Day05;

class AlmanacParser
{
    private static readonly Dictionary<string, Category> AllCategoriesNames;

    static AlmanacParser()
    {
        AllCategoriesNames = Enum.GetValues(typeof(Category))
            .Cast<Category>()
            .ToDictionary(v => v.ToString("G").ToLower(), v => v);
    }

    public Almanac Parse(string text)
    {
        var blocks = text.Split("\n\n");
        var (_, seedsStr) = blocks.First().Split(": ");
        var seeds = ParseNumbers(seedsStr);

        var maps = blocks.Skip(1).Select(ParseMap).ToArray();

        return new Almanac(seeds, maps);
    }

    private Map ParseMap(string block)
    {
        var lines = block.Split('\n');
        var header = ParseHeader(lines.First());
        var rules = lines.Skip(1).Select(ParseRule).ToArray();
        return new Map(header, rules);
    }

    private MapHeader ParseHeader(string line)
    {
        var namesStr = line.Split(' ').First();
        var (from, to) = namesStr
            .Split("-to-")
            .Select(x => AllCategoriesNames[x])
            .ToArray();
        return new MapHeader(from, to);
    }

    private Rule ParseRule(string line)
    {
        var (destinationRangeStart, sourceRangeStart, rangeLength) = ParseNumbers(line);
        return new Rule(destinationRangeStart, sourceRangeStart, rangeLength);
    }
    
    private long[] ParseNumbers(string str)
    {
        return str.Split(' ').Select(long.Parse).ToArray();
    }
}