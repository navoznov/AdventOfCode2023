namespace Common;

public record Vector(int X, int Y)
{
    public static Vector operator +(Vector vector1, Vector vector2)
    {
        return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
    }
    
};