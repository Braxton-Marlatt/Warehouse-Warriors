using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AudioStressTest : MonoBehaviour
{
    private AudioClip audioClip;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Wait for the scene to load
        yield return new WaitForSeconds(1.0f);

        // Load an audio clip
        audioClip = Resources.Load<AudioClip>("shoot-noise");
        Assert.IsNotNull(audioClip, "Audio clip not found.");
    }

    // Stress test for playing multiple audio sources
    [UnityTest]
    public IEnumerator ConcurrentAudioStressTest()
    {
        int maxSources = 1000; // Number of concurrent audio sources
        GameObject[] audioObjects = new GameObject[maxSources];
        AudioSource[] audioSources = new AudioSource[maxSources];

        // Create multiple GameObjects with AudioSource components
        for (int i = 0; i < maxSources; i++)
        {
            audioObjects[i] = new GameObject($"AudioObject_{i}");
            audioSources[i] = audioObjects[i].AddComponent<AudioSource>();
            audioSources[i].clip = audioClip;
        }

        // Play all audio sources concurrently
        for (int i = 0; i < maxSources; i++)
        {
            audioSources[i].Play();

        }

        yield return new WaitForSeconds(1.0f); // Wait for 1 second to allow audio to play

        // Assert that all audio sources are playing
        for (int i = 0; i < maxSources; i++)
        {
            Assert.IsTrue(audioSources[i].isPlaying, $"Audio source {i} is not playing.");
        }

        // Wait for the audio to finish
        yield return new WaitForSeconds(audioClip.length);

        // Assert that all audio sources are no longer playing
        for (int i = 0; i < maxSources; i++)
        {
            Assert.IsFalse(audioSources[i].isPlaying, $"Audio source {i} is still playing.");
        }

        // Clean up the created GameObjects
        for (int i = 0; i < maxSources; i++)
        {
            Object.Destroy(audioObjects[i]);
        }
    }
}
