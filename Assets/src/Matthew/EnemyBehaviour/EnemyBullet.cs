using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage; // Damage the bullet deals
    public string targetTag = "Player"; // The target object that is hit
    public string obstacleTag = "Walls";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag(targetTag) || collision.CompareTag(obstacleTag)){
            Destroy(gameObject);
        }
    }
}
