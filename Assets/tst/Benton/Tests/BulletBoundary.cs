using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BulletBoundary
{
    private GameObject player;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }

    public IEnumerator SetUp()
    {
        yield return new WaitForSeconds(1); // Wait for the scene to load
        player = GameObject.FindWithTag("Player");
        Assert.IsNotNull(player, "Player object not found");
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
                if (bullet.transform.position.y > 7f || bullet.transform.position.y < -5f || bullet.transform.position.x > 12f || bullet.transform.position.x < -6f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    [UnityTest]
    public IEnumerator RandomDirectionTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.AddAmmo();


        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            playerShooter.FireTripleShot((Vector2)playerShooter.transform.position + randomDirection);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (bullet.transform.position.y > 7f || bullet.transform.position.y < -5f || bullet.transform.position.x > 12f || bullet.transform.position.x < -6f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    [UnityTest]
    public IEnumerator RandomDirectionBigCook()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        playerShooter.AddAmmo();


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
                if (bullet.transform.position.y > 7f || bullet.transform.position.y < -5f || bullet.transform.position.x > 12f || bullet.transform.position.x < -6f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    [UnityTest]
    public IEnumerator RandomDirectionBigCookTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        playerShooter.AddAmmo();

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            playerShooter.FireTripleShot((Vector2)playerShooter.transform.position + randomDirection);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (bullet.transform.position.y > 7f || bullet.transform.position.y < -5f || bullet.transform.position.x > 12f || bullet.transform.position.x < -6f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    [UnityTest]
    public IEnumerator RandomDirectionPlusShot()
    {
        Vector3 initialPosition = player.transform.position;
        float moveSpeed = 5f;
        float moveDuration = 2f;
        float elapsedTime = 0f;

        // Move left
        while (elapsedTime < moveDuration)
        {
            player.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = player.transform.position;
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        playerShooter.AddAmmo();

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            playerShooter.FireTripleShot((Vector2)playerShooter.transform.position + randomDirection);

            foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (bullet.transform.position.y > 7f || bullet.transform.position.y < -5f || bullet.transform.position.x > 12f || bullet.transform.position.x < -6f)
                {
                    Assert.Fail("Bullet went past ceiling.");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


}