using MessagePack;
using UnityEngine;

namespace NetworkUtilities
{
    public enum MessageAction
    {
        SetName = 1,
        StartGame = 2,
        PlayerReady = 3,
    }
    
    [MessagePackObject]
    public class Name_MP
    {
        [Key(0)] public string name;
    }
}
