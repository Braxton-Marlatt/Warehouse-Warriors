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
        HelpMenuTracker.source = "start"; // tells back button from help where to return to
        SceneManager.LoadScene(2); // HelpMenu
    }

    
     
       public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

}//End changeScene.cs

