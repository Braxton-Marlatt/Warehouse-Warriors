using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject heartPrefab; // Drag your heart prefab here in the inspector

    private void OnEnable()
    {
        // Subscribe to the enemy death event
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        // Unsubscribe from the enemy death event
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
    }

    // Method to handle enemy death
    private void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        // Spawn a heart at the enemy's position when the enemy dies
        if (heartPrefab != null)
        {
            Instantiate(heartPrefab, enemy.transform.position, Quaternion.identity);
        }
    }
}
