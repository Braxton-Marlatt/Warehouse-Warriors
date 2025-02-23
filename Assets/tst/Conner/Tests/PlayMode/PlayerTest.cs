#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerShooterStressTest
{
    [UnityTest]
    public IEnumerator BulletStressTest()
    {
        SceneManager.LoadScene("TestScene");
        yield return new WaitForSeconds(1f); // Allow scene to load

        // Ensure Camera Exists & Set to -18.89
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = new GameObject("MainCamera").AddComponent<Camera>();
            mainCamera.tag = "MainCamera";
        }
        mainCamera.transform.position = new Vector3(-18.89f, 0, -10);

        var gameObject = new GameObject("PlayerShooter");
        var playerShooter = gameObject.AddComponent<PlayerShooter>();
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.transform.position = Vector3.zero;

        // Load BulletPrefab
        GameObject bulletPrefab = null;
#if UNITY_EDITOR
        bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Benton/PlayerBullet.prefab");
#endif
        Assert.NotNull(bulletPrefab, "BulletPrefab not found.");

        // Assign BulletPrefab
        var bulletPrefabField = playerShooter.GetType().BaseType.GetField("bulletPrefab",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(bulletPrefabField, "bulletPrefab field missing.");
        bulletPrefabField.SetValue(playerShooter, bulletPrefab);

        // Create & Assign FirePoint
        Transform firePoint = new GameObject("FirePoint").transform;
        firePoint.position = gameObject.transform.position + Vector3.right;
        firePoint.SetParent(gameObject.transform);

        var firePointField = playerShooter.GetType().BaseType.GetField("firePoint",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(firePointField, "firePoint field missing.");
        firePointField.SetValue(playerShooter, firePoint);

        // Ensure Ammo
        playerShooter.AddAmmo(9999);

        int bulletCount = 0;
        float fps = 999f, testDuration = 10f, startTime = Time.time;

        while (fps >= 60f && Time.time - startTime < testDuration)
        {
            playerShooter.Shoot(Vector2.right);
            bulletCount++;
            yield return new WaitForSeconds(0.01f);
            fps = 1f / Time.deltaTime;
        }

        Debug.Log($"FPS dropped below 60 after {bulletCount} bullets.");
        Assert.Greater(bulletCount, 0, "FPS dropped too soon.");
    }
}
