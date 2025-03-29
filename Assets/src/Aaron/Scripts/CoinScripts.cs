using UnityEngine;
using UnityEngine.UI;


public class CoinScripts : MonoBehaviour
{
    public CoinManager coinManager;
    public Text coinText;

    void Start()
    {
        coinText.text = "$" + coinManager.GetCoinCount();
    }

    void Update()
    {
        coinText.text = "$" + coinManager.GetCoinCount();
    }
}
