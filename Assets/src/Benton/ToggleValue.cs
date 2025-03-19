using UnityEngine;
using UnityEngine.UI;

public class ToggleValue : MonoBehaviour
{
    public Toggle toggle;
    
    // Public static variable to hold the toggle state
    public static bool isToggleOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load the saved toggle state
        isToggleOn = PlayerPrefs.GetInt("ToggleState", 0) == 1;
        toggle.isOn = isToggleOn;

        // Add a listener to update the static variable when the toggle state changes
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        // Save the toggle state to PlayerPrefs
        PlayerPrefs.SetInt("ToggleState", isOn ? 1 : 0);
        
        // Update the static variable
        isToggleOn = isOn;
    }
}
