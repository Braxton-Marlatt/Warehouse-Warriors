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

    public void AddCoin()
    {
        coinCount++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collided with: {other.name}");
        if(other.CompareTag("Coin"))
        {
            AddCoin();
            Destroy(other.gameObject);
        }
    }
}
