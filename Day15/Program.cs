using System.Text.RegularExpressions;

var str = File.ReadAllText("input.txt");
var stepStrs = str.Split(',');

// Part 1
var sum = stepStrs.Sum(GetHash);
Console.WriteLine(sum);

// Part 2
var regex = new Regex(@"^([a-z]+)([=-])(\d?)$");
var boxes = Enumerable.Range(0, 256).Select(_ => new List<Lens>()).ToArray();
foreach (var stepStr in stepStrs)
{
    var match = regex.Match(stepStr);
    var lensName = match.Groups[1].Value;
    var boxIndex = GetHash(lensName);
    var box = boxes[boxIndex];
    var indexOfLens = box.FindIndex(l => l.Name == lensName);

    var stepType = match.Groups[2].Value;
    if (stepType == "=")
    {
        var focalLength = int.Parse(match.Groups[3].Value);
        var lens = new Lens(lensName, focalLength);
        if (indexOfLens == -1)
        {
            box.Add(lens);
        }
        else
        {
            box[indexOfLens] = lens;
        }
    }
    else if (stepType == "-")
    {
        if (indexOfLens != -1)
        {
            box.RemoveAt(indexOfLens);
        }
    }
}

Console.WriteLine(CalcTotalPower(boxes));

int GetHash(string chars)
{
    var seed = 0;
    foreach (var ch in chars)
    {
        seed += ch;
        seed *= 17;
        seed %= 256;
    }

    return seed;
}

int CalcTotalPower(List<Lens>[] boxes)
{
    var totalPower = 0;
    for (var boxIndex = 0; boxIndex < boxes.Length; boxIndex++)
    {
        var box = boxes[boxIndex];
        for (var slotIndex = 0; slotIndex < box.Count; slotIndex++)
        {
            var lens = box[slotIndex];
            totalPower += (boxIndex + 1) * (slotIndex + 1) * lens.FocalLength;
        }
    }

    return totalPower;
}

record Lens(string Name, int FocalLength);