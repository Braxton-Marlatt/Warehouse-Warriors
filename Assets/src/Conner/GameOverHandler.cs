// (Observer)
// This script listens for the OnPlayerDeath event and responds accordingly
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    // Subscribes to the OnPlayerDeath event when the object becomes active
    private void OnEnable()
    {
        GameEvents.OnPlayerDeath += HandleGameOver;
    }

    // Unsubscribes to prevent memory leaks when object is disabled/destroyed
    private void OnDisable()
    {
        GameEvents.OnPlayerDeath -= HandleGameOver;
    }

    // This method is triggered when the OnPlayerDeath event is invoked
    private void HandleGameOver()
    {
        Debug.Log("[GameOverHandler] Player died. Loading Game Over scene...");
        SceneManager.LoadScene(3);// Assumes Scene 3 is the Game Over scene
        SoundFXManager.Instance.PlaySound("PlayerDeath"); // Play player death sound
    }
}
