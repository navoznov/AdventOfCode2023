namespace Day07;

public class HandCardsByRankComparer : IComparer<char[]>
{
    private readonly IHandStrengthCalculator _handStrengthCalculator;
    private readonly Dictionary<char, int> CardRanks;

    public HandCardsByRankComparer(IHandStrengthCalculator handStrengthCalculator)
    {
        _handStrengthCalculator = handStrengthCalculator;
        CardRanks = GetCardRanks();
    }

    public int Compare(char[]? x, char[]? y)
    {
        if (x == null)
        {
            throw new ArgumentNullException(nameof(x));
        }

        if (y == null)
        {
            throw new ArgumentNullException(nameof(y));
        }

        var xStrength = _handStrengthCalculator.GetHandStrength(x);
        var yStrength = _handStrengthCalculator.GetHandStrength(y);

        if (xStrength < yStrength)
        {
            return -1;
        }

        if (xStrength > yStrength)
        {
            return 1;
        }

        return CompareCardValues(x, y);
    }

    protected virtual Dictionary<char, int> GetCardRanks()
    {
        return new()
        {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 },
        };
    }

    private int CompareCardValues(char[] xCards, char[] yCards)
    {
        if (xCards.Length != yCards.Length)
        {
            throw new ArgumentException($"{nameof(yCards)} count must be equal to ${nameof(xCards)} count.");
        }

        for (var i = 0; i < xCards.Length; i++)
        {
            var xCard = xCards[i];
            var yCard = yCards[i];
            if (xCard == yCard)
            {
                continue;
            }

            return CardRanks[xCard] < CardRanks[yCard] ? -1 : 1;
        }

        return 0;
    }
}