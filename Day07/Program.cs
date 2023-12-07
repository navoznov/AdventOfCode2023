using Common;
using Day07;

var lines = File.ReadAllLines("input.txt");
var hands = lines.Select(Parse).ToArray();
var handBidRanks = hands.OrderBy(h => h.Cards, new HandCardsComparer())
    .Select((h, i) => new { Index = i+1, Bid = h.Bid })
    .ToArray();

var result1 = handBidRanks.Aggregate(0, (sum, hbr) => sum + hbr.Index * hbr.Bid);
Console.WriteLine(result1);

Hand Parse(string str)
{
    var (cardsStr, bidStr) = str.Split(' ');
    return new Hand(cardsStr.ToCharArray(), int.Parse(bidStr));
}

record Hand(char[] Cards, int Bid);

