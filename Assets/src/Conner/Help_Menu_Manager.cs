using UnityEngine;
using UnityEngine.SceneManagement;

public class helpMenuManager : MonoBehaviour
{
    public void returnFromHelp()
    {
        if (HelpMenuTracker.source == "pause")
        {
            SoundEffectManager.Instance.PlaySound("buttonClick", SoundEffectManager.Instance.audioSources);
            SceneManager.LoadScene(1); // TestScene
            PauseManager.isPaused = true; // Pause again on return
        }
        else
        {
            SoundEffectManager.Instance.PlaySound("buttonClick", SoundEffectManager.Instance.audioSources);
            SceneManager.LoadScene(0); // Start_Menu
        }
    }
}//End Help
