using UnityEditor;
using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;


    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }


    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        StartCoroutine(PreventImmediateInput());
    }

    private IEnumerator PreventImmediateInput()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    }


    public void quitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif    
    }

    public void mainMenu()
    {
        Time.timeScale = 1.0f; // Ensure time resumes when switching scenes
        isPaused = false; // Reset the pause state
        SceneManager.LoadScene(0); // Load the main menu scene
    }
}
