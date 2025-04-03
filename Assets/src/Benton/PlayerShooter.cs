using UnityEngine;

public class PlayerShooter : Shooter
{
    public int bulletDamage = 1; // Damage dealt by the player's bullets
    public float fireRate = 0.2f; // Time between shots (e.g., 5 shots per second)
    public Joystick joystick; // Joystick reference for mobile controls

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

    if ((Input.touchCount > 0 || Input.GetMouseButton(0)) && Time.time >= nextFireTime && ammo > 0)
    {
        Vector2 touchPos = Input.touchCount > 0 
            ? (Vector2)Input.GetTouch(0).position 
            : (Vector2)Input.mousePosition;

        // Prevent shooting if touching the joystick
        if (IsTouchOnJoystick(touchPos)) return;

        nextFireTime = Time.time + fireRate;
        AudioManager.Instance?.PlayerShoot();

        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(touchPos);
        animator.SetTrigger("Shoot");

        if (tripleShot && ammo >= 3)
        {
            FireTripleShot(targetPosition);
        }
        else
        {
            Shoot(targetPosition);
            ammo--;
        }
    }
}

bool IsTouchOnJoystick(Vector2 touchPos)
{
    return RectTransformUtility.RectangleContainsScreenPoint(joystick.GetComponent<RectTransform>(), touchPos, null);
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
