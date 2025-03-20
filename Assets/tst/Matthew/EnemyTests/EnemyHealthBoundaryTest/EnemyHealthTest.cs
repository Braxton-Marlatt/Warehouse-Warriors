using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class EnemyHealthTest
{
    private EnemyHealth enemy;
    private GameObject enemyObject;
    
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    [System.Obsolete]
    public IEnumerator Setup()
    {
        yield return new WaitForSeconds(1f); // Ensure the scene is loaded before accessing objects

        enemy = GameObject.FindObjectOfType<EnemyHealth>();
        enemyObject = enemy?.gameObject;

        Assert.NotNull(enemy, "EnemyHealth component not found in the scene.");
    }

    [UnityTest]
    public IEnumerator EnemyHurtTest()
    {
        Assume.That(enemy != null, "EnemyHealth was not found, skipping test.");

        // Reduce health to 1
        while (enemy.GetHealth() > 1)
        {
            enemy.Hurt();
            yield return null;
        }

        // Test inside boundary: health = 1 → 0
        enemy.Hurt();
        yield return null;

        // Ensure health is zero
        Assert.AreEqual(0, enemy.GetHealth(), "Enemy health should be 0 after last valid hit.");

        // Test object destruction logic
        yield return null; // Allow Unity to process the destruction
        if (enemyObject == null || enemy == null)
        {
            Assert.Pass("Enemy object correctly destroyed after reaching 0 health.");
        }
        else
        {
            // Test outside boundary: avoid calling Hurt() on a destroyed object
            enemy.Hurt();
            yield return null;
            Assert.AreEqual(0, enemy.GetHealth(), "Enemy health should remain at 0 when further hurt.");
        }
    }
}
