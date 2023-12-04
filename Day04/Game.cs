namespace Day04;

internal class Game
{
    public int GetCardPoints(Card card)
    {
        var yourWinnerCardCount = card.WinningNumbers.Intersect(card.NumbersYouHave).Count();
        return yourWinnerCardCount == 0 ? 0 : 1 << (yourWinnerCardCount - 1);
    }
}