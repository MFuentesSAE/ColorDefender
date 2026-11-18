using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string GAME_SCENE = "Scene_Game";

    private void Start()
    {
        AdManager.instance.ShowAd();
    }

    public void StartGame()
    {
        Monou.GameScraper.Starts();
        SceneManager.LoadScene(GAME_SCENE);
    }
}
