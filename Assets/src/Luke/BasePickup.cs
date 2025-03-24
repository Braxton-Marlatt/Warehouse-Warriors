using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(GameObject player);
}