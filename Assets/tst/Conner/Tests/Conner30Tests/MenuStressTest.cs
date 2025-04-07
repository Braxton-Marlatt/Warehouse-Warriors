using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Diagnostics;

public class MenuStressTest
{
    PauseManager pauseManager;
    int clicks = 0;
    Stopwatch timer = new Stopwatch();

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        if (SceneManager.GetActiveScene().name != "TestScene")
            SceneManager.LoadScene("TestScene");
        yield return new WaitForSecondsRealtime(0.5f);

        pauseManager = GameObject.FindFirstObjectByType<PauseManager>();
        Assert.IsNotNull(pauseManager, "PauseManager not found.");
    }

    [UnityTest]
    public IEnumerator PauseResumeStressTest()
    {
        float delay = 0.3f, min = 0.03f, decay = 0.90f, fpsMin = 20f;
        int cycles = 150;
        timer.Start();

        for (int i = 0; i < cycles; i++)
        {
            pauseManager.pauseGame(); clicks++;
            yield return new WaitForSecondsRealtime(delay);
            pauseManager.resumeGame(); clicks++;
            yield return new WaitForSecondsRealtime(delay);

            float fps = 1f / Time.unscaledDeltaTime;
            float cps = clicks / (timer.ElapsedMilliseconds / 1000f);

            UnityEngine.Debug.Log($"CPS: {cps:F2} | FPS: {fps:F1}");

            // Simulated fake FPS drop OR intentional failure
            if (fps < fpsMin || i == 80)  // force fail at cycle 80
            {
                LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(".*FPS dropped.*"));
                LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(".*CPS.*"));

                UnityEngine.Debug.LogError($"FPS dropped below {fpsMin}. FPS: {fps:F1}");
                UnityEngine.Debug.LogError($"CPS: {cps:F2}");
                Assert.Fail("Forced failure (simulated low FPS or intentional break).");
            }

            delay = Mathf.Max(min, delay * decay);
        }

        float finalCps = clicks / (timer.ElapsedMilliseconds / 1000f);
        UnityEngine.Debug.Log($"Test complete. CPS: {finalCps:F2}");
    }
}