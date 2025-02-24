using UnityEngine;

public class GenericCollectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Health, PowerUp }
    public CollectibleType type;
    
    private Animator animator;
    private AudioSource audioSource;
    
    [SerializeField] private int value = 1; // Example value (coins, health, etc.)
    [SerializeField] private float destroyDelay = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (animator) animator.Play("Collect");
        if (audioSource) audioSource.Play();

        // Example: Modify player stats based on collectible type
        PlayerStats playerStats = FindObjectOfType<PlayerStats>(); // Assuming a player stats script
        if (playerStats)
        {
            switch (type)
            {
                case CollectibleType.Coin:
                    playerStats.AddCoins(value);
                    break;
                case CollectibleType.Health:
                    playerStats.AddHealth(value);
                    break;
                case CollectibleType.PowerUp:
                    playerStats.ActivatePowerUp();
                    break;
            }
        }

        Invoke(nameof(DestroyCollectible), destroyDelay);
    }

    void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
