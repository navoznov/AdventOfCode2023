namespace Common;

public record Vector(int X, int Y)
{
    public static readonly Vector N = new Vector(0, -1);
    public static readonly Vector S = new Vector(0, 1);
    public static readonly Vector W = new Vector(-1, 0);
    public static readonly Vector E = new Vector(1, 0);

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
    }

    public static Vector operator *(Vector vector, int constant) =>
        new Vector(vector.X * constant, vector.Y * constant);
}