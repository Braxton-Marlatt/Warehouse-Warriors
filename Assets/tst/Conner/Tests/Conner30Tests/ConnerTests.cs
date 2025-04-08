using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ConnerTests_Trimmed
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return null;
        yield return new WaitForSecondsRealtime(0.5f);
    }

    private GameObject FindUI(string name)
    {
        var obj = GameObject.Find(name);
        Assert.NotNull(obj, $"GameObject '{name}' not found.");
        return obj;
    }

    [UnityTest] // Confirms that when player health hits 0, the GameOver scene loads
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

    [UnityTest] // Confirms that PauseManager pauses the game and shows the pause menu
    public IEnumerator PauseMenuIsActiveAfterPause()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        yield return null;

        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Confirms that resuming from pause hides the pause menu
    public IEnumerator PauseMenuIsInactiveAfterResume()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.pauseGame();
        yield return new WaitForSecondsRealtime(0.2f);
        pause.resumeGame();
        yield return null;

        Assert.IsFalse(PauseManager.isPaused);
        Assert.IsFalse(pause.pauseMenu.activeSelf);
    }

    [UnityTest] // Ensures that HelpMenuTracker is updated when Help is opened from pause
    public IEnumerator HelpMenuTrackerSetByPause()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        yield return new WaitForSecondsRealtime(1f);

        var pause = Object.FindFirstObjectByType<PauseManager>();
        pause.loadHelpFromPause();
        yield return new WaitForSecondsRealtime(0.2f);

        Assert.AreEqual("pause", HelpMenuTracker.source);
    }

    [UnityTest] // Tests Pause > Help > Back returns to Game and keeps paused state
    public IEnumerator HelpFromPauseReturnsToPause()
    {
        SceneManager.LoadScene("Game");
        yield return null;
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

    [UnityTest] // Tests Start > Help > Back returns to Start Menu
    public IEnumerator HelpFromHomeReturnsToHome()
    {
        FindUI("Help_Button").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);
        FindUI("Back_Button").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Checks if Start button loads the Game scene
    public IEnumerator StartGameLoadsGameScene()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<changeScene>().startGame();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Confirms Help button loads Help scene and sets source to "start"
    public IEnumerator HelpFromStartMenuLoadsHelpScene()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<changeScene>().loadHelpMenu();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("helpMenu", SceneManager.GetActiveScene().name);
        Assert.AreEqual("start", HelpMenuTracker.source);
    }

    [UnityTest] // Tests that returning from Help still keeps game paused
    public IEnumerator ResumeAfterHelpKeepsPaused()
    {
        SceneManager.LoadScene("Game");
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

    [UnityTest] // Confirms PauseManager.mainMenu loads Start_Menu from Pause state
    public IEnumerator GameOverMainMenuLoads()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.mainMenu();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Tests that quitGame() doesn't crash in editor during tests
    public IEnumerator QuitGameDoesNotCrash()
    {
        var change = Object.FindFirstObjectByType<changeScene>();
        change.quitGame(); // Should not crash
        yield return null;
        Assert.Pass();
    }

    [UnityTest] // Loads GameOver scene directly and verifies it's correct
    public IEnumerator GameOverSceneLoadsCorrectly()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("GameOver", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Checks that all 3 buttons exist in the GameOver scene
    public IEnumerator GameOverButtonsExist()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.NotNull(GameObject.Find("RestartButton"));
        Assert.NotNull(GameObject.Find("MainMenuButton"));
        Assert.NotNull(GameObject.Find("QuitButton"));
    }

    [UnityTest] // Verifies that the restart button loads the Game scene
    public IEnumerator RestartFromGameOverLoadsGame()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<GameOverMenu>().RestartLevel();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Verifies that the Main Menu button loads Start_Menu
    public IEnumerator MainMenuButtonLoadsStartMenu()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<GameOverMenu>().LoadMainMenu();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest] // Makes sure PauseMenu is not active in GameOver scene
    public IEnumerator PauseMenuNotActiveOnGameOver()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        var pause = Object.FindFirstObjectByType<PauseManager>();
        if (pause != null)
            Assert.IsFalse(pause.pauseMenu.activeSelf);
        else
            Assert.Pass("No PauseManager found — no duplicate.");
    }

    [UnityTest] // Confirms that reloading scenes doesn't duplicate PauseManager
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
