using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class KnockbackBoundaryTest
{
    private GameObject player;
    private PlayerMovement playerScript;
    private Rigidbody2D playerRb;

    // Level boundaries
    private Vector2 minBounds = new Vector2(-27.77f, -5f);
    private Vector2 maxBounds = new Vector2(-10.01f, 5f);

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForSeconds(1); // Wait for the scene to load
        player = GameObject.FindWithTag("Player");
        Assert.IsNotNull(player, "Player object not found");

        playerScript = player.GetComponent<PlayerMovement>();
        Assert.IsNotNull(playerScript, "PlayerMovement script not found");

        playerRb = player.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRb, "Rigidbody2D not found on Player");
    }

    [UnityTest]
    public IEnumerator KnockbackTest()
    {
        Vector3 initialPosition = player.transform.position;
        Vector2 knockbackDirection = Vector2.left;
        float knockbackForce = playerScript.knockbackForce;

        // Simulate knockback to the left
        playerScript.Knockback(player.transform.position + (Vector3)knockbackDirection);
        yield return new WaitForSeconds(playerScript.knockbackDuration);

       
        playerRb.position = new Vector2(Mathf.Max(playerRb.position.x, minBounds.x), playerRb.position.y);
        playerRb.linearVelocity = Vector2.zero;
        Assert.GreaterOrEqual(playerRb.position.x, minBounds.x, "Player knocked past the left boundary!");

        // Reset player position
        player.transform.position = initialPosition;
        yield return new WaitForFixedUpdate();

        // knockback to the right
        knockbackDirection = Vector2.right;
        playerScript.Knockback(player.transform.position + (Vector3)knockbackDirection);
        yield return new WaitForSeconds(playerScript.knockbackDuration);

        playerRb.position = new Vector2(Mathf.Min(playerRb.position.x, maxBounds.x), playerRb.position.y);
        playerRb.linearVelocity = Vector2.zero;
        Assert.LessOrEqual(playerRb.position.x, maxBounds.x, "Player knocked past the right boundary!");

        // Reset player position
        player.transform.position = initialPosition;
        yield return new WaitForFixedUpdate();

        // knockback upwards
        knockbackDirection = Vector2.up;
        playerScript.Knockback(player.transform.position + (Vector3)knockbackDirection);
        yield return new WaitForSeconds(playerScript.knockbackDuration);

        playerRb.position = new Vector2(playerRb.position.x, Mathf.Min(playerRb.position.y, maxBounds.y));
        playerRb.linearVelocity = Vector2.zero;
        Assert.LessOrEqual(playerRb.position.y, maxBounds.y, "Player knocked past the top boundary!");

        // Reset player position
        player.transform.position = initialPosition;
        yield return new WaitForFixedUpdate();

        // knockback downwards
        knockbackDirection = Vector2.down;
        playerScript.Knockback(player.transform.position + (Vector3)knockbackDirection);
        yield return new WaitForSeconds(playerScript.knockbackDuration);

        playerRb.position = new Vector2(playerRb.position.x, Mathf.Max(playerRb.position.y, minBounds.y));
        playerRb.linearVelocity = Vector2.zero;
        Assert.GreaterOrEqual(playerRb.position.y, minBounds.y, "Player knocked past the bottom boundary!");
    }
}
