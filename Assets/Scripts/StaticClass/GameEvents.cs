using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action PeerNameReceived;

    public static void RaisePeerName()
    {
        PeerNameReceived?.Invoke();
    }
}
