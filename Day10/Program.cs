using Day10;

var lines = File.ReadAllLines("input.txt");

var (map, startPoint) = new MapParser().Parse(lines);
var loopStepsCount = new PathFinder().GetLoopStepsCount(map, startPoint);
Console.WriteLine(loopStepsCount / 2);

enum Cell
{
    Empty,
    Start,
    WE,
    NS,
    SW,
    NW,
    SE,
    NE,
}