using Day05;

var inputText = File.ReadAllText("input.txt");
var almanacParser = new AlmanacParser();
var almanac = almanacParser.Parse(inputText);

var locations = almanac.Seeds
    .Select(s => ProcessSeed(s, almanac.Maps))
    .ToArray();
Console.WriteLine(locations.Min());

long ProcessSeed(long seed, Map[] maps)
{
    var value = seed;
    foreach (var map in maps)
    {
        value = Transform(value, map);
    }

    return value;
}

long Transform(long value, Map map)
{
    foreach (var rule in map.Rules)
    {
        if (value >= rule.SourceRangeStart && value < rule.SourceRangeStart + rule.RangeLength)
        {
            return value - rule.SourceRangeStart + rule.DestinationRangeStart;
        }
    }

    return value;
}

Console.WriteLine();


record MapHeader(Category From, Category To);

record Map(MapHeader Header, Rule[] Rules);

record Almanac(long[] Seeds, Map[] Maps)
{
}

enum Category
{
    Seed,
    Soil,
    Fertilizer,
    Water,
    Light,
    Temperature,
    Humidity,
    Location,
}

record Rule(long DestinationRangeStart, long SourceRangeStart, long RangeLength);