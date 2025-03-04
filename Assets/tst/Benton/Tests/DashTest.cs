using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class DashTest
{
    private Vector2 startPosition = new Vector2(-21, 0); // Middle of the screen
    private float moveSpeed = 2f;
    private float dashSpeed = 12f;
    private float speedIncrement = 2f;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnityTest]
    public IEnumerator DashingTest()
    {
        var player = GameObject.FindObjectOfType<PlayerMovement>();
        Assert.NotNull(player, "PlayerMovement not found.");

        while (true)
        {
            player.transform.position = startPosition; // Reset player to middle
            float elapsedTime = 0f;

            while (player.transform.position.x > -10) // Move left until out of bounds
            {
                player.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                Debug.Log($"Player Position: {player.transform.position}, Speed: {moveSpeed}, Dash Speed: {dashSpeed}");
                
                if (elapsedTime > 1f) // Dash every 1 second
                {
                    player.Dash();
                    elapsedTime = 0f;
                }
                
                yield return null;
            }

            dashSpeed += speedIncrement; // Increase dash speed on respawn
        }
    }
}
