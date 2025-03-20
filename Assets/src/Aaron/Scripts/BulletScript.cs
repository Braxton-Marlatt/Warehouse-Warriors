using UnityEngine;
using UnityEngine.UI;


public class BullettScript : MonoBehaviour
{
    public PlayerShooter playerShooter;
    public Text bulletText;
    private int bulletCount = 0;
    private int maxBullets = 10;

    void Start()
    {
        bulletText.text = "" + playerShooter.getAmmo();
    }

    void Update()
    {
        bulletText.text = "" + playerShooter.getAmmo();
    }
}
