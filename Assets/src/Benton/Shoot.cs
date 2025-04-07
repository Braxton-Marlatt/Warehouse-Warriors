using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    public float bulletSpeed = 10f;

    // Method to shoot a projectile toward a specified target position
    public virtual GameObject Shoot(Vector2 targetPosition)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("BulletPrefab or FirePoint is not assigned!");
            return null;
        }

        BulletPrototype prototype = bulletPrefab.GetComponent<BulletPrototype>();
        if (prototype == null)
        {
            Debug.LogError("bulletPrefab must have a BulletPrototype component!");
            return null;
        }

        Vector2 firePoint2D = firePoint.position;
        Vector2 direction = (targetPosition - firePoint2D).normalized;

        GameObject bullet = prototype.Clone();
        bullet.transform.position = firePoint.position;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        return bullet;
    }
}
