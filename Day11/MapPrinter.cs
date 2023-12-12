namespace Day11;

class MapPrinter
{
    public string Print(Cell cell)
    {
        return cell switch
        {
            Cell.Space => ".",
            Cell.Galaxy => "#",
            _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
        };
    }
}