using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public int coinCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void AddCoin()
    {
        Debug.Log("Coin Count Increased by 1");
        coinCount++;
    }

}
