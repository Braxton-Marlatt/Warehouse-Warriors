using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PauseBoundaryTest
{
    private PlayerShooter playerShooter;
    private PauseManager pauseManager;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("TestScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null;

        playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    [UnityTest]
    public IEnumerator CannotShootWhilePaused()
    {
        pauseManager.resumeGame();
        yield return null;

        int ammoBefore = playerShooter.getAmmo();

        pauseManager.pauseGame();
        yield return new WaitForSecondsRealtime(0.1f);

        playerShooter.Shoot(Vector2.right);
        yield return null;

        int ammoAfter = playerShooter.getAmmo();

        Assert.AreEqual(ammoBefore, ammoAfter, "Ammo decreased while paused.");
        Debug.Log("Passed: Cannot shoot while paused.");
    }
}
