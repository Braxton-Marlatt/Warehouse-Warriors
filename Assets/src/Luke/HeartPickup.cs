using UnityEngine;

public class HeartPickup : BasePickup
{
    public int healAmount = 1;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal();
            Debug.Log($"Player healed by {healAmount} HP!");
        }
    }
}