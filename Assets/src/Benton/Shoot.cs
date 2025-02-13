using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // The projectile to shoot
    public Transform firePoint;      // The point from which the projectile is fired
    public float bulletSpeed = 10f;  // Speed of the projectile

    // Method to shoot a projectile toward a specified target position
    public virtual GameObject Shoot(Vector3 targetPosition)
    {
        // Calculate direction from the fire point to the target position
        Vector3 direction = (targetPosition - firePoint.position).normalized;

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
