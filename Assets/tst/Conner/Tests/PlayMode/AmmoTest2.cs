//using System.Collections;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;
//using UnityEngine.SceneManagement;
//using System.Runtime.CompilerServices;

//public class AmmoTest2
//{
//    [OneTimeSetUp]
//    public IEnumerator LoadScene()
//    {
//        SceneManager.LoadScene("TestScene");

//        // Wait until the scene is fully loaded
//        while (SceneManager.GetActiveScene().name != "TestScene")
//        {
//            yield return null;
//        }
//        // Wait an extra frame to ensure all objects are initialized
//        yield return null;
//    }



//    [UnityTest]
//        public IEnumerator spareAmmo()
//        {
//            var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
//            Assert.NotNull(playerShooter, "PlayerShooter not found.");
//            while (playerShooter.getAmmo() > 1)
//            {
//                playerShooter.Shoot(Vector2.right);
//            }
//            playerShooter.Shoot(Vector2.left);
//            Assert.AreEqual(0, playerShooter.getAmmo());//Inside Boundary Test

//            playerShooter.Shoot(Vector2.left);
//            Assert.AreEqual(0, playerShooter.getAmmo());//Outside Boundary Test
//            yield return null;
//        }
//}


using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AmmoTest2
{
    [UnitySetUp] // Changed from [OneTimeSetUp]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("TestScene");

        // Wait until the scene is fully loaded
        while (!SceneManager.GetSceneByName("TestScene").isLoaded)
        {
            yield return null;
        }

        // Ensure TestScene is the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TestScene"));

        // Wait an extra frame to ensure all objects are initialized
        yield return null;
    }

    [UnityTest]
    public IEnumerator spareAmmo()
    {
        yield return null; // Ensure Unity initializes the scene

        var playerShooter = GameObject.FindObjectOfType<PlayerShooter>();
        Assert.NotNull(playerShooter, "PlayerShooter not found.");

        while (playerShooter.getAmmo() > 1)
        {
            playerShooter.Shoot(Vector2.right);
        }

        playerShooter.Shoot(Vector2.left);
        Assert.AreEqual(0, playerShooter.getAmmo()); // Inside Boundary Test

        playerShooter.Shoot(Vector2.left);
        Assert.AreEqual(0, playerShooter.getAmmo()); // Outside Boundary Test

        yield return null; // Required for UnityTest coroutine
    }
}
