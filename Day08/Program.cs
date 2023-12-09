using System.Diagnostics;
using Day08;

var lines = File.ReadAllLines("input.txt");
var inputParser = new InputParser();
var (directions, nodes) = inputParser.Parse(lines);

// Part 1
var startNodeName = "AAA";
var finishNodeName = "ZZZ";
var stepsCountPart1 = GetStepsCount(nodes[startNodeName], s => s == "ZZZ");
Console.WriteLine(stepsCountPart1);
Console.WriteLine();

// Part 2
var startNodes = nodes.Values.Where(n => n.Name.Last() == 'A').ToArray();
var steps = startNodes
    .Select(node => GetStepsCount(node , s => s.Last() == 'Z'))
    .ToArray();
var result2 = FindLeastCommonMultiple(steps);
Console.WriteLine(result2);

long GetStepsCount(Node startNode, Func<string, bool> checkReachingFinish)
{
    var currentNode = startNode;
    var stepsCounter = 0;
    while (true)
    {
        var direction = directions[stepsCounter % directions.Length];
        var nextNodeName = direction == 'L' ? currentNode.Left : currentNode.Right;
    
        currentNode = nodes[nextNodeName];
        stepsCounter++;
        if (checkReachingFinish(currentNode.Name))
        {
            return stepsCounter;
        }
    }
}

long FindLeastCommonMultiple(long[] numbers)
{
    return numbers.Aggregate(LCM2);
}

long GCD2(long a, long b)
{
    return b == 0 ? a : GCD2(b, a % b);
}

long LCM2(long a, long b)
{
    return (a / GCD2(a, b)) * b;
}

record Node(string Name, string Left, string Right);