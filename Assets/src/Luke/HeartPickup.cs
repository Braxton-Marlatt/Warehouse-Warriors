using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Heart picked up by player!");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                int newHealth = (playerHealth.GetHealth() + healAmount);
                playerHealth.SetHealth(newHealth);

                Debug.Log("Player healed by " + healAmount + " HP!");
            }
            Destroy(gameObject);

        }
    }
}
