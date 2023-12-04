using System.Threading.Channels;
using Day04;

var lines = File.ReadAllLines("input.txt");
var cardParser = new CardParser();
var cards = lines.Select(cardParser.Parse).ToArray();

// Part 1
var part1 = cards.Select(CardHelper.GetCardPoints).Sum();
Console.WriteLine(part1);

// Part 2
var cardCounts = cards.ToDictionary(c=>c.Id, c=> 1);

foreach (var card in cards)
{
    var cardCount = cardCounts[card.Id];
    var matchingNumbersCount = CardHelper.GetMatchingNumbersCount(card);
    var cardIds = Enumerable.Range(card.Id+1, matchingNumbersCount);
    foreach (var cardId in cardIds)
    {
        cardCounts[cardId] += cardCount;
    }
}

var part2 = cardCounts.Sum(x=>x.Value);
Console.WriteLine(part2);

public record Card(int Id, HashSet<int> WinningNumbers, HashSet<int> NumbersYouHave);