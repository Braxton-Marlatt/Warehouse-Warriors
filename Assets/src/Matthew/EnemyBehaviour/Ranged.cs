using UnityEngine;

public class Ranged : Enemy
{
    public float fireRate = 3f;
    public Transform firePoint; 
    public GameObject bulletPrefab;
    public int bulletDamage= 1;
    public float bulletSpeed = 5f;
    private float nextFireTime = 0f;
    //private bool isShooting = false;

    protected override void Update(){
        base.Update();
        if (!isSpawned) return;
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
            SoundFXManager.Instance.PlaySound("EnemyShoot"); // Play shooting sound
            SoundFXManager.Instance.PlaySoundWithDelay("EnemyReload", 0.5f); // Play shooting sound with a delay
        }else Debug.LogWarning("Bullet prefab needs a Rigidbody2D component!");
    }
    //public override void CreatePath(){
    //    if (!isShooting) base.CreatePath();
    //}
}
