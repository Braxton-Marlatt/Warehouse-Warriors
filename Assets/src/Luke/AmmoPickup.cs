using UnityEngine;

public class AmmoPickup : BasePickup
{
    public int ammoAmount = 10;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerShooter playerShoot = player.GetComponent<PlayerShooter>();
        if (playerShoot != null)
        {
            playerShoot.AddAmmo();
            Debug.Log("Ammo refilled!");
        }
        else
        {
            Debug.LogError("PlayerShoot component not found!");
        }
    }
}