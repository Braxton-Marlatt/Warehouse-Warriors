using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject heartPrefab; // Drag your heart prefab here in the inspector
    public GameObject ammoPrefab;

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
        if (Random.value <= 0.75f) // 75% chance
            {
                if (Random.value < 0.38f) // 50% of the time within the 75%, drop a heart
                {
                    Instantiate(heartPrefab, enemy.transform.position, Quaternion.identity);
                }
                else // 50% of the time within the 75%, drop ammo
                {
                    Instantiate(ammoPrefab, enemy.transform.position, Quaternion.identity);
                }
            }
    }
}
