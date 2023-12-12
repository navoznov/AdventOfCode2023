namespace Common;

public record Point(int X, int Y)
{
    public static Point operator +(Point point, Vector vector)
    {
        return new Point(point.X + vector.X, point.Y + vector.Y);
    }

    public static Vector operator -(Point point1, Point point2)
    {
        return new Vector(point1.X - point2.X, point1.Y - point2.Y);
    }

    public virtual bool Equals(Point? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public record ValuePoint<T>(T Value, Point Point);