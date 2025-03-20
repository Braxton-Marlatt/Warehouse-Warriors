using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class EnemyBulletTest
{
    private Ranged testRangedEnemy;
    private Transform player;
    
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    [System.Obsolete]
    public IEnumerator Setup()
    {
        yield return new WaitForSeconds(1f); 
        testRangedEnemy = GameObject.FindObjectOfType<Ranged>();
        player = GameObject.FindWithTag("Player")?.transform;

        Assert.NotNull(testRangedEnemy, "Test enemy not found in scene.");
        Assert.NotNull(player, "Player not found in scene.");
    }

    [UnityTest]
    public IEnumerator ShootAtPlayerTest()
    {
        bool isDone = false;
        float startTime = Time.time;

        while (!isDone)
        {
            testRangedEnemy.bulletSpeed += 10;
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"bulletSpeed: {testRangedEnemy.bulletSpeed}, FPS: {fps}");
            testRangedEnemy.Shoot();

            // Check for bullet collisions
            foreach (var bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                if (bullet.GetComponent<Collider2D>().bounds.Intersects(player.GetComponent<Collider2D>().bounds))
                {
                    Assert.Fail("Player was hit by an enemy bullet! Bullet has broken through the wall!");
                    isDone = true;
                }
            }

            // Stop after 5 seconds to prevent infinite loop
            if (Time.time - startTime > 5f)
            {
                isDone = true;
            }

            yield return null;
        }
    }
}
