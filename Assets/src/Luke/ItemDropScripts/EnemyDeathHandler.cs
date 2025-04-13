// Description: Handles enemy death and loot drops. Uses private fields and properties for encapsulation.

using UnityEngine;
using System.Collections.Generic;

public class EnemyDeathHandler : MonoBehaviour
{
    [System.Serializable]
    public class LootDrop // Nested class (unchanged)
    {
        public GameObject pickupPrefab;
        [Range(0, 1)] public float dropProbability;
    }

    [System.Serializable]
    public class LootData // Dedicated data class
    {
        public List<LootDrop> lootDrops = new List<LootDrop>();
    }

    [SerializeField] private LootData lootData; // Replaces direct lootDrops

    private void OnEnable() => EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    private void OnDisable() => EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;

    protected virtual void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        float totalWeight = 0;
        foreach (var loot in lootData.lootDrops) totalWeight += loot.dropProbability;

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var loot in lootData.lootDrops) // Use lootData.lootDrops
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