using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // Damage the bullet deals
    public string targetTag; // The target object that is hit

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an target
        if (collision.CompareTag(targetTag))
        {
            // EnemyHealth health = collision.GetComponent<EnemyHealth>();
            // if (health != null)
            // {
            //     //health.TakeDamage(damage); // Deal damage to the enemy
            // }

            Destroy(gameObject); // Destroy the bullet after hitting the target
        }

        // Destroy the bullet if it hits an obstacle
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
