using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void startGame()
    {
        SoundEffectManager.Instance.PlaySound("buttonClick", SoundEffectManager.Instance.audioSources);
        SceneManager.LoadScene(1); // Gamescene
    }

    public void loadHelpMenu()
    {
        SoundEffectManager.Instance.PlaySound("buttonClick", SoundEffectManager.Instance.audioSources);
        HelpMenuTracker.source = "start";
        SceneManager.LoadScene(2); // HelpMenu
    }

    
    public void quitGame()
    {
        SoundEffectManager.Instance.PlaySound("buttonClick", SoundEffectManager.Instance.audioSources);
        // Play the quit sound effect
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

}//End changeScene.cs

