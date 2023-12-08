using Day08;

var lines = File.ReadAllLines("input.txt");
var inputParser = new InputParser();
var (directions, nodes) = inputParser.Parse(lines);

var startNodeName = "AAA";
var finishNodeName = "ZZZ";

var currentNode = nodes[startNodeName];
var stepsCounter = 0;
while (true)
{
    var direction = directions[stepsCounter % directions.Length];
    stepsCounter++;
    
    var nextNodeName = direction == 'L' ? currentNode.Left : currentNode.Right;
    if (nextNodeName == finishNodeName)
    {
        break;
    }
    
    currentNode = nodes[nextNodeName];
}

Console.WriteLine(stepsCounter);

record Node(string Name, string Left, string Right);