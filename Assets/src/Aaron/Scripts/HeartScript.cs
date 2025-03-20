using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{

    public PlayerHealth playerHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
     for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth.GetHealth())
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < playerHealth.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

}

public void UpdateHearts()
{
    Update();
}


}

