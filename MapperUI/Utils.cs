using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperUI;
public static class Utils
{
    public static Point Subtract(Point a, Point b)
    {
        return new(a.X - b.X, a.Y - b.Y);
    }

    public static Point Addition(Point a, Point b)
    {
        return new(a.X + b.X, a.Y + b.Y);
    }

    public static SizeF Scale(SizeF size, float value)
    {
        return new(size.Width * value, size.Height * value);
    }

    public static Point Scale(Point a, float scale)
    {
        return new((int)(a.X * scale), (int)(a.Y * scale));
    }
}
