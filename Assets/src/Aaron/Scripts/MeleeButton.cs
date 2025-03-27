using UnityEngine;
using UnityEngine.UI;

public class MeleeButton : MonoBehaviour
{
    public Button meleeButton;  // Assign this in the Inspector
    public UpdMelee updMelee;

        void Start()
    {
        // Hide the button if not on mobile
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            meleeButton.gameObject.SetActive(false);
        }
    }

    public void ButtonMelee() 
    {
        updMelee.TriggerAttack();

    }
}
