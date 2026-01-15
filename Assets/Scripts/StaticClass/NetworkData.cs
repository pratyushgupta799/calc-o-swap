using System.Net;
using Unity.Services.Relay.Models;
using UnityEngine;

public static class NetworkData
{
    private static string name;
    private static string peerName;
    
    private static bool isHost = false;
    private static bool isConnected = false;
    
    private static IPEndPoint peer_ep;
    private static IPEndPoint player_ep;

    private static Allocation alloc;

    private static string lobbyId;

    public static void SetName(string playerName)
    {
        name = playerName;
        Debug.Log("Player name set to " + name);
    }
    
    public static string GetName()
    {
        return name;
    }

    public static void SetAsHost()
    {
        isHost = true;
    }
    
    public static bool IsHost()
    {
        return isHost;
    }

    public static void SetEndPoint(IPEndPoint ep)
    {
        player_ep = ep;
        Debug.Log("Player end point set to " + player_ep);
    }

    public static IPEndPoint GetEndPoint()
    {
        return player_ep;
    }
    
    public static void SetPeerEndPoint(IPEndPoint ep)
    {
        peer_ep = ep;
        Debug.Log("Peer end point set to " + peer_ep);
    }

    public static void SetLobbyId(string lobby_id)
    {
        lobbyId = lobby_id;
        Debug.Log("Lobby id set to " + lobbyId);
    }

    public static string GetLobbyId()
    {
        return lobbyId.ToString();
    }

    public static void SetPeerName(string name)
    {
        peerName = name;
    }
    
    public static string GetPeerName()
    {
        return peerName;
    }

    public static void SetAllocation(Allocation allocation)
    {
        alloc = allocation;
    }
    
    public static Allocation GetAllocation()
    {
        return alloc;
    }
}
