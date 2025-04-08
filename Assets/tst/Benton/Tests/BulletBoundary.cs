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

         for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

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
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

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
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

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
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }
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
    public IEnumerator LeftBigCookTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }
        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.FireTripleShot(Vector2.left);

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
    public IEnumerator RightBigCookTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }
        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.FireTripleShot(Vector2.right);

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
    public IEnumerator UpBigCookTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }
        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.FireTripleShot(Vector2.up);

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
    public IEnumerator DownBigCookTrip()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }
        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.FireTripleShot(Vector2.down);

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
    public IEnumerator LeftDirectionBigCook()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.left);

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

    public IEnumerator RightDirectionBigCook()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.right);

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

    public IEnumerator UpDirectionBigCook()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }


        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.up);

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

    public IEnumerator DownDirectionBigCook()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
        playerShooter.bigCookie = true;
        for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.down);

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
    public IEnumerator LeftDirection()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
         for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.left);

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
    public IEnumerator DownDirection()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
         for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.down);

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
    public IEnumerator RightDirection()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
         for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.right);

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
    public IEnumerator UpDirection()
    {
        var playerShooter = GameObject.FindFirstObjectByType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found."); // Load Player in the Scene
         for (int i = 0; i < 3; i++)
        {
            playerShooter.AddAmmo();
        }

        float startTime = Time.time;
        for (int i = 0; i < 100; i++)
        {
            float fps = 1.0f / Time.deltaTime; // Calculate FPS
            Debug.Log($"BulletSpeed: {playerShooter.bulletSpeed}, FPS: {fps}");

            // Generate a random direction
            playerShooter.Shoot(Vector2.up);

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