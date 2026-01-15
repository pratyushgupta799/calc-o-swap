using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Networking.Transport;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MultiplayerHandler : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public struct AddResponse
    {
        public int id;
    }
    
    [System.Serializable]
    public struct JoinResponse
    {
        public string ip;
        public int port;
    }
    
    [System.Serializable]
    public class WsMessage
    {
        public string type;
        public string from;
        public Data data;

        [System.Serializable]
        public class Data
        {
            public Position position;
            public string level;
            public string name;
        }

        [System.Serializable]
        public class Position
        {
            public float x, y, z;
        }
    }
    
    public static MultiplayerHandler Instance;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Photon Lobby");
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList) continue;

            Debug.Log(
                $"Room: {room.Name} | Players: {room.PlayerCount}/{room.MaxPlayers}"
            );
        }
    }

    public void SetPlayerName()
    {
        var textField = GameObject.FindWithTag("PlayerName").GetComponent<TextMeshProUGUI>();
        if (textField.text != "")
        {
            NetworkData.SetName(textField.text);
            SceneManager.LoadScene("Multiplayer_lobbyStart");
        }
    }

    public void CreateLobby()
    {
        NetworkData.SetAsHost();
        
        PhotonNetwork.NickName = NetworkData.GetName();
        string code = Random.Range(1000, 9999).ToString();
        code = code
            .Replace("\u200B", "")
            .Replace("\u200C", "")
            .Replace("\u200D", "")
            .Replace("\uFEFF", "");
        
        NetworkData.SetLobbyId(code);
        
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        
        PhotonNetwork.CreateRoom(code, options);
    }

    public void JoinLobby()
    {
        PhotonNetwork.NickName = NetworkData.GetName();
        SceneManager.LoadScene("Multiplayer_lobbyID");
    }
    
    public void SearchLobby(string lobbyID)
    {
        NetworkData.SetLobbyId(lobbyID);
        
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(NetworkData.GetLobbyId());
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        if(NetworkData.IsHost())
            SceneManager.LoadScene("Multiplayer_lobbyViewServer");
        else
            SceneManager.LoadScene("Multiplayer_lobbyViewClient");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Joined Room Failed: " + message);
    }
    
    public void HandleMessage(string json)
    {
        var msg = JsonUtility.FromJson<WsMessage>(json);
        if (msg == null) return;

        switch (msg.type)
        {
            case "update":
                Debug.Log("Message received from: " + msg.from);
                break;
        }
    }
}
