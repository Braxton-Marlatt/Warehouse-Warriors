using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BulletBoundary
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnityTest]
    public IEnumerator RandomDirection()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            playerShooter.Shoot((Vector2)playerShooter.transform.position + randomDirection);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (bullet.transform.position.y > 6f || bullet.transform.position.y < -5f || bullet.transform.position.x > -11f || bullet.transform.position.x < -28f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}