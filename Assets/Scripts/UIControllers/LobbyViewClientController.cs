using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyViewClientController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI secondPlayerName;
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI lobbyIdText;

    void OnEnable()
    {
        readyButton.onClick.AddListener(OnReadyPressed);
    }

    void OnDisable()
    {
        readyButton.onClick.RemoveAllListeners();
    }
    
    void Start()
    {
        lobbyIdText.text = "Lobby ID: " + NetworkData.GetLobbyId();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.NickName != NetworkData.GetName())
            {
                NetworkData.SetPeerName(player.NickName);
                secondPlayerName.text = NetworkData.GetPeerName();
            }
        }
    }

    private void OnReadyPressed()
    {
        photonView.RPC("PlayerReadyRPC", RpcTarget.MasterClient);
    }
}
