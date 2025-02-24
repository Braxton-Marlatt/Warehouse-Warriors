using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


//Everyone will need the next bit of code
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;


public class BulletAmplifier
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }
 //To Here ^



    [UnityTest]
    public IEnumerator RoofTest()
    {
        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found.");
        bool IsDone = false;

        float startTime = Time.time;
        while (!IsDone)
        {
            playerShooter.bulletSpeed += 10;
            Debug.Log("BulletSpeed: " + playerShooter.bulletSpeed);
            playerShooter.Shoot((Vector2)playerShooter.transform.position + Vector2.up);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                if (bullet.transform.position.y > 5.37f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                    IsDone = true;
                }
            yield return new WaitForSeconds(0.0f);
        }
    }
}

