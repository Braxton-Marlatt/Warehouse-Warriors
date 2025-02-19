using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // The projectile to shoot
    public Transform firePoint;      // The point from which the projectile is fired
    public float bulletSpeed = 10f;  // Speed of the projectile

    // Method to shoot a projectile toward a specified target position
    public virtual GameObject Shoot(Vector2 targetPosition)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("BulletPrefab or FirePoint is not assigned!");
            return null;
        }
        // Calculate direction from the fire point to the target position
        Vector2 firePoint2D = firePoint.position;
        Vector2 direction = (targetPosition - firePoint2D).normalized;

        // Instantiate the bullet and set its velocity
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        
        return bullet; // Return the bullet so the child class can modify it
    }
}
