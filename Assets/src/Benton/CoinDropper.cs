using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    public GameObject coinPrefab;  // Assign in Unity Inspector
    public Transform player;       // Reference to the player

    void DropCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("Coin prefab not assigned!");
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 2f; // Random position in a small radius
            Vector2 spawnPosition = (Vector2)player.position + randomOffset;

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
