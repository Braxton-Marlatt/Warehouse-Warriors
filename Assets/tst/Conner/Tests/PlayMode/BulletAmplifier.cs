using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class BulletAmplifier
{

// Everyone will need the next bit of code
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }
// To Here ^

    [UnityTest]
    [System.Obsolete]
    public IEnumerator RoofTest()
    {
        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene


        bool IsDone = false;

        float startTime = Time.time;
        while (!IsDone)
        {
            playerShooter.bulletSpeed += 15;
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            playerShooter.Shoot((Vector2)playerShooter.transform.position + Vector2.up);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                if (bullet.transform.position.y > 5.37f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                    IsDone = true;
                }
            yield return null;
        }
    }
}
