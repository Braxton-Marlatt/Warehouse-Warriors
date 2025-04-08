using UnityEngine;

[CreateAssetMenu(fileName = "NewCoinEffect", menuName = "Pickup Effects/Coin")]
public class CoinEffect : ScriptableObject, IPickupEffect
{
    [SerializeField] private int coins = 1;

    public void ApplyEffect(GameObject player)
    {
        if(coins < 0){
        coins = 1;
        }
        FindAnyObjectByType<CoinManager>().AddCoin();
        Debug.Log("Coin picked up");
    }
}