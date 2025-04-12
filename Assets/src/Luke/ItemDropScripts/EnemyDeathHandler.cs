// Description: Handles the enemy death event and spawns loot based on defined probabilities.

using UnityEngine;
using System.Collections.Generic;

public class EnemyDeathHandler : MonoBehaviour
{
    [System.Serializable]
    public class LootDrop
    {
        public GameObject pickupPrefab; // Assign HeartPickup/AmmoPickup prefabs
        [Range(0, 1)] public float dropProbability;
    }

    [SerializeField] private List<LootDrop> lootDrops = new List<LootDrop>();

    private void OnEnable() => EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    private void OnDisable() => EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;


    // In C#, the virtual keyword marks a method in a base class as overridable, allowing subclasses to provide their own implementation.
    protected virtual void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        float totalWeight = 0;
        foreach (var loot in lootDrops) totalWeight += loot.dropProbability;

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var loot in lootDrops)
        {
            cumulative += loot.dropProbability;
            if (randomValue <= cumulative)
            {
                Instantiate(loot.pickupPrefab, enemy.transform.position, Quaternion.identity);
                return;
            }
        }
    }
}