 //Subject
 // This static class acts as the event broadcaster for major game events
// In this case, we use it to signal when the player dies
using System;
using UnityEngine;

public static class GameEvents
{
    // Event for when the player dies
    public static event Action OnPlayerDeath;

    // Call this method to trigger the OnPlayerDeath event
    public static void PlayerDied()
    {
        Debug.Log("[GameEvents] PlayerDied event triggered.");
        OnPlayerDeath?.Invoke(); // Safely invoke the event only if there are listeners
    }
}