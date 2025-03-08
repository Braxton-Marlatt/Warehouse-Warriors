using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage; // Damage the bullet deals
    public string targetTag = "Player"; // The target object that is hit
    public string obstacleTag = "Walls";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the bullet hits an enemy
        if (collision.CompareTag(targetTag)){
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null){
                Vector2 direction = ((Vector2)collision.transform.position - (Vector2)transform.position).normalized;
                playerHealth.Hurt(damage);
            }
            Destroy(gameObject);
        }

        // Destroy the bullet if it hits an obstacle
        if (collision.CompareTag(obstacleTag)){
            Destroy(gameObject);
        }
    }
}
