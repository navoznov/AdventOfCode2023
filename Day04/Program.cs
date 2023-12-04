using System.Threading.Channels;
using Day04;

var lines = File.ReadAllLines("input.txt");
var cardParser = new CardParser();
var cards = lines.Select(cardParser.Parse).ToArray();

var game = new Game();
var part1 = cards.Select(game.GetCardPoints).Sum();
Console.WriteLine(part1);

public record Card(int Id, HashSet<int> WinningNumbers, HashSet<int> NumbersYouHave);