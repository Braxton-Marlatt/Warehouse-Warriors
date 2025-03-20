using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    // Reference to the heartPrefab2
    public GameObject heartPrefab2;

    private void OnEnable()
    {
        // Subscribe to the enemy death event
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        // Unsubscribe from the enemy death event to prevent memory leaks
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
    }

    // Method to handle enemy death
    private void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        // Check if the heartPrefab2 is assigned
        if (heartPrefab2 != null)
        {
            // Spawn the heartPrefab2 at the enemy's position
            Instantiate(heartPrefab2, enemy.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("heartPrefab2 is not assigned in the EnemyDeathHandler script.");
        }
    }
}