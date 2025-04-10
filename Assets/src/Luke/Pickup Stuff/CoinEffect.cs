using UnityEngine;

[CreateAssetMenu(fileName = "NewCoinEffect", menuName = "Pickup Effects/Coin")]
public class CoinEffect : ScriptableObject, IPickupEffect
{
    [SerializeField] private int coins = 1;

    public void ApplyEffect(GameObject player)
    {
        FindObjectOfType<CoinManager>().AddCoin();
        Debug.Log("Coin picked up");
    }
}