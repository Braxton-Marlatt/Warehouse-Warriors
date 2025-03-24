using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private HealthEffect pickupEffect; // Assign via Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickupEffect.ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }
}