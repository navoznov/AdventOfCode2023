var lines = File.ReadAllLines("input.txt");

var tenDigitsRange = Enumerable.Range(0, 10);
var digits = tenDigitsRange.Select(x => (char)('0' + x)).ToArray();

var simpleSum = lines.Select(GetNumberSimple).Sum();
Console.WriteLine(simpleSum);

var wordDigits = new Dictionary<string, int>()
{
    { "zero", 0 },
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 },
};

var advancedSum = lines.Select(GetNumberAdvanced).Sum();
Console.WriteLine(advancedSum);

int GetNumberAdvanced(string line)
{
    int GetLeftNumber()
    {
        var lineSpan = line.AsSpan();
        for (var i = 0; i < line.Length; i++)
        {
            var currentSpan = lineSpan[i..];
            for (var j = 0; j < digits.Length; j++)
            {
                if (currentSpan[0] == digits[j])
                {
                    return j;
                }
            }

            foreach (var wordPair in wordDigits)
            {
                if (currentSpan.StartsWith(wordPair.Key))
                {
                    return wordPair.Value;
                }
            }
        }

        throw new Exception("No digits were found");
    }

    int GetRightNumber()
    {
        var lineSpan = line.AsSpan();
        for (var i = line.Length - 1; i >= 0; i--)
        {
            var currentSpan = lineSpan[i..];
            for (var j = 0; j < digits.Length; j++)
            {
                if (currentSpan[0] == digits[j])
                {
                    return j;
                }
            }

            foreach (var wordPair in wordDigits)
            {
                if (currentSpan.StartsWith(wordPair.Key))
                {
                    return wordPair.Value;
                }
            }
        }

        throw new Exception("No digits were found");
    }


    return GetLeftNumber() * 10 + GetRightNumber();
}

int GetNumberSimple(string line)
{
    var result = 0;
    var chars = line.ToCharArray();

    foreach (var t in chars)
    {
        if (digits.Contains(t))
        {
            result = 10 * (t - '0');
            break;
        }
    }

    for (var i = chars.Length - 1; i >= 0; i--)
    {
        if (!digits.Contains(chars[i]))
        {
            continue;
        }

        result += chars[i] - '0';
        break;
    }

    return result;
}
