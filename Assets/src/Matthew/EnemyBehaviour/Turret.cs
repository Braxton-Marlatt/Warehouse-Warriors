using UnityEngine;

public class Turret : Enemy
{
    /**
     *  LOGIC:
     *  - Shoots at a target (fixed or moving)
     *  - Cannot die
     *  - Only stops shooting once all enemies are dead
     */

    public Transform target; // Target to shoot at
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float fireRate = 2f; // Shoots every 2 seconds

    private float nextFireTime = 0f; // Tracks when to shoot next

    void Update()
    {
        if (target == null){
            Debug.LogWarning("Turret has no target assigned!");
            return;
        }

        if (Time.time >= nextFireTime){
            Shoot();
            nextFireTime = Time.time + fireRate; // Set next fire time
        }
    }

    public void Shoot()
    {
        if (bulletPrefab == null || target == null) return;

        // Instantiate bullet at turret's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null){
            // Calculate direction to target
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }else{
            Debug.LogWarning("Bullet prefab needs a Rigidbody2D component!");
        }
    }
}