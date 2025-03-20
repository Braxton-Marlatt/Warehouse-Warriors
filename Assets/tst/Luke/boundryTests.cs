using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SimpleHealthTests
{
    GameObject enemy;
    EnemyHealth enemyHealth;
    GameObject heartPrefab;
    EnemyDeathHandler testHandler;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        // 1. Clear existing death handlers
        var existingHandlers = Object.FindObjectsByType<EnemyDeathHandler>(FindObjectsSortMode.None);
        foreach (var handler in existingHandlers)
        {
            Object.Destroy(handler.gameObject);
        }

        // 2. Load prefab with validation
        heartPrefab = Resources.Load<GameObject>("HeartPrefab");
        Assert.IsNotNull(heartPrefab, "Heart prefab not found in Resources folder!");

        // 3. Create test handler FIRST
        testHandler = new GameObject("TestHandler").AddComponent<EnemyDeathHandler>();
        testHandler.heartPrefab = heartPrefab;

        // 4. Create enemy with required components
        enemy = new GameObject("TestEnemy");
        enemy.AddComponent<SpriteRenderer>();
        enemyHealth = enemy.AddComponent<EnemyHealth>();
        enemy.AddComponent<Enemy>();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator Drops_Heart_When_Enemy_Dies()
    {
        // Store initial heart count
        int initialHearts = Object.FindObjectsByType<HeartPickup>(FindObjectsSortMode.None).Length;

        // Trigger death
        enemyHealth.Hurt(1000);
        yield return new WaitForSeconds(0.1f);

        // Verify new heart was created
        int finalHearts = Object.FindObjectsByType<HeartPickup>(FindObjectsSortMode.None).Length;
        Assert.Greater(finalHearts, initialHearts, "No heart spawned on death");

        // Cleanup
        foreach (var heart in Object.FindObjectsByType<HeartPickup>(FindObjectsSortMode.None))
        {
            Object.Destroy(heart.gameObject);
        }
    }

    [UnityTest]
    public IEnumerator Heart_Heals_Player()
    {
        // Create player with required components
        var player = new GameObject("Player");
        player.tag = "Player";

        // Add and configure collider
        var playerCollider = player.AddComponent<BoxCollider2D>();
        playerCollider.isTrigger = true;
        playerCollider.size = Vector2.one * 2f;

        // Add physics components (critical for collision)
        var rb = player.AddComponent<Rigidbody2D>();
        player.AddComponent<SpriteRenderer>(); // Required by PlayerHealth

        // Add and setup PlayerHealth
        var health = player.AddComponent<PlayerHealth>();
        health.maxHealth = 5;
        health.SetHealth(1); // Use public method to set initial health

        // Create heart with required components
        var heart = Object.Instantiate(heartPrefab, player.transform.position, Quaternion.identity);

        // Ensure heart has collider
        var heartCollider = heart.GetComponent<Collider2D>();
        if (heartCollider == null)
        {
            heartCollider = heart.AddComponent<BoxCollider2D>();
            heartCollider.isTrigger = true;
        }

        // Wait for collision processing
        yield return new WaitForSeconds(0.2f); // Increased wait time
        yield return new WaitForFixedUpdate();

        // Verify healing using public GetHealth() method
        Assert.AreEqual(5, health.GetHealth(),
            $"Player health is {health.GetHealth()}, expected 5. " +
            "Check if HeartPickup calls Heal() properly.");

        // Cleanup
        Object.Destroy(player);
        Object.Destroy(heart);
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(enemy);
        Object.Destroy(GameObject.Find("DeathHandler"));
        yield return null;
    }
}