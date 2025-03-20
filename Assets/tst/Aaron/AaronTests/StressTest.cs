using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Diagnostics;

public class BulletHUDStressTest
{
    private GameObject playerObject;
    private BullettScript bulletScript;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        if (SceneManager.GetActiveScene().name != "TestScene")
            SceneManager.LoadScene("TestScene");

        yield return new WaitForSecondsRealtime(0.5f);

        playerObject = GameObject.FindObjectOfType<PlayerShooter>()?.gameObject;
        Assert.IsNotNull(playerObject, "PlayerShooter not found in scene!");

        bulletScript = playerObject.GetComponent<BullettScript>();
        Assert.IsNotNull(bulletScript, "BullettScript component missing!");
    }

    [UnityTest]
    public IEnumerator BulletHUDStressTest_AmmoIncrease()
    {

        float delay = 0.5f;             // Start slow
        float minDelay = 0.05f;         // Minimum delay between updates
        float decayFactor = 0.95f;      // Speed increases ~5% faster per cycle
        float fpsThreshold = 20f;       // FPS threshold for performance check
        int cycles = 20;

        var stopwatch = new Stopwatch();

        // Initial ammo count
        int ammoCount = bulletScript.playerShooter.getAmmo();
        UnityEngine.Debug.Log($"Starting ammo count: {ammoCount}");

        for (int i = 0; i < cycles; i++)
        {
            stopwatch.Restart();

            // Increase ammo rapidly
            bulletScript.playerShooter.AddAmmo(); // Increase ammo by 1000 per cycle
            yield return new WaitForSecondsRealtime(delay);

            // Check if HUD updated correctly
            string displayedAmmo = bulletScript.bulletText.text;
            Assert.AreEqual(displayedAmmo, bulletScript.playerShooter.getAmmo().ToString(), $"HUD text mismatch on cycle {i}. Expected: {bulletScript.playerShooter.getAmmo()}, Got: {displayedAmmo}");

            // FPS and cycle time monitoring
            stopwatch.Stop();
            float fps = 1f / Time.unscaledDeltaTime;
            float cycleTime = stopwatch.ElapsedMilliseconds / 1000f;

            UnityEngine.Debug.Log($"[Cycle {i}] Delay: {delay:F3}s | FPS: {fps:F1} | Iteration Time: {cycleTime:F3}s");

            if (fps < fpsThreshold)
            {
                Assert.Fail($"TEST FAILED: FPS dropped below {fpsThreshold} at cycle {i}. FPS: {fps:F1}");
            }

            // Exponential acceleration for next cycle
            delay = Mathf.Max(minDelay, delay * decayFactor);
        }

        UnityEngine.Debug.Log("Bullet HUD stress test completed successfully.");
    }
}
