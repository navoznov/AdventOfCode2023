using Day03;

var lines = File.ReadAllLines("input.txt");
var engine = new Engine(lines);
var details = engine.GetDetails();

var part1Result = details.Sum(x => int.Parse(x.Number));
Console.WriteLine(part1Result);

var part2Result = engine.GetGearRatios().Sum();
Console.WriteLine(part2Result);