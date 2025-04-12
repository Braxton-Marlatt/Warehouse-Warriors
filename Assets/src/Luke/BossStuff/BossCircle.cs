using UnityEngine;

public class BossCircularAttack : MonoBehaviour
{
    public int numberOfBullets = 12;
    public float bulletSpeed = 5f;
    public int bulletDamage = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackCooldown = 4f;

    private float nextAttackTime;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            ShootCircular();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void ShootCircular()
    {
        if (bulletPrefab == null) return;

        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector2 bulletDir = Quaternion.Euler(0, 0, angle) * Vector2.up;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletDir * bulletSpeed;
            }

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.damage = bulletDamage;
            }

            angle += angleStep;
            PlayShootingSounds();
        }
    }

    private void PlayShootingSounds()
    {
        SoundFXManager.Instance.PlaySound("EnemyShoot");
        SoundFXManager.Instance.StartCoroutine(
            SoundFXManager.Instance.PlaySoundWithDelay("EnemyReload", 0.1f)
        );
    }
}