// Description: This script is responsible for the boss's homing bullet attack.

using UnityEngine;

public class BossHomingShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject homingBulletPrefab;
    public int bulletDamage = 1;
    public float bulletSpeed = 5f;
    public float rotationSpeed = 200f;

    public void ShootHoming() // Function to shoot a homing bullet
    {
        GameObject bullet = Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
        HomingBullet homingComponent = bullet.GetComponent<HomingBullet>();
        homingComponent.Initialize(bulletSpeed, rotationSpeed, bulletDamage);
    }
}