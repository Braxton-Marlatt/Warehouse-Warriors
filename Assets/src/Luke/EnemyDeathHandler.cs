using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] public GameObject heartPrefab; // Drag your heart prefab here in the inspector
    [SerializeField] public GameObject ammoPrefab;

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
        Debug.Log("Got to enemy death handler");
        // Spawn a heart at the enemy's position when the enemy dies
        if (Random.value <= 0.75f) // 75% chance
            Debug.Log("first loop");
        {
                if (Random.value < 0.38f) // 50% of the time within the 75%, drop a heart
                Debug.Log("Got 2 second loop");
                {
                   Instantiate(heartPrefab, enemy.transform.position, Quaternion.identity);
                //}
                //else // 50% of the time within the 75%, drop ammo
                //{
                   //Instantiate(ammoPrefab, enemy.transform.position, Quaternion.identity);
                }
            }
    }
}
