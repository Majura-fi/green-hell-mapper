using UnityEngine;

namespace MapperSource;

public record PlayerInfo
{
    public int PlayerId { get; }
    public Vector3 Location { get; }

    public PlayerInfo(int playerId, Vector3 location)
    {
        PlayerId = playerId;
        Location = location;
    }

    public string ToJSON()
    {
        return $"{{ \"PlayerId\": {PlayerId}, \"Location\": {{ \"X\": {Location.x}, \"Y\": {Location.y}, \"Z\": {Location.z} }} }}";
    }
}