using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Diagnostics;

public class HeartHUDStressTest
{
    private GameObject playerObject;
    private HeartScript heartScript;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        if (SceneManager.GetActiveScene().name != "Game")
            SceneManager.LoadScene("Game");

        yield return new WaitForSecondsRealtime(0.5f);

        playerObject = GameObject.FindObjectOfType<PlayerHealth>()?.gameObject;
        Assert.IsNotNull(playerObject, "PlayerHealth not found in scene!");

        heartScript = playerObject.GetComponent<HeartScript>();
        Assert.IsNotNull(heartScript, "HeartScript component missing!");
    }

    [UnityTest]
    public IEnumerator HeartHUDStressTest_HealthChanges()
    {
        // Variables for stress test
        float delay = 0.001f;             // Extremely short delay to force updates to happen fast
        float minDelay = 0.0001f;         // Ultra low minimum delay between updates
        float decayFactor = 0.90f;        // Speed up the update process even more
        float fpsThreshold = 600f;        // FPS threshold for failure at 500 FPS
        int cycles = 1000;                // Number of cycles to run

        var stopwatch = new Stopwatch();

        // Initial heart count
        int maxHealth = heartScript.playerHealth.maxHealth;
        UnityEngine.Debug.Log($"Starting max health: {maxHealth}");

        // Wait for 4 seconds to let everything load
        yield return new WaitForSecondsRealtime(4f);

        // Track the initial FPS (should be around 1200)
        float initialFps = 1f / Time.unscaledDeltaTime;
        UnityEngine.Debug.Log($"Initial FPS: {initialFps:F1}");

        // Randomize health toggle between 0 and 12
        System.Random random = new System.Random();

        for (int i = 0; i < cycles; i++)
        {
            stopwatch.Restart();

            // Rapidly change health to random values between 0 and 12
            for (int j = 0; j < 500; j++) // Rapid changes within each cycle
            {
                int randomHealth = random.Next(0, 13);  // Random number between 0 and 12
                heartScript.playerHealth.SetHealth(randomHealth);  // Set random health
                yield return new WaitForSecondsRealtime(delay);
            }

            // Monitor FPS and cycle time to observe any significant lag/drop
            stopwatch.Stop();
            float fps = 1f / Time.unscaledDeltaTime;
            float cycleTime = stopwatch.ElapsedMilliseconds / 1000f;

            UnityEngine.Debug.Log($"[Cycle {i}] Delay: {delay:F5}s | FPS: {fps:F1} | Iteration Time: {cycleTime:F3}s");

            // Check if FPS dropped to below the threshold after rapid changes
            if (fps < fpsThreshold)
            {
                Assert.Fail($"TEST FAILED: FPS dropped below {fpsThreshold} at cycle {i}. FPS: {fps:F1}");
            }

            // Exponential acceleration for next cycle
            delay = Mathf.Max(minDelay, delay * decayFactor);
        }

        // Track FPS after the test and confirm it dropped
        float finalFps = 1f / Time.unscaledDeltaTime;
        UnityEngine.Debug.Log($"Final FPS after 1000 cycles: {finalFps:F1}");

        Assert.Less(finalFps, 500f, $"Final FPS was too high: {finalFps:F1}, it should have dropped below 500");

        UnityEngine.Debug.Log("Heart HUD stress test completed successfully.");
    }
}
