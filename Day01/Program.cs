var lines = File.ReadAllLines("input.txt");
var digits = Enumerable.Range(0,10).Select(x=> (char)('0' + x)).ToArray();
var sum = lines.Select(GetNumber).Sum();
Console.WriteLine(sum);

int GetNumber(string line)
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
Console.WriteLine("Hello, World!");