using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance; // Singleton instance -> Used in GameOverMenu.cs and ChangeScene.cs

    public GameObject pauseMenu;
    public static bool isPaused;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); // Prevent duplicates
    }
    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("PauseManager ERROR: pauseMenu is not assigned!");
        }

        if (isPaused)
        {
            SoundFXManager.Instance.PlaySound("Buttonclick"); // Play sound when pausing
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundFXManager.Instance.PlaySound("Buttonclick"); // Play sound when pausing/unpausing
            if (isPaused)
                resumeGame();
            else
                pauseGame();
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

    public void loadHelpFromPause()
    {   
        SoundFXManager.Instance.PlaySound("Buttonclick"); // Play sound when loading help menu
        HelpMenuTracker.source = "pause";
        Time.timeScale = 1.0f;
        isPaused = false;
        SceneManager.LoadScene(2); // Load Help scene directly without hiding pauseMenu first
    }

    public void mainMenu()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
} // End PauseManager.cs
