using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTest
{
    private PlayerHealth player;
    private GameObject playerObject;

    [OneTimeSetUp]
    public void LoadScene()
    {
        // Ensure the scene is loaded before tests begin
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    [System.Obsolete]
    public IEnumerator Setup()
    {
        // Wait a bit to make sure the scene has time to initialize and load components
        yield return new WaitForSeconds(1f);

        // Ensure PlayerHealth is found in the scene
        player = GameObject.FindObjectOfType<PlayerHealth>();
        playerObject = player?.gameObject;

        // Check if the PlayerHealth component is found in the scene
        Assert.NotNull(player, "PlayerHealth component not found in the scene.");
    }

    [UnityTest]
    public IEnumerator PlayerHurtTest()
    {
        Assume.That(player != null, "PlayerHealth was not found, skipping test.");

        // Reduce health to 1
        while (player.GetHealth() > 1)
        {
            player.Hurt(1);
            yield return null; // Wait one frame for Hurt to process
        }

        // Test inside boundary: health = 1 → 0
        player.Hurt(1);
        yield return null; // Wait one frame for the final Hurt to process
    }
}
