using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthEffect", menuName = "Pickup Effects/Health")]
public class HealthEffect : ScriptableObject, IPickupEffect
{
    [SerializeField] private int healAmount = 1;

    public void ApplyEffect(GameObject player)
    {
        if(healAmount < 0){
        healAmount = 1;
        }

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null) health.Heal();
    }
}