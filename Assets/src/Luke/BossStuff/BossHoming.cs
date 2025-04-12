using UnityEngine;

public class BossHomingAttack : MonoBehaviour
{
    public GameObject homingBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 4f;
    public float homingStrength = 5f;
    public int bulletDamage = 2;
    public float attackCooldown = 6f;

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
            ShootHomingBullet();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void ShootHomingBullet()
    {
        if (homingBulletPrefab == null) return;

        GameObject bullet = Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
        HomingBullet homingComponent = bullet.AddComponent<HomingBullet>();

        homingComponent.Initialize(player, bulletSpeed, homingStrength, bulletDamage);
        PlayShootingSounds();
    }

    private void PlayShootingSounds()
    {
        SoundFXManager.Instance.PlaySound("EnemyShoot");
        SoundFXManager.Instance.StartCoroutine(
            SoundFXManager.Instance.PlaySoundWithDelay("EnemyReload", 0.1f)
        );
    }
}

// Homing Bullet Component
public class HomingBullet : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float homingStrength;
    private int damage;
    private Rigidbody2D rb;

    public void Initialize(Transform target, float speed, float homingStrength, int damage)
    {
        this.target = target;
        this.speed = speed;
        this.homingStrength = homingStrength;
        this.damage = damage;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * homingStrength;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().Hurt(damage);
            Destroy(gameObject);
        }
    }
}