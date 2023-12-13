using Day12;

var lines = File.ReadAllLines("input.txt");
var lineParser = new ReportParser();


var reports = lines.Select(lineParser.Parse).ToArray();

enum Cell
{
    Operational,
    Damaged,
    Unknown,
}

record Report(Cell[] Springs, int[] Lengths);