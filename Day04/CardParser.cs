using Common;

namespace Day04;

public class CardParser
{
    public Card Parse(string line)
    {
        var (idStr, numbersStr) = line.Split(": ");
        var id = int.Parse(idStr.Split(" ").Last());
        var (winningNumbers, numbersYouHave) = numbersStr.Split(" | ").Select(ParseNumbers).ToArray();
        return new Card(id, winningNumbers, numbersYouHave);
    }

    static HashSet<int> ParseNumbers(string str)
    {
        return str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
    }
}