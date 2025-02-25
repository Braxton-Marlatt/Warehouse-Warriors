using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AmmoTest2
{

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("TestScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // Extra frame to ensure scene objects are initialized
        yield return null;
    }



    [UnityTest]
    public IEnumerator spareAmmo()
    {
        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found.");

        // Shoot until one bullet remains
        while (playerShooter.getAmmo() > 1)
        {
            playerShooter.Shoot(Vector2.right);
            yield return null; // Yield to allow frame updates
        }

        // Fire the last bullet, expecting ammo to become 0
        playerShooter.Shoot(Vector2.right);
        yield return null;
        Assert.AreEqual(0, playerShooter.getAmmo()); // Inside Boundary Test

        // Try shooting when ammo is already 0
        playerShooter.Shoot(Vector2.right);
        yield return null;
        Assert.AreEqual(0, playerShooter.getAmmo()); // Outside Boundary Test

        yield return null;
    }
}
