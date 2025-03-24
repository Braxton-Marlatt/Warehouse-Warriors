using UnityEngine;
using System.Collections.Generic;

public class EnemyDeathHandler : MonoBehaviour
{
    [System.Serializable]
    public class LootDrop
    {
        public GameObject pickupPrefab;
        [Range(0, 1)] public float dropProbability;
    }

    public List<LootDrop> lootDrops = new List<LootDrop>();

    private void OnEnable() => EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    private void OnDisable() => EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;

    private void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy)
    {
        float totalWeight = 0f;
        foreach (var loot in lootDrops)
        {
            totalWeight += loot.dropProbability;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var loot in lootDrops)
        {
            cumulative += loot.dropProbability;
            if (randomValue <= cumulative)
            {
                if (loot.pickupPrefab != null)
                {
                    Instantiate(loot.pickupPrefab, enemy.transform.position, Quaternion.identity);
                    Debug.Log($"Dropped {loot.pickupPrefab.name}");
                }
                return;
            }
        }
    }
}