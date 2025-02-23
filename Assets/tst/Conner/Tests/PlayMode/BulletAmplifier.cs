using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BulletAmplifier
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }
    [UnityTest]
    public IEnumerator RoofTest()
    {
        float testDuration = 10f; // time to run test
        float startTime = Time.time; // start time of test
        while (Time.time - startTime < testDuration)
        {
            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (bullet.transform.position.y > 5.37f)
                {
                    Debug.LogError("Bullet went past ceiling.");
                    Assert.Fail("Not supposed to clip through.");
                    yield break;
                }
            }
            yield return null; // Check every frame
        }

        Debug.Log("Roof test passed.");
    }

    [UnityTest]
    public IEnumerator FPSDropTest()
    {
        float testDuration = 10f; // Test runs for 10 seconds
        float minAllowedFPS = 60f; // FPS threshold
        bool fpsDropped = false; // Track if FPS drops

        while (Time.time < testDuration)
        {
            if (1f / Time.deltaTime < minAllowedFPS) // Check FPS
            {
                fpsDropped = true;
                break; // Stop early if FPS drops
            }
            yield return null; // Check every frame
        }

        if (!fpsDropped) Assert.Fail("FPS never dropped below 60.");
    }
}
