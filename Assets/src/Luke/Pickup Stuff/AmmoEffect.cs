using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmoEffect", menuName = "Pickup Effects/Ammo")]
public class AmmoEffect : MonoBehaviour, IPickupEffect
{
    [SerializeField] private int ammoAmount = 10;

    public void ApplyEffect(GameObject player)
    {
        PlayerShooter shooter = player.GetComponent<PlayerShooter>();
        if (shooter != null) shooter.AddAmmo();
    }
}