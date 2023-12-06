using Day06;

var text = File.ReadAllText("input.txt");
var races = RacesParser.Parse(text);
var winWayCounts = races.Select(GetWinWayCount).ToArray();
var part1Result = winWayCounts.Aggregate(1, (a,b) => a*b);
Console.WriteLine(part1Result);


int GetWinWayCount(Race race)
{
    int winWayCounter = 0;
    var raceTime = race.Time;
    var raceDistance = race.Distance;
    
    for (int holdButtonTime = 1; holdButtonTime < raceTime; holdButtonTime++)
    {
        var speed = holdButtonTime;
        var distance = speed * (raceTime - holdButtonTime);
        if (distance> raceDistance)
        {
            winWayCounter++;
        }
    }

    return winWayCounter;
}


record Race(int Time, int Distance);