using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthEffect", menuName = "Pickup Effects/Health")]
public class HealthEffect : MonoBehaviour, IPickupEffect
{
    [SerializeField] private int healAmount = 1;

    public void ApplyEffect(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null) health.Heal();
    }
}