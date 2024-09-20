using System.Numerics;

namespace MapperUI;
public record PlayerInfo
{
    public int PlayerId { get; }
    public Vector3 Location { get; }

    public PlayerInfo(int playerId, Vector3 location)
    {
        PlayerId = playerId;
        Location = location;
    }

    public Vector2 ToMapCoordinates()
    {
        return new(
            Location.X * 2.53f - 111f,
            Location.Z * 2.74f - 1339f
        );
    }
}
