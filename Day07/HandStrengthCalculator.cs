namespace Day07;

public static class HandStrengthCalculator
{
    public static int GetHandStrength(IEnumerable<char> cards)
    {
        var sameCardCounts = cards.GroupBy(c => c)
            .Select(g => g.Count())
            .OrderDescending()
            .ToArray();

        return sameCardCounts switch
        {
            [5] => 6,
            [4, 1] => 5,
            [3, 2] => 4,
            [3, 1, 1] => 3,
            [2, 2, 1] => 2,
            [2, 1, 1, 1] => 1,
            [1, 1, 1, 1, 1] => 0,
            _ => throw new ArgumentOutOfRangeException($"Uknown combination {sameCardCounts}"),
        };
    }
}