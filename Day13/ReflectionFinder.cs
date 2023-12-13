using Common;

namespace Day13;

class ReflectionFinder
{
    private static Func<Cell, string> printer = cell => cell == Cell.Ash ? "." : "#";

    public (bool Result, int Index) TryGetVerticalLineReflectionIndex(Map2d<Cell> map)
    {
        for (var x = 0; x < map.XSize - 1; x++)
        {
            if (CheckIsVerticalLineReflection(x, map))
            {
                return (true, x);
            }
        }

        return (false, default);
    }

    public (bool Result, int Index) TryGetHorizontalLineReflectionIndex(Map2d<Cell> map)
    {
        for (var y = 0; y < map.YSize - 1; y++)
        {
            if (CheckIsHorizontalLineReflection(y, map))
            {
                return (true, y);
            }
        }

        return (false, default);
    }

    bool CheckIsVerticalLineReflection(int x, Map2d<Cell> map)
    {
        for (var offset = 0;; offset++)
        {
            var x1 = x - offset;
            var x2 = x + 1 + offset;
            if (offset > 0 && (x1 < 0 || x2 >= map.XSize))
            {
                break;
            }

            if (!CheckTwoColumnsAreTheSame(x1, x2, map))
            {
                return false;
            }
        }

        return true;
    }

    bool CheckIsHorizontalLineReflection(int y, Map2d<Cell> map)
    {
        for (var offset = 0;; offset++)
        {
            var y1 = y - offset;
            var y2 = y + 1 + offset;
            if (offset > 0 && (y1 < 0 || y2 >= map.YSize))
            {
                break;
            }

            if (!CheckTwoRowsAreTheSame(y1, y2, map))
            {
                return false;
            }
        }

        return true;
    }

    bool CheckTwoColumnsAreTheSame(int x1, int x2, Map2d<Cell> map)
    {
        for (var y = 0; y < map.YSize; y++)
        {
            if (map[x1, y] != map[x2, y])
            {
                return false;
            }
        }

        return true;
    }

    bool CheckTwoRowsAreTheSame(int y1, int y2, Map2d<Cell> map)
    {
        for (var x = 0; x < map.XSize; x++)
        {
            if (map[x, y1] != map[x, y2])
            {
                return false;
            }
        }

        return true;
    }
}