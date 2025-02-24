using UnityEngine;

public class Collectible : MonoBehaviour
{
    private enum CollectibleState { Idle, Collected, Destroyed }
    private CollectibleState currentState;
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private float destroyDelay = 0.5f; // Time before destroying the object

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure there is an AudioSource component
        ChangeState(CollectibleState.Idle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentState == CollectibleState.Idle)
        {
            ChangeState(CollectibleState.Collected);
        }
    }

    void ChangeState(CollectibleState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        switch (newState)
        {
            case CollectibleState.Idle:
                break;
            case CollectibleState.Collected:
                if (animator) animator.Play("Collect");
                if (audioSource) audioSource.Play(); // Play sound if available
                Invoke(nameof(DestroyCollectible), destroyDelay); // Destroy after animation
                break;
            case CollectibleState.Destroyed:
                Destroy(gameObject);
                break;
        }
    }

    void DestroyCollectible()
    {
        ChangeState(CollectibleState.Destroyed);
    }
}
