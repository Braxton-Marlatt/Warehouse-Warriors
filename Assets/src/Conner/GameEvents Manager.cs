 //Subject
 // This static class acts as the event broadcaster for major game events
// In this case, we use it to signal when the player dies
using System;
using UnityEngine;

public class GameEvents//Super Class
{
    // Event for when the player dies
    public static event Action OnPlayerDeath;

    // Call this method to trigger the OnPlayerDeath event
    public virtual void PlayerDied()
    {
        Debug.Log("[GameEvents] PlayerDied event triggered.");
        OnPlayerDeath?.Invoke(); // Safely invoke the event only if there are listeners
    }
}

public class GameEventsSubClass : GameEvents //Sub-class
{
    // Call this method to trigger the OnPlayerDeath event
    public override void PlayerDied()
    {
        Debug.Log("** PlayerDied function overridden **");
        base.PlayerDied(); // Safely invoke the event only if there are listeners

        //Add or change code here to do something different
    }
}