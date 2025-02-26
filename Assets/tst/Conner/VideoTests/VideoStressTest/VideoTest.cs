using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class VideoTest
{

    [OneTimeSetUp]

    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }



    [UnityTest]
    public IEnumerator ShootAtCeilingTest()
    {
       var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found in scene");

        bool isDone = false;

        float startTime = Time.time;

        while (!isDone)
        {
            playerShooter.bulletSpeed += 10;
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"bulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            playerShooter.Shoot((Vector2)playerShooter.transform.position+ Vector2.up);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                if (bullet.transform.position.y > 5.37)
                {
                    Assert.Fail("Bullet went past the ceiling");
                    isDone = true;
                }
                yield return null;
            
        }
    }
}
