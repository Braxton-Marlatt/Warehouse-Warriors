using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public PlayerHealth player;

    void Update()
    {
        if (player != null && player.GetHealth() <= 0)
        {
            // Scene 4 = Game Over
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(3);
        }
    }
}
