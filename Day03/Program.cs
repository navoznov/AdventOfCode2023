﻿var lines = File.ReadAllLines("input.txt");
var engine = new Engine(lines);
var details = engine.GetDetails();

var part1Result = details.Sum(x => int.Parse(x.Number));
Console.WriteLine(part1Result);
