using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Singleton was used in this code snippet


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
       // PauseManager.instance.mainMenu();//Singleton Used here
        SceneManager.LoadScene(0); // Game scene

    }


    // Called by the Quit button
    public void QuitGame()
    {
        //PauseManager.instance.quitGame();//Singleton Used here
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
}
