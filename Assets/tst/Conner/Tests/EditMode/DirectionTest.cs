using NUnit.Framework;
using UnityEngine;

public class DirectionTest
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        // Create GameObject
        playerObject = new GameObject();

        // Add PlayerMovement script
        playerMovement = playerObject.AddComponent<PlayerMovement>();

        // Ensure Rigidbody2D is attached
        Rigidbody2D rb = playerObject.AddComponent<Rigidbody2D>();

        // Manually initialize the rb variable in PlayerMovement
        playerMovement.GetType()
            .GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerMovement, rb);
    }

    [TearDown]
    public void Teardown()
    {
        // Destroy the GameObject after each test
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void PlayerStartsWithZeroVelocity()
    {
        Assert.NotNull(playerMovement, "playerMovement is null");
        Assert.NotNull(playerMovement.GetComponent<Rigidbody2D>(), "Rigidbody2D is missing");

        // Rigidbody should have zero velocity at start
        Assert.AreEqual(Vector2.zero, playerMovement.GetComponent<Rigidbody2D>().linearVelocity);
    }


    [Test]
    public void NorthDirectionTest()
    {
        // Ensure playerMovement is not null
        Assert.NotNull(playerMovement, "playerMovement is null in NorthDirectionTest");

        // Simulate moving north
        playerMovement.moveDirection = new Vector2(0, 1);
        playerMovement.MovePlayer();

        // Assert movement direction
        Assert.AreEqual(new Vector2(0, 1), playerMovement.moveDirection);
    }

    [Test]
    public void SouthDirectionTest()
    {
        Assert.NotNull(playerMovement, "playerMovement is null in SouthDirectionTest");

        playerMovement.moveDirection = new Vector2(0, -1);
        playerMovement.MovePlayer();

        Assert.AreEqual(new Vector2(0, -1), playerMovement.moveDirection);
    }

    [Test]
    public void EastDirectionTest()
    {
        Assert.NotNull(playerMovement, "playerMovement is null in EastDirectionTest");
        playerMovement.moveDirection = new Vector2(1, 0);
        playerMovement.MovePlayer();
        Assert.AreEqual(new Vector2(1, 0), playerMovement.moveDirection);
    }

    [Test]
    public void WestDirectionTest()
    {
        Assert.NotNull(playerMovement, "playerMovement is null in WestDirectionTest");
        playerMovement.moveDirection = new Vector2(-1, 0);
        playerMovement.MovePlayer();
        Assert.AreEqual(new Vector2(-1, 0), playerMovement.moveDirection);
    }

    [Test]
    public void Dash_IncreasesVelocity()
    {
        playerMovement.moveDirection = new Vector2(1, 0); // Move right
        float initialSpeed = playerMovement.moveSpeed;

        playerMovement.Dash(); // Trigger dash

        float expectedSpeed = playerMovement.dashSpeed;
        float actualSpeed = playerMovement.GetComponent<Rigidbody2D>().linearVelocity.magnitude;

        Assert.AreEqual(expectedSpeed, actualSpeed, 0.1f);
    }

}
