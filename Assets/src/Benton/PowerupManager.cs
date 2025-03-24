using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public int requiredCoinsForTripleShot = 1; // Number of coins required for triple shot

    private CoinManager coinManager; // Reference to CoinManager
    private PlayerShooter playerShooter; // Reference to PlayerShooter

    void Start()
    {
        // Get references to CoinManager and PlayerShooter
        coinManager = FindFirstObjectByType<CoinManager>();
        playerShooter = FindFirstObjectByType<PlayerShooter>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player interacts with the power-up item
        if (other.CompareTag("TripleShot"))
        {
            // Check if the player has enough coins to activate the power-up
            if (coinManager.GetCoinCount() >= requiredCoinsForTripleShot)
            {
                // Activate Triple Shot
                playerShooter.tripleShot = true;
                coinManager.coinCount -= requiredCoinsForTripleShot; // Deduct coins for the power-up
                Debug.Log("Triple Shot Activated!");
            }
            else
            {
                Debug.Log("Not enough coins for power-up.");
            }
            // Optionally, destroy the power-up item after interaction
            Destroy(other.gameObject);
        }
    }
}
