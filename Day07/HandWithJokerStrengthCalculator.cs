namespace Day07;

public class HandWithJokerStrengthCalculator : IHandStrengthCalculator
{
    public int GetHandStrength(IEnumerable<char> cards)
    {
        var cardsArray = cards as char[] ?? cards.ToArray();
        var jokerCount = cardsArray.Count(c => c == 'J');

        var sameCardCounts = cardsArray
            .Where(c => c != 'J')
            .GroupBy(c => c)
            .Select(g => g.Count())
            .OrderDescending()
            .ToArray();

        if (sameCardCounts.Any())
        {
            sameCardCounts[0] += jokerCount;
        }
        else
        {
            sameCardCounts = new[] { jokerCount };
        }

        return sameCardCounts switch
        {
            [5] => 6,
            [4, 1] => 5,
            [3, 2] => 4,
            [3, 1, 1] => 3,
            [2, 2, 1] => 2,
            [2, 1, 1, 1] => 1,
            [1, 1, 1, 1, 1] => 0,
            _ => throw new ArgumentOutOfRangeException($"Uknown combination {sameCardCounts}")
        };
    }
}