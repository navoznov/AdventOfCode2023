using Day02;

using State = System.Collections.Generic.Dictionary<Color, int>;

var lines = File.ReadAllLines("input.txt");
var gameParser = new GameParser();
var games = lines.Select(line => gameParser.Parse(line)).ToArray();

var state = new State
{
    { Color.Red, 12 },
    { Color.Green, 13 },
    { Color.Blue, 14 },
};

var part1Result = games.Where(g => g.IsValidForCubes(state)).Sum(g => g.Id);
Console.WriteLine(part1Result);

var part2Result = games
    .Sum(g=>g.GetMinimumState()
        .Select(x=>x.Value)
        .Aggregate(1, (x, i) => x*i));
Console.WriteLine(part2Result);