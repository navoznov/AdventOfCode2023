using Common;

namespace Day02;

class GameParser
{
    static Dictionary<string, Color> _colorParseRules = new Dictionary<string, Color>()
    {
        { "blue", Color.Blue },
        { "red", Color.Red },
        { "green", Color.Green },
    };

    public Game Parse(string line)
    {
        var (gameLine, statesLine) = line.Split(": ");
        var gameId = ParseGameId(gameLine);
        var states = ParseStates(statesLine);
        return new Game(gameId, states);
    }

    private int ParseGameId(string gameLine)
    {
        var (_, idStr) = gameLine.Split(' ');
        return int.Parse(idStr);
    }

    private Dictionary<Color, int>[] ParseStates(string statesLine)
    {
        var stateLines = statesLine.Split("; ");

        return stateLines.Select(ParseState).ToArray();
    }

    private Dictionary<Color, int> ParseState(string stateLine)
    {
        var cubeLines = stateLine.Split(", ");
        return cubeLines.Select(ParseCube).ToDictionary(x => x.Color, x => x.Count);
    }

    private (Color Color, int Count) ParseCube(string cubeStr)
    {
        var (countStr, colorStr) = cubeStr.Split(' ');
        return (_colorParseRules[colorStr], int.Parse(countStr));
    }
}