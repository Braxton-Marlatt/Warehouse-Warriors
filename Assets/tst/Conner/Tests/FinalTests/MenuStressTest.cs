using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Diagnostics;

public class MenuStressTest
{
    private GameObject pauseManagerObject;
    private PauseManager pauseManager;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        if (SceneManager.GetActiveScene().name != "TestScene")
            SceneManager.LoadScene("TestScene");

        yield return new WaitForSecondsRealtime(0.5f);

        pauseManagerObject = GameObject.FindObjectOfType<PauseManager>()?.gameObject;
        Assert.IsNotNull(pauseManagerObject, "PauseManager not found in scene!");

        pauseManager = pauseManagerObject.GetComponent<PauseManager>();
        Assert.IsNotNull(pauseManager, "PauseManager component missing!");
    }

    [UnityTest]
    public IEnumerator MenuStressTest_PauseResume_AggressiveAcceleration()
    {
        float delay = 0.5f;           // Start slow
        float minDelay = 0.05f;      // Stop at this speed
        float decayFactor = 0.95f;    // Speed increases ~15% faster per cycle
        float fpsThreshold = 20f;
        int cycles = 20;

        var stopwatch = new Stopwatch();

        for (int i = 0; i < cycles; i++)
        {
            stopwatch.Restart();

            pauseManager.pauseGame();
            yield return new WaitForSecondsRealtime(delay);
            Assert.IsTrue(PauseManager.isPaused, $"❌ Failed to pause on cycle {i}");

            pauseManager.resumeGame();
            yield return new WaitForSecondsRealtime(delay);
            Assert.IsFalse(PauseManager.isPaused, $"❌ Failed to resume on cycle {i}");

            stopwatch.Stop();

            float fps = 1f / Time.unscaledDeltaTime;
            float cycleTime = stopwatch.ElapsedMilliseconds / 1000f;

            UnityEngine.Debug.Log($"[Cycle {i}] Delay: {delay:F3}s | FPS: {fps:F1} | Iteration Time: {cycleTime:F3}s");

            if (fps < fpsThreshold)
            {
                Assert.Fail($"❌ TEST FAILED: FPS dropped below {fpsThreshold} at cycle {i}. FPS: {fps:F1}");
            }

            // Exponential acceleration
            delay = Mathf.Max(minDelay, delay * decayFactor);
        }

        UnityEngine.Debug.Log("✅ Stress test completed successfully.");
    }
}
