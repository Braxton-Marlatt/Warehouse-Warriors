//using System.Collections;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;
//using UnityEngine.SceneManagement;
//using System.Runtime.CompilerServices;


//public class BulletAmplifier
//{
//    [OneTimeSetUp]
//    public void LoadScene()
//    {
//        SceneManager.LoadScene("TestScene");
//    }
//    public class MagazineTest
//    {
//        [Test]
//        public void MagazineTestSimplePasses()
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
//        }


//    }

//}