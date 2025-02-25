using UnityEngine;

public class PlayerShooter : Shooter
{
    public int bulletDamage = 1; // Damage dealt by the player's bullets
    public float fireRate = 0.2f; // Time between shots (e.g., 5 shots per second)
    
    private float nextFireTime = 0f; // Time when the player can shoot next
    [SerializeField] public AudioManager audioManager;
    [SerializeField] private int ammo = 35;

    // For shooting types
     public bool tripleShot = false;


    public int getAmmo() { return ammo; }
    void Update()
    {
        if (Time.timeScale == 0) return; // Prevent shooting when the game is paused // Conner added this line

        // Full auto: Removed fire rate restriction by commenting out NextFireTime

        // Handle input for shooting with a fixed fire rate
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && ammo > 0) // Left mouse button held
        {
            //comment out for test
            nextFireTime = Time.time + fireRate; // Set the next allowed fire time // 

            // Get mouse position in world space
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(tripleShot && ammo >= 3){
               FireTripleShot(mousePos);    
            }else{
                // Call the Shoot method from the base class
                Shoot(mousePos);
                ammo--; //Comment out for Play test
                audioManager.PlayPlayerShoot();
            }
        }
    }

       private void FireTripleShot(Vector2 targetPosition)
    {
        float spreadAngle = 15f; // Angle spread for the triple shot

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 leftDir = Quaternion.Euler(0, 0, spreadAngle) * direction;
        Vector2 rightDir = Quaternion.Euler(0, 0, -spreadAngle) * direction;

        Shoot(targetPosition); // Center shot
        Shoot((Vector2)transform.position + leftDir * 10f); // Left shot
        Shoot((Vector2)transform.position + rightDir * 10f); // Right shot

        ammo -= 3;
        audioManager.PlayPlayerShoot();
    }


    public override GameObject Shoot(Vector2 targetPosition)
    {
        if (ammo <= 0) return null; // Prevent shooting if no ammo

        GameObject bullet = base.Shoot(targetPosition);

        // Assign damage to the bullet
        if (bullet != null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = bulletDamage;
            }
        }

        ammo--; // Now ammo will always decrement when Shoot() is called
        return bullet;
    }

}
