using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene(1); // Gamescene
    }

    public void loadHelpMenu()
    {
        HelpMenuTracker.source = "start";
        SceneManager.LoadScene(2); // HelpMenu
    }

    
        public void quitGame()
    {
        PauseManager.instance.quitGame();//Singleton Used here
    }

}//End changeScene.cs

