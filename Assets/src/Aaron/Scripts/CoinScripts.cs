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

    // VIRTUAL METHOD
    // This method is marked as 'virtual' to allow child classes (subclasses) to override it.
    // This enables polymorphism — letting different versions of Update() run depending on the object's actual type.
    // In this base class, Update() simply refreshes the coin count display every frame.
    public virtual void Update()
    {
        coinText.text = "$" + coinManager.GetCoinCount();
    }
}
