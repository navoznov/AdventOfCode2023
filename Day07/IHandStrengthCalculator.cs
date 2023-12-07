namespace Day07;

public interface IHandStrengthCalculator
{
    int GetHandStrength(IEnumerable<char> cards);
}