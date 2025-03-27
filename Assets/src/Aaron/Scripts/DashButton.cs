using UnityEngine;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
{
    public Button dashButton;  // Assign this in the Inspector
    public PlayerMovement playerMovement;

    void Start()
    {
        // Hide the button if not on mobile
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            dashButton.gameObject.SetActive(false);
        }
    }

    public void ButtonDash() 
    {
        playerMovement.Dash();

    }
}
