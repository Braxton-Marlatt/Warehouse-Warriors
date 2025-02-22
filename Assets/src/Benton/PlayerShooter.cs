using UnityEngine;

public class PlayerShooter : Shooter
{
    public int bulletDamage = 1; // Damage dealt by the player's bullets
    public float fireRate = 0.2f; // Time between shots (e.g., 5 shots per second)
    private float nextFireTime = 0f; // Time when the player can shoot next
    [SerializeField] private int ammo = 35;

    void Update()
    {
        // Handle input for shooting with a fixed fire rate
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && ammo > 0) // Left mouse button held
        {
            nextFireTime = Time.time + fireRate; // Set the next allowed fire time
            
            // Get mouse position in world space
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Call the Shoot method from the base class
            Shoot(mousePos);
            ammo--;
        }
    }

    // Override Shoot to assign damage to the bullet
    public override GameObject Shoot(Vector2 targetPosition)
    {
        GameObject bullet = base.Shoot(targetPosition); // Get the bullet from the parent class

        // Assign damage to the bullet
        if (bullet != null)
        {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = bulletDamage;
            }
        }

        return bullet; // Return the modified bullet (optional)
    } 

    public void AddAmmo(int amount=10)
    {
        ammo +=amount;
    }

}
