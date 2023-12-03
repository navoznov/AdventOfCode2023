namespace Day02;

internal record Game(int Id, Dictionary<Color, int>[] States)
{
    public bool IsValidForCubes(Dictionary<Color, int> allCubes)
    {
        return States.All(state => CheckIsValidState(state, allCubes));
    }

    private bool CheckIsValidState(Dictionary<Color, int> state, Dictionary<Color, int> allCubes)
    {
        return state.All(ci => ci.Value <= allCubes[ci.Key]);
    }

    public Dictionary<Color, int> GetMinimumState()
    {
        return Enum.GetValues<Color>()
            .Select(color => new { Color = color, Count = GetMaxCount(color) })
            .ToDictionary(x => x.Color, x => x.Count);
    }

    private int GetMaxCount(Color color)
    {
        return States.Max(state => state.TryGetValue(color, out var value) ? value : 0);
    }
}