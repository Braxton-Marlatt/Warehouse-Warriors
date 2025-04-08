using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmoEffect", menuName = "Pickup Effects/Ammo")]
public class AmmoEffect : ScriptableObject, IPickupEffect
{
    [SerializeField] private int ammoAmount = 10;

    public void ApplyEffect(GameObject player)
    {
        if (ammoAmount < 0){
        ammoAmount = 10;
        }
        PlayerShooter shooter = player.GetComponent<PlayerShooter>();
        if (shooter != null) shooter.AddAmmo();
    }
}