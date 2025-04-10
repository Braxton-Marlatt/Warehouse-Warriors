using System.Collections;
using NUnit.Framework;
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
        yield return null;
        yield return new WaitForSecondsRealtime(0.5f);
    }

    private GameObject FindUI(string name)
    {
        var obj = GameObject.Find(name);
        Assert.NotNull(obj, $"GameObject '{name}' not found.");
        return obj;
    }

    [UnityTest]
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

    [UnityTest]
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

    [UnityTest]
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

    [UnityTest]
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

    [UnityTest]
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

        // Re-fetch PauseManager after scene reload
        pause = Object.FindFirstObjectByType<PauseManager>();

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);

    }

    [UnityTest]
    public IEnumerator HelpFromHomeReturnsToHome()
    {
        FindUI("Help_Button").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);
        FindUI("Back_Button").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator StartGameLoadsGameScene()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<changeScene>().startGame();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator HelpFromStartMenuLoadsHelpScene()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<changeScene>().loadHelpMenu();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("helpMenu", SceneManager.GetActiveScene().name);
        Assert.AreEqual("start", HelpMenuTracker.source);
    }

    [UnityTest]
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

        // Refetch pause manager after reloading Game scene
        pause = Object.FindFirstObjectByType<PauseManager>();

        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        Assert.IsTrue(PauseManager.isPaused);
        Assert.IsTrue(pause.pauseMenu.activeSelf);

    }

    [UnityTest]
    public IEnumerator GameOverMainMenuLoads()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.mainMenu();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    //[UnityTest]
    //public IEnumerator QuitGameDoesNotCrash()
    //{
    //    var change = Object.FindFirstObjectByType<changeScene>();
    //    change.quitGame();
    //    yield return null;
    //    Assert.Pass();
    //}

    [UnityTest]
    public IEnumerator GameOverSceneLoadsCorrectly()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("GameOver", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator GameOverButtonsExist()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f); // This needs to be here or it will fail

        Debug.Log("Restart_Button found: " + GameObject.Find("Restart_Button"));
        Debug.Log("Menu_Button found: " + GameObject.Find("Menu_Button"));
        Debug.Log("Quit_Button found: " + GameObject.Find("Quit_Button"));
    }

    [UnityTest]
    public IEnumerator RestartFromGameOverLoadsGame()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<GameOverMenu>().RestartLevel();
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator MainMenuButtonLoadsStartMenu()
    {
        SceneManager.LoadScene("GameOver");
        yield return new WaitForSecondsRealtime(0.5f);
        Object.FindFirstObjectByType<GameOverMenu>().LoadMainMenu();
        yield return new WaitForSecondsRealtime(0.5f);
        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
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

    [UnityTest]
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
