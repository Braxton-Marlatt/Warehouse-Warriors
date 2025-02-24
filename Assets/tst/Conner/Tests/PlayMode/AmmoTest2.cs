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



    //[UnityTest]
    //public IEnumerator spareAmmo()
    //{
    //    var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
    //    Assert.NotNull(playerShooter, "PlayerShooter not found.");

    //    // Shoot until one bullet remains
    //    while (playerShooter.getAmmo() > 1)
    //    {
    //        playerShooter.Shoot(Vector2.right);
    //        yield return null; // Yield to allow frame updates
    //    }

    //    // Fire the last bullet, expecting ammo to become 0
    //    playerShooter.Shoot(Vector2.left);
    //    yield return null;
    //    Assert.AreEqual(0, playerShooter.getAmmo()); // Inside Boundary Test

    //    // Try shooting when ammo is already 0
    //    playerShooter.Shoot(Vector2.left);
    //    yield return null;
    //    Assert.AreEqual(0, playerShooter.getAmmo()); // Outside Boundary Test

    //    yield return null;
    //}

    [UnityTest]
    public IEnumerator spareAmmo()
    {
        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found.");

        Debug.Log("Starting ammo: " + playerShooter.getAmmo());

        float timeout = 10f;
        float startTime = Time.time;

        // Shoot until one bullet remains or timeout is reached
        while (playerShooter.getAmmo() > 1)
        {
            playerShooter.Shoot(Vector2.right);
            Debug.Log("Current Ammo: " + playerShooter.getAmmo()); // Log Ammo

            yield return new WaitForSeconds(0.1f); // Allow time for changes to take effect

            if (Time.time - startTime > timeout)
            {
                Assert.Fail("Timeout: Ammo did not decrement to 1 within expected time.");
                yield break;
            }
        }

        Debug.Log("Final Ammo Before Last Shot: " + playerShooter.getAmmo());

        // Fire the last bullet, expecting ammo to become 0
        playerShooter.Shoot(Vector2.left);
        yield return null;
        Debug.Log("Ammo after last shot: " + playerShooter.getAmmo());
        Assert.AreEqual(0, playerShooter.getAmmo(), "Ammo should be 0 after firing the last bullet.");

        // Attempt to shoot when ammo is already 0
        playerShooter.Shoot(Vector2.left);
        yield return null;
        Debug.Log("Ammo after attempting to fire with no ammo: " + playerShooter.getAmmo());
        Assert.AreEqual(0, playerShooter.getAmmo(), "Ammo should remain 0 when attempting to fire with no ammo.");

        yield return null;
    }


}






