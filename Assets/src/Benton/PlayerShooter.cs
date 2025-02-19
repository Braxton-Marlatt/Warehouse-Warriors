using UnityEngine;

public class PlayerShooter : Shooter
{
    public int bulletDamage = 1; // Damage dealt by the player's bullets

    void Update()
    {
        // Handle input for shooting
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Get mouse position in world space
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Call the Shoot method from the base class
            Shoot(mousePos);
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
}
