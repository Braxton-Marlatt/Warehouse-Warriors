using UnityEngine;
using UnityEngine.SceneManagement;

public class helpMenuManager : MonoBehaviour
{
    public void returnFromHelp()
    {
        if (HelpMenuTracker.source == "pause")
        {
            SceneManager.LoadScene(1); // TestScene
            PauseManager.isPaused = true; // Pause again on return
        }
        else
        {
            SceneManager.LoadScene(0); // Start_Menu
        }
    }
}
