using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AmmoTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }



    [UnityTest]
    public IEnumerator ammoTest()
    {
        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found in scene");

        while (playerShooter.getAmmo() > 1)
        {
            playerShooter.Shoot(Vector2.right);
            yield return null;
        }

        //Ammo Should be 1
        playerShooter.Shoot(Vector2.right);
        yield return null;
        Assert.AreEqual(0, playerShooter.getAmmo()); // Inside Boundary Test

        //Ammo should be 0
        playerShooter.Shoot(Vector2.right);
        yield return null;
        Assert.AreEqual(0, playerShooter.getAmmo()); // Outside Boundary Test
        yield return null;
    }
}
