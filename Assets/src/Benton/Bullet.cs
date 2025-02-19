using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // Damage the bullet deals
    public string targetTag = "Enemy"; // The target object that is hit
    public string obstacleTag = "Walls";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an target
        if (collision.CompareTag(targetTag)){
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.Hurt();
            }
            Destroy(gameObject); // Destroy the bullet after hitting the target
        }

        // Destroy the bullet if it hits an obstacle
        if (collision.CompareTag(obstacleTag))
        {
            Destroy(gameObject);
        }
    }
}
