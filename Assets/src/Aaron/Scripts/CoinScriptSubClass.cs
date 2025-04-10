using UnityEngine;

public class CoinScriptSubClass : CoinScripts
{
    // This class inherits from CoinScripts and overrides its Update() method.
    // The 'override' keyword replaces the base class Update() behavior with custom behavior in this subclass.
    // This demonstrates dynamic binding — at runtime, Unity will call this version of Update() if this subclass is used.

    public override void Update()
    {
        // Call the base class Update() to preserve its functionality
        base.Update();

        // Add additional behavior specific to this subclass
        coinText.text = coinManager.GetCoinCount() + " $$$$";
        Debug.Log("Testing Dynamic Binding");
    }
}
