using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyIdUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyIDText;
    [SerializeField] private Button joinButton;
    
    private void OnEnable()
    {
        joinButton.onClick.AddListener(OnJoinButtonClicked);
    }
    
    private void OnDisable()
    {
        joinButton.onClick.RemoveAllListeners();
    }
    
    private void OnJoinButtonClicked()
    {
        string lobbyId = lobbyIDText.text;
        lobbyId = lobbyId
            .Replace("\u200B", "")
            .Replace("\u200C", "")
            .Replace("\u200D", "")
            .Replace("\uFEFF", "");
        
        MultiplayerHandler.Instance.SearchLobby(lobbyId.ToUpper());
    }
}
