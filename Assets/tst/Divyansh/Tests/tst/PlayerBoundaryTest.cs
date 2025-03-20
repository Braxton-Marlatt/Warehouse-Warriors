using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerBoundaryTest
{
    private GameObject player;
    private Rigidbody2D playerRb;

    private Vector2 minBounds = new Vector2(-27.77f, -5f);  // Adjust as per map size
    private Vector2 maxBounds = new Vector2(-10.01f, 5f);

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForSeconds(1); // Ensure scene is loaded
        player = GameObject.FindWithTag("Player");
        Assert.NotNull(player, "Player not found!");

        playerRb = player.GetComponent<Rigidbody2D>();
        Assert.NotNull(playerRb, "Rigidbody2D not found on Player!");
    }

    [UnityTest]
    public IEnumerator PlayerCannotMoveOutsideBoundary()
    {
        yield return MovePlayer(new Vector2(minBounds.x - 2f, playerRb.position.y)); // Move left out of bounds
        Assert.GreaterOrEqual(playerRb.position.x, minBounds.x, "Player moved past left boundary!");

        yield return MovePlayer(new Vector2(maxBounds.x + 2f, playerRb.position.y)); // Move right out of bounds
        Assert.LessOrEqual(playerRb.position.x, maxBounds.x, "Player moved past right boundary!");

        yield return MovePlayer(new Vector2(playerRb.position.x, minBounds.y - 2f)); // Move down out of bounds
        Assert.GreaterOrEqual(playerRb.position.y, minBounds.y, "Player moved past bottom boundary!");

        yield return MovePlayer(new Vector2(playerRb.position.x, maxBounds.y + 2f)); // Move up out of bounds
        Assert.LessOrEqual(playerRb.position.y, maxBounds.y, "Player moved past top boundary!");
    }

    private IEnumerator MovePlayer(Vector2 targetPosition)
    {
        playerRb.position = targetPosition;
        yield return new WaitForFixedUpdate();
    }
}
