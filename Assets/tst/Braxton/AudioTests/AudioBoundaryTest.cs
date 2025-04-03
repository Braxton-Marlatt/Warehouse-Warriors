using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AudioBoundaryTest: MonoBehaviour
{
    private AudioClip audioClip;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    // Test minimum and maximum volume
    [UnityTest]
    public IEnumerator VolumeBoundaryTest()
    {
        float[] testVolumes = { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };

        foreach (float volume in testVolumes)
        {
            SoundEffectManager.Instance.SetVolume(SoundEffectManager.Instance.audioSources, volume);
            Assert.AreEqual(volume, SoundEffectManager.Instance.GetVolume("playershoot", SoundEffectManager.Instance.audioSources), $"Volume {volume} was not set correctly.");
            SoundEffectManager.Instance.PlaySound("playershoot", SoundEffectManager.Instance.audioSources);
            Assert.AreEqual(volume, SoundEffectManager.Instance.GetVolume("playershoot", SoundEffectManager.Instance.audioSources), $"Volume {volume} was not set correctly.");
            yield return new WaitForSeconds(1.0f); // Wait for a frame to ensure the volume is set
        }
    }

    [UnityTest]
    public IEnumerator VolumePitchTest(){
        float[] testPitches = { 0.0f, 0.5f, 1.0f, 2.0f, 4.0f, 8.0f, 16.0f, 32.0f, 64.0f, 128.0f, 256.0f, 512.0f, 1024.0f, 2048.0f, 4096.0f, 8192.0f };

        foreach (float pitch in testPitches)
        {
            SoundEffectManager.Instance.SetPitch(SoundEffectManager.Instance.audioSources, pitch);
            Assert.AreEqual(pitch, SoundEffectManager.Instance.GetPitch("playershoot", SoundEffectManager.Instance.audioSources), $"Pitch {pitch} was not set correctly.");
            SoundEffectManager.Instance.PlaySound("playershoot", SoundEffectManager.Instance.audioSources);
            Assert.AreEqual(pitch, SoundEffectManager.Instance.GetPitch("playershhot",SoundEffectManager.Instance.audioSources), $"Pitch {pitch} was not set correctly.");
            yield return new WaitForSeconds(0.5f); // Wait for a frame to ensure the pitch is set
        }
    }
}
