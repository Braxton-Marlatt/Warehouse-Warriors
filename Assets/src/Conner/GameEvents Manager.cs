//Subject
// This static class acts as the event broadcaster for major game events
// In this case, we use it to signal when the player dies
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents//Super Class
{
    // Event for when the player dies
    public static event Action OnPlayerDeath;

    // Call this method to trigger the OnPlayerDeath event
    public virtual void PlayerDied() // Virtual - Declares that a method can be overridden by a subclass.
    {
        Debug.Log("[GameEvents] PlayerDied event triggered.");
        OnPlayerDeath?.Invoke(); // Safely invoke the event only if there are listeners
        //Loads Scene 3 
    }
}

public class GameEventsSubClass : GameEvents //Sub-class
{
    // Call this method to trigger the OnPlayerDeath event

    //public override void PlayerDied() //- //Override - Indicates that the method is replacing a virtual method in the base class.
    //{
    //    Debug.Log("** PlayerDied function overridden **");
    //    SceneManager.LoadScene(2); //Loads help menu

    //}
}
