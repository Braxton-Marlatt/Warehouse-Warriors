using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class DashTest
{
    private GameObject player;

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
    }

    [UnityTest]
    public IEnumerator MovementTest()
    {
        Vector3 initialPosition = player.transform.position;
        float moveSpeed = 5f;
        float moveDuration = 2f;
        float elapsedTime = 0f;

        // Move left
        while (elapsedTime < moveDuration)
        {
            player.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = player.transform.position;
        Assert.Less(finalPosition.x, -21f, "Player did not move out of the screen to the left");

        // Reset player position
        player.transform.position = initialPosition;
        elapsedTime = 0f;

        // Move right
        // while (elapsedTime < moveDuration)
        // {
        //     player.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        //     elapsedTime += Time.deltaTime;
        //     yield return null;
        // }

        // finalPosition = player.transform.position;
        // Assert.Greater(finalPosition.x, 21f, "Player did not move out of the screen to the right");

        // // Reset player position
        // player.transform.position = initialPosition;
        // elapsedTime = 0f;

        // Move up
        while (elapsedTime < moveDuration)
        {
            player.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        finalPosition = player.transform.position;
        Assert.Less(finalPosition.y, 6f, "Player did not move out of the screen upwards");

        // Reset player position
        player.transform.position = initialPosition;
        elapsedTime = 0f;

        // Move down
        while (elapsedTime < moveDuration)
        {
            player.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        finalPosition = player.transform.position;
        Assert.Greater(finalPosition.y, -5f, "Player did not move out of the screen downwards");
    }
}