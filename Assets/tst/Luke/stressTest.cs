using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class StressTests
{
    [UnityTest]
    [System.Obsolete]
    public static IEnumerator Mass_Enemy_Death_Stress_Test()
    {
        const int enemyCount = 100;

        // Create multiple enemies
        for (int i = 0; i < enemyCount; i++)
        {
            var e = new GameObject($"Enemy_{i}");
            var health = e.AddComponent<EnemyHealth>();
            e.AddComponent<Enemy>();
            health.Hurt(1000); // Kill immediately
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // Simple check - just verify no errors occurred
        var hearts = GameObject.FindObjectsOfType<HeartPickup>();
        Assert.IsTrue(hearts.Length > 0, "No hearts dropped at all");
    }
}
