using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public PlayerHealth player; // Reference to the player health script

    GameEvents gameEvents = new GameEventsSubClass(); //Creates a new gameEvents object

    void Update()
    {
        // Check if player is assigned and if their health is 0 or less
        if (player != null && player.GetHealth() <= 0)
        {
            Time.timeScale = 1.0f; // Reset time scale in case game was paused

            // Instead of loading the Game Over scene directly we trigger the OnPlayerDeath event from GameEvents

            gameEvents.PlayerDied();// Dynamically binded at run time
            SoundFXManager.Instance.PlaySound("PlayerDeath"); // Play player death sound
        }
    }
}
