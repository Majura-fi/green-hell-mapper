using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MapperUI;
public static class MathUtils
{
    public static Quaternion Euler(float pitch, float yaw, float roll)
    {
        pitch = Deg2Rad(pitch);
        yaw = Deg2Rad(yaw);
        roll = Deg2Rad(roll);

        float sinPitch = MathF.Sin(pitch * 0.5f);
        float cosPitch = MathF.Cos(pitch * 0.5f);
        float sinYaw = MathF.Sin(yaw * 0.5f);
        float cosYaw = MathF.Cos(yaw * 0.5f);
        float sinRoll = MathF.Sin(roll * 0.5f);
        float cosRoll = MathF.Cos(roll * 0.5f);

        float x = cosYaw * sinPitch * cosRoll + sinYaw * cosPitch * sinRoll;
        float y = sinYaw * cosPitch * cosRoll - cosYaw * sinPitch * sinRoll;
        float z = cosYaw * cosPitch * sinRoll - sinYaw * sinPitch * cosRoll;
        float w = cosYaw * cosPitch * cosRoll + sinYaw * sinPitch * sinRoll;

        return new Quaternion(x, y, z, w);
    }

    public static Vector3 Multiply(Quaternion rotation, Vector3 point)
    {
        float x = rotation.X * 2.0f;
        float y = rotation.Y * 2.0f;
        float z = rotation.Z * 2.0f;
        float xx = rotation.X * x;
        float yy = rotation.Y * y;
        float zz = rotation.Z * z;
        float xy = rotation.X * y;
        float xz = rotation.X * z;
        float yz = rotation.Y * z;
        float wx = rotation.W * x;
        float wy = rotation.W * y;
        float wz = rotation.W * z;

        return new Vector3(
            (1.0f - (yy + zz)) * point.X + (xy - wz) * point.Y + (xz + wy) * point.Z,
            (xy + wz) * point.X + (1.0f - (xx + zz)) * point.Y + (yz - wx) * point.Z,
            (xz - wy) * point.X + (yz + wx) * point.Y + (1.0f - (xx + yy)) * point.Z
        );
    }

    public static float Deg2Rad(float v)
    {
        return v * (MathF.PI / 180f);
    }

    public static Vector2 MoveAlongAngle(Vector2 location, float angleRad, float length)
    {
        Vector2 dir = new(
            MathF.Sin(angleRad),
            MathF.Cos(angleRad)
        );
        return location + dir * length;
    }
}
