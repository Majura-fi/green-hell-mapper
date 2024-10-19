using System.Text;
using UnityEngine;

namespace MapperSource;

public record PlayerInfo
{
    public int PlayerId { get; }
    public Vector3 Location { get; }
    public Vector3 Forward { get; }

    public PlayerInfo(int playerId, Vector3 location, Vector3 forward)
    {
        PlayerId = playerId;
        Location = location;
        Forward = forward;
    }

    public string ToJSON()
    {
        return $"{{ \"PlayerId\": {PlayerId}, \"Location\": {{ \"X\": {Location.x}, \"Y\": {Location.y}, \"Z\": {Location.z} }}, \"Forward\": {{ \"X\": {Forward.x}, \"Y\": {Forward.y}, \"Z\": {Forward.z} }} }}";
    }
}