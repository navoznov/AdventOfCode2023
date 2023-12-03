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

foreach (var game in games)
{
    if (game.IsValidForCubes(state))
    {
        
    }
    else
    {
        
    }
}
var validGames = games.Where(g => g.IsValidForCubes(state)).ToList();
var part1result = validGames.Sum(g => g.Id);
Console.WriteLine(part1result);
return;