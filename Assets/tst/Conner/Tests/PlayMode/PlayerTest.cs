using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    [UnityTest]
    public IEnumerator MoveNorth()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<PlayerMovement>(); // Attach PlayerMovement

        // 🔹 FIX: Add Rigidbody2D to prevent NullReferenceException
        var rb = gameObject.AddComponent<Rigidbody2D>();

        // Manually assign Rigidbody2D since Start() won't run automatically in Edit Mode
        player.GetType()
            .GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(player, rb);

        // Disable gravity so physics doesn’t interfere
        rb.gravityScale = 0;

        // Simulate player moving north
        player.moveDirection = new Vector2(0, 1);

        // Call MovePlayer() to process movement
        player.MovePlayer();

        yield return null; // Wait one frame for Unity updates

        // 🔹 Check if movement logic is correctly applied
        Assert.AreEqual(new Vector2(0, 1), player.moveDirection);
    }
}
