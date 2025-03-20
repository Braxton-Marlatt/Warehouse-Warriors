using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CameraStress
{
    private RoomCamera roomCamera;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnityTest]
    public IEnumerator CameraShakeStress()
    {
        yield return new WaitForSeconds(1);

        roomCamera = GameObject.FindFirstObjectByType<RoomCamera>();
        Assert.NotNull(roomCamera, "RoomCamera not found in scene");

        bool isDone = false;
        float startTime = Time.time;

        while (!isDone)
        {
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"ShakeAmount: {roomCamera.speed}, FPS: {fps}");

            if (fps < 60)
            {
                Assert.Fail("FPS dropped below acceptable limit due to camera processing.");
                isDone = true;
            }

            yield return null;
        }
    }
}