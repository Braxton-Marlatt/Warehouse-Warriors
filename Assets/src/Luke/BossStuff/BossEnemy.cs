using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("Ranged Settings")]
    public float fireRate = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int bulletDamage = 1;
    public float bulletSpeed = 5f;
    private float nextFireTime = 0f;

    // Reference to the EnemyHealth component
    private EnemyHealth enemyHealth;

    // Define phase thresholds (customize as needed)
    private int phase2LowerThreshold = 25; // below 25 hp: ranged phase
    private int phase1LowerThreshold = 50; // between 50 and 25: chase phase (phase 2 in description)

    // Initialization
    void Start()
    {
        // Call base start to initialize spriteRenderer etc
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("BossEnemy: No EnemyHealth component found!");
        }
    }

    // Update is called once per frame.
    // We override Update to switch behavior based on current health.
    protected override void Update()
    {
        if (!isSpawned) return;               // Only run behavior when spawned
        if (enemyHealth == null) return;        // Safety check

        FacePlayer();  // always face the player

        // Determine behavior based on current health value
        int currentHP = enemyHealth.health;

        // Default behavior: use chase (Engage) state.
        // Here we assume the boss in full health (> 50 hp) and between 50 and 25 hp both chase.
        // (You can modify this logic if you wish to have a distinct phase for >50hp.)
        if (currentHP > phase1LowerThreshold || (currentHP <= phase1LowerThreshold && currentHP >= phase2LowerThreshold))
        {
            // Boss chases (like enemy type 0)
            currentState = StateMachine.Engage;
            // Use A* path creation defined in Enemy.cs to navigate toward the player
            CreatePath();
        }
        else if (currentHP < phase2LowerThreshold)
        {
            // Boss ranged phase:
            // Stop moving (freeze the A* pathing) and start shooting at the player
            currentState = StateMachine.Freeze;
            // Remain in place – optionally you could add a bit of movement or strafe if desired.
            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Fire a bullet at the player
    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || player == null)
        {
            Debug.LogError("BossEnemy: Missing bulletPrefab, firePoint, or player reference!");
            return;
        }
        // Instantiate the bullet and set its velocity
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
            // Play shooting sound using SoundFXManager (from Ranged.cs)
            SoundFXManager.Instance.PlaySound("EnemyShoot");
            // Optionally schedule a reload sound via a coroutine
            SoundFXManager.Instance.StartCoroutine(SoundFXManager.Instance.PlaySoundWithDelay("EnemyReload", 0.1f));
        }
        else
        {
            Debug.LogWarning("BossEnemy: Bullet prefab must have a Rigidbody2D component!");
        }
    }
}
