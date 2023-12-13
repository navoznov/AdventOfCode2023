using Day13;

var text = File.ReadAllText("input.txt");
var patterns = text.Split("\n\n")
    .Select(PatternParser.Parse)
    .ToArray();
var reflectionFinder = new ReflectionFinder();
var verticalLineReflectionIndexes = patterns.Select(map2d => reflectionFinder.TryGetVerticalLineReflectionIndex(map2d))
    .Where(s => s.Result)
    .Select(s => s.Index + 1)
    .ToArray();

for (var index = 0; index < patterns.Length; index++)
{
    var p = patterns[index];
    var i = reflectionFinder.TryGetHorizontalLineReflectionIndex(p);
}

var horizintalLineReflectionIndexes = patterns.Select(map2d => reflectionFinder.TryGetHorizontalLineReflectionIndex(map2d))
    .Where(s => s.Result)
    .Select(s => s.Index + 1)
    .ToArray();
Console.WriteLine(verticalLineReflectionIndexes.Sum() + 100 * horizintalLineReflectionIndexes.Sum());

enum Cell
{
    Ash,
    Rock,
}