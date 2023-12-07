namespace Day07;

public class HandCardsByRankWithJokerComparer : HandCardsByRankComparer
{
    protected override Dictionary<char, int> GetCardRanks()
    {
        return new()
        {
            { 'J', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 },
        };
    }

    public HandCardsByRankWithJokerComparer(IHandStrengthCalculator handStrengthCalculator) : base(
        handStrengthCalculator)
    {
    }
}