namespace Common;

public static class ArrayExtensions
{
    public static void Deconstruct<T>(this T[] parts, out T part0, out T part1)
    {
        part0 = parts[0];
        part1 = parts[1];
    }

    public static void Deconstruct<T>(this T[] parts, out T part0, out T part1, out T part2)
    {
        part0 = parts[0];
        part1 = parts[1];
        part2 = parts[2];
    }
}