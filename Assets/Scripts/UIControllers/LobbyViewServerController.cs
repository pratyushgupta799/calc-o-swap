using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

public class LobbyViewServerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI secondPlayerName;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI lobbyIdText;
    
    void Awake()
    {
        GameEvents.PeerNameReceived += OnPeerNameReceived;
    }
    
    void Start()
    {
        secondPlayerName.text = "";
        startButton.interactable = false;
        lobbyIdText.text = "Lobby ID: " + NetworkData.GetLobbyId();
    }

    void OnPeerNameReceived()
    {
        secondPlayerName.text = NetworkData.GetPeerName();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Someone Joined");
        NetworkData.SetPeerName(newPlayer.NickName);
        OnPeerNameReceived();
    }
}
