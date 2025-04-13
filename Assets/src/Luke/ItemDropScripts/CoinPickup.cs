// Description: Picks up coins in the game. When the player collides with a coin, it applies the coin effect and destroys the coin object.

using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private CoinEffect pickupEffect; // Assign via Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickupEffect.ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }
}