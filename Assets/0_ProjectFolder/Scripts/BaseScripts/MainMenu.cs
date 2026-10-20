using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string GAME_SCENE = "Scene_Game";

    public void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }
}
