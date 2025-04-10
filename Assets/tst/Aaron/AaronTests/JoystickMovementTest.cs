using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using System.Collections;

public class JoystickMovementTest
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Joystick joystick;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        // Set up player GameObject with Rigidbody2D and PlayerMovement
        player = new GameObject("Player");
        rb = player.AddComponent<Rigidbody2D>();
        playerMovement = player.AddComponent<PlayerMovement>();
        playerMovement.moveSpeed = 5f;

        // Set up mock joystick
        GameObject joystickGO = new GameObject("Joystick", typeof(RectTransform));
        joystick = joystickGO.AddComponent<Joystick>();

        // Fake mobile setup
        playerMovement.joystick = joystick;
        typeof(PlayerMovement).GetField("isPhone", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerMovement, true);
    }

    [UnityTest]
    public IEnumerator JoystickInput_MovesPlayer()
    {
        // Simulate joystick input to the right
        Vector2 simulatedInput = new Vector2(1f, 0f);
        typeof(Joystick).GetField("input", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(joystick, simulatedInput);

        playerMovement.MovePlayer();

        yield return new WaitForFixedUpdate(); // Wait for physics update

        Assert.AreEqual(Vector2.right * playerMovement.moveSpeed, rb.linearVelocity, "Player did not move right as expected from joystick input.");
    }
}
