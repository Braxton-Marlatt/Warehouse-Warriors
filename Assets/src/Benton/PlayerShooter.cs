using UnityEngine;

public class PlayerShooter : Shooter
{
    public int bulletDamage = 1; // Damage dealt by the player's bullets
    public float fireRate = 0.2f; // Time between shots (e.g., 5 shots per second)

    private float nextFireTime = 0f; // Time when the player can shoot next
    [SerializeField] private int ammo = 64;

    // For shooting types
    public bool tripleShot = false;
    public bool bigCookie = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return; // Prevent shooting when the game is paused

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && ammo > 0) // Left mouse button held
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayerShoot();
                Debug.Log("Playing shoot sound");
            }
            else
            {
                Debug.LogError("AudioManager instance is null!");
            }

            nextFireTime = Time.time + fireRate;
            AudioManager.Instance.PlayerShoot();

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            animator.SetTrigger("Shoot"); // Trigger animation

            if (tripleShot && ammo >= 3)
            {
                FireTripleShot(mousePos);
            }
            else
            {
                Shoot(mousePos);
                ammo--;
            }
        }
    }

    public void FireTripleShot(Vector2 targetPosition)
    {
        float spreadAngle = 15f;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 leftDir = Quaternion.Euler(0, 0, spreadAngle) * direction;
        Vector2 rightDir = Quaternion.Euler(0, 0, -spreadAngle) * direction;

        Shoot(targetPosition);
        Shoot((Vector2)transform.position + leftDir * 10f);
        Shoot((Vector2)transform.position + rightDir * 10f);

        ammo -= 3;
    }

    public override GameObject Shoot(Vector2 targetPosition)
    {
        if (ammo <= 0) return null;

        GameObject bullet = base.Shoot(targetPosition);

        if (bullet != null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = bulletDamage;

                if (bigCookie)
                {
                    bullet.transform.localScale *= 2f;
                    bulletScript.damage += 1;
                }
            }
        }
        return bullet;
    }

    public int getAmmo() { return ammo; }

    public void AddAmmo()
    {
        ammo += 32;
    }
}
