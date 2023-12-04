namespace Day04;

internal static class CardHelper
{
    public static int GetCardPoints(Card card)
    {
        var yourWinnerCardsCount = GetMatchingNumbersCount(card);
        return yourWinnerCardsCount == 0 ? 0 : 1 << (yourWinnerCardsCount - 1);
    }

    public static int GetMatchingNumbersCount(Card card)
    {
        return card.WinningNumbers.Intersect(card.NumbersYouHave).Count();
    }
}