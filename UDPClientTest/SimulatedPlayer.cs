using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UDPClientTest;
public class SimulatedPlayer
{
    public int PlayerId { get; set; }
    public Vector3 Location { get; set; }
    public Vector3 Forward { get; set; }

    private readonly static Random rnd = new();
    private Vector3 nextLocation = new(494f, 113f, 1409f);
    private Vector3 nextForward;
    private Vector3 previousLocation;

    public SimulatedPlayer(int playerId) 
    {
        PlayerId = playerId;
        Location = new(494f, 113f, 1409f);
        Forward = new();
        SetupNextLocation();
    }

    public void Update()
    {
        Location = MoveTowards(Location, nextLocation, 5f);
        Forward = MoveTowards(Forward, nextForward, 5f);

        if (Vector3.Distance(Location, nextLocation) <= 100f)
        {
            SetupNextLocation();
        }

        if (MathF.Abs(Forward.Y - nextForward.Y) <= 5f)
        {
            nextForward = new(0, Random(-180, 180), 0);
        }
    }

    private static Vector3 MoveTowards(Vector3 from, Vector3 to, float distance)
    {
        Vector3 v = to - from;

        if (v.Length() == 0)
        {
            return Vector3.Zero;
        }

        Vector3 direction = Vector3.Normalize(v);
        return from + direction * distance;
    }

    private void SetupNextLocation()
    {
        previousLocation = Location;
        nextLocation = Vector3.Add(nextLocation, new(Random(-100f, 100f), 0, Random(-100f, 100f)));

        nextLocation.X = nextLocation.X < 394f ? 394f : nextLocation.X;
        nextLocation.Z = nextLocation.Z < 1309f ? 1309f : nextLocation.Z;

        nextLocation.X = nextLocation.X > 594f ? 594f : nextLocation.X;
        nextLocation.Z = nextLocation.Z > 1509f ? 1509f : nextLocation.Z;
    }

    public string ToJSON()
    {
        return $"{{ \"PlayerId\": {PlayerId}, " +
            $"\"Location\": {{ \"X\": {Location.X.ToString(CultureInfo.InvariantCulture)}, \"Y\": {Location.Y.ToString(CultureInfo.InvariantCulture)}, \"Z\": {Location.Z.ToString(CultureInfo.InvariantCulture)} }}, " +
            $"\"Forward\": {{ \"X\": {Forward.X.ToString(CultureInfo.InvariantCulture)}, \"Y\": {Forward.Y.ToString(CultureInfo.InvariantCulture)}, \"Z\": {Forward.Z.ToString(CultureInfo.InvariantCulture)} }} }}";
    }

    private static float Random(float min, float max)
    {
        return rnd.NextSingle() * (max - min) + min;
    }
}
