using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Level1");
    }

    public async void MultiplayerStart()
    {
        SceneManager.LoadScene("Multiplayer_playerName");
    }
}
