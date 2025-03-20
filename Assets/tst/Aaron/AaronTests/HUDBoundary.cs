using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class HUDBoundary
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private GameObject hud;
    private HeartScript heartScript;
    private Image[] hearts;

    [SetUp]
    public void Setup()
    {
        // Get the first object of type PlayerHealth in the scene
        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        if (playerHealth == null)
        {
            player = new GameObject("Player");
            playerHealth = player.AddComponent<PlayerHealth>();
        }
        else
        {
            player = playerHealth.gameObject;
        }

        // Ensure the player has a SpriteRenderer
        if (!player.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer = player.AddComponent<SpriteRenderer>();
        }

        playerHealth.maxHealth = 5;
        playerHealth.SetHealth(3);

        // Create HUD and assign HeartScript
        hud = new GameObject("HUD");
        heartScript = hud.AddComponent<HeartScript>();
        heartScript.playerHealth = playerHealth;

        // Create heart images
        hearts = new Image[playerHealth.maxHealth];
        for (int i = 0; i < hearts.Length; i++)
        {
            GameObject heartObj = new GameObject("Heart" + i);
            hearts[i] = heartObj.AddComponent<Image>();
        }
        heartScript.hearts = hearts;
    }

    [UnityTest]
public IEnumerator HUDBoundary_HeartUI_HandlesEdgesCorrectly()
{
    // Ensure no unexpected log errors
    //LogAssert.NoUnexpectedReceived();

    // Test: Minimum Health (0)
    playerHealth.SetHealth(0);
    heartScript.UpdateHearts();
    foreach (var heart in hearts)
    {
        Assert.AreEqual(heartScript.emptyHeart, heart.sprite, "Heart UI failed at 0 health");
    }

    // Test: Maximum Health
    playerHealth.SetHealth(playerHealth.maxHealth);
    heartScript.UpdateHearts();
    foreach (var heart in hearts)
    {
        Assert.AreEqual(heartScript.fullHeart, heart.sprite, "Heart UI failed at max health");
    }

    // Test: Health Just Below Max
    playerHealth.SetHealth(playerHealth.maxHealth - 1);
    heartScript.UpdateHearts();
    for (int i = 0; i < hearts.Length; i++)
    {
        Assert.AreEqual(i < playerHealth.GetHealth() ? heartScript.fullHeart : heartScript.emptyHeart, 
                        hearts[i].sprite, $"Heart UI failed at {playerHealth.GetHealth()} health");
    }

    // Test: Health at 1
    playerHealth.SetHealth(1);
    heartScript.UpdateHearts();
    for (int i = 0; i < hearts.Length; i++)
    {
        Assert.AreEqual(i < playerHealth.GetHealth() ? heartScript.fullHeart : heartScript.emptyHeart, 
                        hearts[i].sprite, "Heart UI failed at 1 health");
    }

    yield return null;
}

}