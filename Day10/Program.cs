using Day10;

var lines = File.ReadAllLines("input.txt");

var (map, startPoint) = new MapParser().Parse(lines);
var pathFinder = new PathFinder();
var points = pathFinder.GetLoopPath(map, startPoint);

// Part 1
Console.WriteLine(points.Length / 2);

// Part 2
var innerPoints = pathFinder.GetInnerPoints(points);
Console.WriteLine(innerPoints.Length);