using Day02;

using State = System.Collections.Generic.Dictionary<Color, int>;

var lines = File.ReadAllLines("input.txt");
var gameParser = new GameParser();
var games = lines.Select(line => gameParser.Parse(line));

var state = new State
{
    { Color.Red, 12 },
    { Color.Green, 13 },
    { Color.Blue, 14 },
};

var part1result = games.Where(g => g.IsValidForCubes(state)).Sum(g => g.Id);
Console.WriteLine(part1result);
return;