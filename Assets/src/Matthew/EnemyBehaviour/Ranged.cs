using UnityEngine;

public class Ranged : EnemyMovement
{
    public float fireRate = 3f;
    public bool stopOnAim = false;
    public Transform firePoint; 
    public GameObject bulletPrefab;
    public int bulletDamage= 1;
    public float bulletSpeed = 5f;
    private float nextFireTime = 0f;
    private void Update(){
        if (player == null || firePoint == null || bulletPrefab == null){
            Debug.LogError("Player, Fire Point, or Bullet Prefab not assigned in the Inspector!");
            return;
        }
        if (Time.time > nextFireTime){
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    public void Shoot(){
        if (bulletPrefab == null) return;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null){
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }else Debug.LogWarning("Bullet prefab needs a Rigidbody2D component!");
        
    }
}
