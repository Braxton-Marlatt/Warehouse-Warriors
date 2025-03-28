using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public int requiredCoinsForTripleShot = 1; // Number of coins required for triple shot
    public int requiredCoinsForBigCook = 1;
    public int requiredCoinsForFastDash = 1;
    public int requiredCoinsForBigBake = 1;


    private CoinManager coinManager; // Reference to CoinManager
    private PlayerShooter playerShooter; // Reference to PlayerShooter
    private PlayerMovement playerMovement; // Reference to PlayerMovement
    private UpdMelee updMelee; // Reference to UpdMelee

    void Start()
    {
        // Get references to CoinManager and PlayerShooter
        coinManager = FindFirstObjectByType<CoinManager>();
        playerShooter = FindFirstObjectByType<PlayerShooter>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        updMelee = FindFirstObjectByType<UpdMelee>();

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
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("BigCook"))
        {
            // Check if the player has enough coins to activate the power-up
            if (coinManager.GetCoinCount() >= requiredCoinsForBigCook)               
            {
                    playerShooter.bigCookie = true;
                    coinManager.coinCount -= requiredCoinsForBigCook; // Deduct coins for the power-up
                    Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("FastDash"))
        {
            if (coinManager.GetCoinCount() >= requiredCoinsForFastDash)
            {
                playerMovement.fastDash = true;
                coinManager.coinCount -= requiredCoinsForFastDash; // Deduct coins for the power-up
                Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("BigBake"))
        {
            if (coinManager.GetCoinCount() >= requiredCoinsForBigBake)
            {
                updMelee.bigBake = true;
                coinManager.coinCount -= requiredCoinsForFastDash; // Deduct coins for the power-up
                Destroy(other.gameObject);
            }
        }
    }
}
