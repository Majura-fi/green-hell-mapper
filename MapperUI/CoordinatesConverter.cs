using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MapperUI;
public static class CoordinatesConverter
{
    private static readonly PointF offset = new(-108f, 5310f);
    private static readonly PointF factor = new(2.53f, -2.74f);

    public static Vector2 GameCoordinatesToMapCoordinates(Vector3 coords)
    {
        return new(
            (coords.X * factor.X) + offset.X,
            (coords.Z * factor.Y) + offset.Y
        );
    }

    public static Vector3 MapCoordinatesToGameCoordinates(Vector2 coords)
    {
        return new(
            (coords.X - offset.X) / factor.X,
            0,
            (coords.Y - offset.Y) / factor.Y
        );
    }

    public static Vector2 GameForwardToMapForward(Vector3 forward)
    {
        return Vector2.Normalize(new(
            forward.X,
            -forward.Z
        ));
    }
}
