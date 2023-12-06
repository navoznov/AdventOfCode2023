using Day06;

var text = File.ReadAllText("input.txt");

// Part 1
Console.WriteLine(GetResult(text, new RacesParser()));

// Part 2
Console.WriteLine(GetResult(text, new RaceAdvancedParser()));

long GetResult(string input, RacesParser racesParser)
{
    var races = racesParser.Parse(input);
    var winWayCounts = races.Select(GetWinWayCount).ToArray();
    return winWayCounts.Aggregate(1L, (a, b) => a * b);
}

long GetWinWayCount(Race race)
{
    var winWayCounter = 0;
    var raceTime = race.Time;
    for (var holdButtonTime = 1; holdButtonTime < raceTime; holdButtonTime++)
    {
        var speed = holdButtonTime;
        var distance = speed * (raceTime - holdButtonTime);
        if (distance > race.Distance)
        {
            winWayCounter++;
        }
    }

    return winWayCounter;
}

record Race(long Time, long Distance);