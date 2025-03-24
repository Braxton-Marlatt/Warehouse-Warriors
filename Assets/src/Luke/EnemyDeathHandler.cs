using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    // Reference to the heartPrefab2
    public GameObject heartPrefab2;
    public GameObject ammoPrefab;

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


    private void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        // Check if the heartPrefab2 is assigned
        float randomChance = Random.value;  // This will give a float between 0 and 1

        // Spawn ammoPrefab half the time, heartPrefab2 the other half
        if (randomChance < 0.5f)
        {
            // Spawn ammoPrefab
            Instantiate(ammoPrefab, enemy.transform.position, Quaternion.identity);
            Debug.Log("Spawned Ammo");
        }
        else if (randomChance > 0.5f)
        {
            // Spawn heartPrefab2
            Instantiate(heartPrefab2, enemy.transform.position, Quaternion.identity);
            Debug.Log("Spawned Heart");
        }
        else
        {
            Debug.LogWarning("heartPrefab2 is not assigned in the EnemyDeathHandler script.");
        }
    }
}