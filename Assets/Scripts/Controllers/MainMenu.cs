using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Change to your game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Game Exited!");
        Application.Quit();
    }
}