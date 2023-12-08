using System.Text.RegularExpressions;

namespace Day08;

class InputParser
{
    private static readonly Regex NodeParsingRegex = new(@"^(?'name'[A-Z]{3}) = \((?'left'[A-Z]{3}), (?'right'[A-Z]{3})\)$");

    public (string directions, Dictionary<string, Node> nodes) Parse(string[] lines)
    {
        var directions = lines.First();
        var nodes = lines.Skip(2)
            .Select(ParseNodeLine)
            .ToDictionary(x => x.Name, x => x);
        return (directions, nodes);
    }

    private Node ParseNodeLine(string str)
    {
        var match = NodeParsingRegex.Match(str);
        var name = match.Groups["name"].Value;
        var left = match.Groups["left"].Value;
        var right = match.Groups["right"].Value;
        return new Node(name, left, right);
    }
}