using Day14;

var lines = File.ReadAllLines("input.txt");

var platform = PlatformParser.Parse(lines);
var tiltedPlatform = PlatformTilter.TiltPlatform(platform);

// Func<Cell, string> printer = cell => cell == Cell.Empty ? "." : (cell == Cell.Cube ? "#" : "O");
// Console.WriteLine(tiltedPlatform.ToString(printer));

var ySize = tiltedPlatform.YSize;
var sum = tiltedPlatform.GetAllItems()
    .Where(i => i.Value == Cell.Round)
    .Sum(i => ySize - i.Point.Y);
Console.WriteLine(sum);

enum Cell
{
    Empty,
    Round,
    Cube,
}