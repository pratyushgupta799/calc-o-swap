using UnityEngine;
using UnityEngine.UI;

public class LobbyStartUIController : MonoBehaviour
{
    [SerializeField] private Button createLobby;
    [SerializeField] private Button joinLobby;
    
    private void OnEnable()
    {
        createLobby.onClick.AddListener(OnCreateLobbyClicked);
        joinLobby.onClick.AddListener(OnJoinLobbyClicked);
    }

    private void OnDisable()
    {
        createLobby.onClick.RemoveAllListeners();
        joinLobby.onClick.RemoveAllListeners();
    }

    private void OnCreateLobbyClicked()
    {
        MultiplayerHandler.Instance.CreateLobby();
    }

    private void OnJoinLobbyClicked()
    {
        MultiplayerHandler.Instance.JoinLobby();
    }
}
