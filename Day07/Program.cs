using Common;
using Day07;
using FluentAssertions;

var lines = File.ReadAllLines("input.txt");
var hands = lines.Select(Parse).ToArray();

// Part 1
var simpleComparer = new HandCardsByRankComparer(new HandStrengthCalculator());
var result1 = CalcResult(hands, simpleComparer);
Console.WriteLine(result1);


// Part 2
var handWithJokerStrengthCalculator = new HandWithJokerStrengthCalculator();
TestHandWithJokerStrengthCalculator();

var jokerComparer = new HandCardsByRankWithJokerComparer(handWithJokerStrengthCalculator);
var result2 = CalcResult(hands, jokerComparer);
Console.WriteLine(result2);

Hand Parse(string str)
{
    var (cardsStr, bidStr) = str.Split(' ');
    return new Hand(cardsStr.ToCharArray(), int.Parse(bidStr));
}

int CalcResult(Hand[]? hands, HandCardsByRankComparer handCardsComparer)
{
    var handBidRanks = hands.OrderBy(h => h.Cards, handCardsComparer)
        .Select((h, i) => new { Index = i + 1, Bid = h.Bid })
        .ToArray();
    return handBidRanks.Aggregate(0, (sum, hbr) => sum + hbr.Index * hbr.Bid);
}

void TestHandWithJokerStrengthCalculator()
{
    handWithJokerStrengthCalculator.GetHandStrength("32T3K".ToCharArray()).Should().Be(1);
    handWithJokerStrengthCalculator.GetHandStrength("32TJK".ToCharArray()).Should().Be(1);

    handWithJokerStrengthCalculator.GetHandStrength("662TT".ToCharArray()).Should().Be(2);

    handWithJokerStrengthCalculator.GetHandStrength("6626T".ToCharArray()).Should().Be(3);
    handWithJokerStrengthCalculator.GetHandStrength("66JT3".ToCharArray()).Should().Be(3);

    handWithJokerStrengthCalculator.GetHandStrength("66JTT".ToCharArray()).Should().Be(4);
    handWithJokerStrengthCalculator.GetHandStrength("66TTT".ToCharArray()).Should().Be(4);

    handWithJokerStrengthCalculator.GetHandStrength("66T66".ToCharArray()).Should().Be(5);
    handWithJokerStrengthCalculator.GetHandStrength("66TJ6".ToCharArray()).Should().Be(5);

    handWithJokerStrengthCalculator.GetHandStrength("66666".ToCharArray()).Should().Be(6);
    handWithJokerStrengthCalculator.GetHandStrength("6666J".ToCharArray()).Should().Be(6);
}

record Hand(char[] Cards, int Bid);