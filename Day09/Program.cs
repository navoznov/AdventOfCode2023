using Day09;

var lines = File.ReadAllLines("input.txt");
var sequences = lines.Select(l => l.Split(' ').Select(int.Parse).ToList()).ToArray();

var sensor = new Sensor();
var result1 = sequences.Sum(x => sensor.GetExtrapolatedSequence(x).Last());
Console.WriteLine(result1);

var result2 = sequences.Sum(x=>sensor.GetBackwardExtrapolatedSequence(x).First());
Console.WriteLine(result2);