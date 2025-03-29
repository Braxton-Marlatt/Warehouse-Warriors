using UnityEngine;
using UnityEngine.UI;


public class BullettScript : MonoBehaviour
{
    public PlayerShooter playerShooter;
    public Text bulletText;

    void Start()
    {
        bulletText.text = "" + playerShooter.getAmmo();
    }

    void Update()
    {
        bulletText.text = "" + playerShooter.getAmmo();
    }
}
