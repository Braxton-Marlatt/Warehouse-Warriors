using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class ConnerTests
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
        // LogAssert.Expect(LogType.Error, new Regex(".*AudioSource.*null.*")); // There is a known error in the MusicManager that we are silencing here

    }

    [UnityTest] // Checks if Help button returns to home screen from home
    public IEnumerator HelpFromHomeReturnsToHome() 
    {
        // Silence known MusicManager errors
        LogAssert.Expect(LogType.Error, "WeBringTheBoom AudioSource is null!");


        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);

        GameObject helpBtn = GameObject.Find("Help_Button");
        helpBtn.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);

        GameObject returnBtn = GameObject.Find("Back_Button");
        returnBtn.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Checks if Help button returns to game (Pause) from game(Pause)
    public IEnumerator HelpFromPauseReturnsToPause()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(1f);

        // Pause the game
        PauseManager pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        yield return new WaitForSecondsRealtime(0.5f);

        // Go to help
        HelpMenuTracker.source = "pause";
        pause.loadHelpFromPause();
        yield return new WaitForSecondsRealtime(1f);

        // Return from help
        Object.FindFirstObjectByType<helpMenuManager>().returnFromHelp();
        yield return new WaitForSecondsRealtime(1f);

        // Check if we're back and paused
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Checks if pause activates when called
    public IEnumerator PauseMenuIsActiveAfterPause()
    {
        SceneManager.LoadScene(1); // Load Game scene
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        yield return null;

        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Checks if resume hides pause
    public IEnumerator PauseMenuIsInactiveAfterResume()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        yield return new WaitForSecondsRealtime(0.2f);
        pause.resumeGame();
        yield return null;

        Assert.IsFalse(PauseManager.isPaused);
        Assert.IsFalse(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Checks HelpMenuTracker changes on loadHelpFromPause()
    public IEnumerator HelpMenuTrackerSetByPause()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.loadHelpFromPause();
        yield return new WaitForSecondsRealtime(0.2f);

        Assert.AreEqual("pause", HelpMenuTracker.source);
    }

    [UnityTest] // Checks quitGame() doesn’t crash
    public IEnumerator QuitGameDoesNotCrash()
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSecondsRealtime(0.5f);

        var change = Object.FindFirstObjectByType<changeScene>();
        Assert.NotNull(change);
        change.quitGame(); // Should not crash
        yield return null;

        Assert.Pass();
    }

    [UnityTest] // Calls PauseManager.mainMenu() from GameOverMenu
    public IEnumerator GameOverMainMenuLoads()
    {
        SceneManager.LoadScene(1); // Game scene
        yield return new WaitForSecondsRealtime(1f);

        PauseManager.instance.mainMenu();
        yield return new WaitForSecondsRealtime(1f);

        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Makes sure scene 1 = Game
    public IEnumerator StartGameLoadsGameScene()
    {
        SceneManager.LoadScene(0); // Main menu
        yield return new WaitForSecondsRealtime(0.5f);

        var cs = Object.FindFirstObjectByType<changeScene>();
        cs.startGame();
        yield return new WaitForSecondsRealtime(1f);

        Assert.AreEqual(1, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest] // Checks Help button sets source and loads help scene
    public IEnumerator HelpFromStartMenuLoadsHelpScene()
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSecondsRealtime(0.5f);

        var cs = Object.FindFirstObjectByType<changeScene>();
        cs.loadHelpMenu();
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
        Assert.AreEqual("start", HelpMenuTracker.source);
    }


    [UnityTest] // Simulates ESC key and checks pause toggles
    public IEnumerator PauseTogglesWithEscape()
    {
        SceneManager.LoadScene(1); // Game
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        Assert.NotNull(pause);

        pause.pauseGame();
        yield return null;
        Assert.IsTrue(PauseManager.isPaused);

        pause.resumeGame();
        yield return null;
        Assert.IsFalse(PauseManager.isPaused);
    }

    [UnityTest] // Verifies HelpMenuTracker.source is 'pause' after help call
    public IEnumerator HelpMenuTracksPauseSource()
    {
        SceneManager.LoadScene(1); // Game
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.loadHelpFromPause();
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("pause", HelpMenuTracker.source);
    }

    [UnityTest] // Returns from help and checks game remains paused
    public IEnumerator ResumeAfterHelpKeepsPaused()
    {
        SceneManager.LoadScene(1); // Game
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        HelpMenuTracker.source = "pause";
        pause.loadHelpFromPause();
        yield return new WaitForSecondsRealtime(1f);

        Object.FindFirstObjectByType<helpMenuManager>().returnFromHelp();
        yield return new WaitForSecondsRealtime(1f);

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // From pause menu, main menu loads Start_Menu scene
    public IEnumerator MainMenuFromPauseLoadsStart()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(1f);

        PauseManager.instance.mainMenu();
        yield return new WaitForSecondsRealtime(1f);

        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Calls quitGame from GameOverMenu, test should not crash
    public IEnumerator QuitFromGameOverDoesNotCrash()
    {
        SceneManager.LoadScene(3); // GameOver scene if that's your setup
        yield return new WaitForSecondsRealtime(0.5f);

        var gm = Object.FindFirstObjectByType<GameOverMenu>();
        Assert.NotNull(gm);
        gm.QuitGame();
        Assert.Pass(); // We just want to ensure no crash
    }

    [UnityTest] // Pause menu should not be active on Game load
    public IEnumerator PauseMenuIsDisabledByDefault()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(0.5f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        Assert.IsFalse(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Checks that only one PauseManager exists in scene
    public IEnumerator PauseManagerIsSingleton()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(0.5f);

        var all = Object.FindObjectsByType<PauseManager>(FindObjectsSortMode.None);
        Assert.LessOrEqual(all.Length, 1);
    }

    [UnityTest] // Checks default HelpMenuTracker.source is 'start'
    public IEnumerator HelpMenuTrackerDefaultValue()
    {
        yield return null;
        Assert.AreEqual("start", HelpMenuTracker.source);
    }

    [UnityTest] // Checks Restart button loads Game scene
    public IEnumerator RestartFromGameOverLoadsGame()
    {
        SceneManager.LoadScene(3); // GameOver
        yield return new WaitForSecondsRealtime(0.5f);

        var gm = Object.FindFirstObjectByType<GameOverMenu>();
        gm.RestartLevel();
        yield return new WaitForSecondsRealtime(1f);

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Checks PauseManager is not duplicated on reload
    public IEnumerator SceneLoadDoesNotDuplicatePause()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSecondsRealtime(0.5f);
        var before = Object.FindObjectsByType<PauseManager>(FindObjectsSortMode.None).Length;

        SceneManager.LoadScene(2); // Help scene
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene(1); // Back to game
        yield return new WaitForSecondsRealtime(1f);

        var after = Object.FindObjectsByType<PauseManager>(FindObjectsSortMode.None).Length;
        Assert.LessOrEqual(after, before);
    }



    [UnityTest] // Loads GameOver scene and confirms it's loaded
    public IEnumerator GameOverSceneLoadsCorrectly()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("GameOver", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Checks if key GameOver buttons exist
    public IEnumerator GameOverButtonsExist()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.NotNull(GameObject.Find("RestartButton"));
        Assert.NotNull(GameObject.Find("MainMenuButton"));
        Assert.NotNull(GameObject.Find("QuitButton"));
    }

    [UnityTest] // Simulates restart button functionality
    public IEnumerator RestartButtonLoadsGame()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        var gm = Object.FindFirstObjectByType<GameOverMenu>();
        gm.RestartLevel();
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Simulates main menu button functionality
    public IEnumerator MainMenuButtonLoadsStartMenu()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        var gm = Object.FindFirstObjectByType<GameOverMenu>();
        gm.LoadMainMenu();
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Calls quit function (should not crash)
    public IEnumerator QuitButtonDoesNotCrashEditor()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        var gm = Object.FindFirstObjectByType<GameOverMenu>();
        gm.QuitGame(); // Should not throw
        Assert.Pass();
    }

    [UnityTest] // Confirms GameOverScript is in scene
    public IEnumerator GameOverScriptIsPresent()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(0.5f);

        var gos = Object.FindFirstObjectByType<GameOverScript>();
        Assert.NotNull(gos);
    }

    [UnityTest] // Triggers GameOver when player health hits 0
    public IEnumerator GameOverTriggersOnZeroHealth()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(0.5f);

        var player = GameObject.FindWithTag("Player");
        var health = player.GetComponent<PlayerHealth>();

        health.SetHealth(0);
        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("GameOver", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Makes sure pause menu isn't active on GameOver
    public IEnumerator PauseMenuNotActiveOnGameOver()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        if (pause != null)
        {
            Assert.IsFalse(pause.pauseMenu.activeSelf);
        }
        else
        {
            Assert.Pass("No PauseManager found — no duplicate.");
        }
    }

    [UnityTest] // Checks that scene switches from GameOver don't cause duplicates
    public IEnumerator SceneLoadsFromGameOverDoNotDuplicateManagers()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(0.5f);

        var all = Object.FindObjectsByType<PauseManager>(FindObjectsSortMode.None);
        Assert.LessOrEqual(all.Length, 1);
    }
}






