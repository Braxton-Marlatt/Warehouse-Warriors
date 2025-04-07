using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerHealth player;

    void Update()
    {
        if (player != null && player.GetHealth() <= 0)
        {
            // Scene 4 = Game Over
            Time.timeScale = 1.0f;
            //SceneTracker.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(4);
        }
    }
}
