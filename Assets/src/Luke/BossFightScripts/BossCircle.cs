// Description: This script handles the boss's circular shooting pattern.

using UnityEngine;
using System.Collections;

public class BossCircleShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int bulletDamage = 1;
    public float bulletSpeed = 5f;
    public float timeBetweenBullets = 0.1f;

    public void TriggerBurst()
    {
        StartCoroutine(ShootBurst());
    }

    private IEnumerator ShootBurst() // Coroutine to handle the burst shooting
    {
        int bullets = 12;
        float angleStep = 360f / bullets;
        float currentAngle = 0;

        for (int i = 0; i < bullets; i++)
        {
            Vector2 dir = Quaternion.Euler(0, 0, currentAngle) * Vector2.up;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Instantiate the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = dir * bulletSpeed;

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent) bulletComponent.damage = bulletDamage; // Set the bullet damage

            currentAngle += angleStep;
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }
}