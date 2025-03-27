using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Called by the Restart button
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1); // Game scene
    }

    // Called by the Main Menu button
    public void LoadMainMenu()
    {
        PauseManager.instance.mainMenu();
    }


    // Called by the Quit button
    public void QuitGame()
    {
        PauseManager.instance.quitGame();
    }
}
