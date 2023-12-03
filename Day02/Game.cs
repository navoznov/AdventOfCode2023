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
}