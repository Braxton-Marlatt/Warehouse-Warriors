using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // This can be used if you modify AddAmmo() later

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupAmmo(other);
        }
    }

    private void PickupAmmo(Collider2D player)
    {
        // Get the PlayerShoot component from the player
        PlayerShooter playerShoot = player.GetComponent<PlayerShooter>();

        if (playerShoot != null)
        {
            // Call AddAmmo to refill ammo
            playerShoot.AddAmmo();
            Debug.Log("Ammo refilled!");

            // Destroy the ammo pickup object
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("PlayerShoot component not found on the player!");
        }
    }
}