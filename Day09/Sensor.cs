namespace Day09;

class Sensor
{
    public List<int> GetExtrapolatedSequence(List<int> sequence)
    {
        var sequences = GetDiffSequences(sequence);

        sequences.Last().Add(0);

        for (var i = sequences.Count - 1; i >= 1; i--)
        {
            var value = sequences[i].Last() + sequences[i - 1].Last();
            sequences[i - 1].Add(value);
        }

        return sequences[0];
    }

    public List<int> GetBackwardExtrapolatedSequence(List<int> sequence)
    {
        var sequences = GetDiffSequences(sequence);

        sequences.Last().Insert(0, 0);

        for (var i = sequences.Count - 1; i >= 1; i--)
        {
            var value = sequences[i - 1].First() - sequences[i].First();
            sequences[i - 1].Insert(0, value);
        }

        return sequences[0];
    }

    private List<List<int>> GetDiffSequences(List<int> sequence)
    {
        var sequences = new List<List<int>> { sequence };
        var currentSequence = sequence;
        while (true)
        {
            var diffs = GetDiffs(currentSequence);
            sequences.Add(diffs);
            currentSequence = diffs;

            if (diffs.All(x => x == 0))
            {
                break;
            }
        }

        return sequences;
    }


    List<int> GetDiffs(List<int> sequence)
    {
        var diffs = new List<int>(sequence.Count - 1);
        for (var i = 1; i < sequence.Count; i++)
        {
            diffs.Add(sequence[i] - sequence[i - 1]);
        }

        return diffs;
    }
}