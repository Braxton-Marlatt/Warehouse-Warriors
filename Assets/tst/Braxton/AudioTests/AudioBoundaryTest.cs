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
        SceneManager.LoadScene("Game");
    }

    // Test minimum and maximum volume
    [UnityTest]
    public IEnumerator SoundFXVolumeBoundaryTest()
    {
        float[] testVolumes = { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };

        foreach (float volume in testVolumes)
        {
            SoundFXManager.Instance.SetVolume(volume);
            Assert.AreEqual(volume, SoundFXManager.Instance.GetVolume("PlayerShoot"), $"Volume {volume} was not set correctly.");
            SoundFXManager.Instance.PlaySound("PlayerShoot");
            Assert.AreEqual(volume, SoundFXManager.Instance.GetVolume("PlayerShoot"), $"Volume {volume} was not set correctly.");
            yield return new WaitForSeconds(1.0f); // Wait for a frame to ensure the volume is set
        }
    }

    [UnityTest]
    public IEnumerator MusicVolumeBoundaryTest()
    {
        float[] testVolumes = { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };

        foreach (float volume in testVolumes)
        {
            MusicManager.Instance.SetVolume(volume);
            Assert.AreEqual(volume, MusicManager.Instance.GetVolume("PlayerShoot"), $"Volume {volume} was not set correctly.");
            MusicManager.Instance.PlaySound("PlayerShoot");
            Assert.AreEqual(volume, MusicManager.Instance.GetVolume("PlayerShoot"), $"Volume {volume} was not set correctly.");
            yield return new WaitForSeconds(1.0f); // Wait for a frame to ensure the volume is set
        }
    }

    [UnityTest]
    public IEnumerator VolumePitchTest(){
        float[] testPitches = { 0.0f, 0.5f, 1.0f, 2.0f, 4.0f, 8.0f, 16.0f, 32.0f, 64.0f, 128.0f, 256.0f, 512.0f, 1024.0f, 2048.0f, 4096.0f, 8192.0f };

        foreach (float pitch in testPitches)
        {
            SoundFXManager.Instance.SetPitch(pitch);
            Assert.AreEqual(pitch, SoundFXManager.Instance.GetPitch("PlayerShoot"), $"Pitch {pitch} was not set correctly.");
            SoundFXManager.Instance.PlaySound("PlayerShoot");
            Assert.AreEqual(pitch, SoundFXManager.Instance.GetPitch("PlayerShoot"), $"Pitch {pitch} was not set correctly.");
            yield return new WaitForSeconds(0.5f); // Wait for a frame to ensure the pitch is set
        }
    }

    [UnityTest]
    public IEnumerator InvalidSoundKeyTest()
    {
        string invalidKey = "InvalidKey";

        // Attempt to play an invalid sound key
        LogAssert.Expect(LogType.Warning, $"Sound key '{invalidKey}' not found in AudioManager!");
        SoundFXManager.Instance.PlaySound(invalidKey);

        // Attempt to get volume for an invalid sound key
        LogAssert.Expect(LogType.Warning, $"Sound key '{invalidKey}' not found in AudioManager!");
        float volume = SoundFXManager.Instance.GetVolume(invalidKey);
        Assert.AreEqual(0f, volume, "Volume for invalid key should be 0.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator InvalidMusicSoundKeyTest()
    {
        string invalidKey = "InvalidKey";

        // Attempt to play an invalid sound key
        LogAssert.Expect(LogType.Warning, $"Sound key '{invalidKey}' not found in AudioManager!");
        MusicManager.Instance.PlaySound(invalidKey);

        // Attempt to get volume for an invalid sound key
        LogAssert.Expect(LogType.Warning, $"Sound key '{invalidKey}' not found in AudioManager!");
        float volume = MusicManager.Instance.GetVolume(invalidKey);
        Assert.AreEqual(0f, volume, "Volume for invalid key should be 0.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayAndStopSoundTest()
    {
        string soundKey = "PlayerShoot";

        // Play the sound
        SoundFXManager.Instance.PlaySound(soundKey);
        Assert.IsTrue(SoundFXManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' is not playing.");

        // Stop the sound
        SoundFXManager.Instance.StopSound(soundKey);
        Assert.IsFalse(SoundFXManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' did not stop.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayAndStopMusicTest()
    {
        string soundKey = "WeBringTheBoom";

        // Play the sound
        MusicManager.Instance.PlaySound(soundKey);
        Assert.IsTrue(MusicManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' is not playing.");

        // Stop the sound
        MusicManager.Instance.StopSound(soundKey);
        Assert.IsFalse(MusicManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' did not stop.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator MainMenuMusicStopsInGameTest()
    {
        // Ensure the main menu music is playing
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSeconds(1.0f); // Wait for the scene to load
        string mainMenuMusicKey = "WeBringTheBoom";
        Assert.IsTrue(MusicManager.Instance.isPlaying(mainMenuMusicKey), "Main menu music is not playing.");

        // Simulate transitioning to the game scene
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(1.0f); // Wait for the scene to load

        // Ensure the main menu music has stopped
        Assert.IsFalse(MusicManager.Instance.isPlaying("Webringtheboom"), "Main menu music is still playing in the game scene.");

        // Ensure the game music is playing
        string gameMusicKey = "GameMusic";
        Assert.IsTrue(MusicManager.Instance.isPlaying(gameMusicKey), "Game music is not playing.");
    }
    [UnityTest]
    public IEnumerator GameMusicStopsInMainMenuTest()
    {
        // Ensure the game music is playing
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(1.0f); // Wait for the scene to load
        string gameMusicKey = "GameMusic";
        Assert.IsTrue(MusicManager.Instance.isPlaying(gameMusicKey), "Game music is not playing.");

        // Simulate transitioning back to the main menu scene
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSeconds(1.0f); // Wait for the scene to load

        // Ensure the game music has stopped
        Assert.IsFalse(MusicManager.Instance.isPlaying(gameMusicKey), "Game music is still playing in the main menu scene.");

        // Ensure the main menu music is playing
        string mainMenuMusicKey = "WeBringTheBoom";
        Assert.IsTrue(MusicManager.Instance.isPlaying(mainMenuMusicKey), "Main menu music is not playing.");
    }

    [UnityTest]
    public IEnumerator RapidStartAndStopSoundTest()
    {
        string soundKey = "PlayerShoot";

        // Play and stop the sound multiple times
        for (int i = 0; i < 50; i++) // Repeat 50 times
        {
            SoundFXManager.Instance.PlaySound(soundKey);
            Assert.IsTrue(SoundFXManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' is not playing on iteration {i}.");

            SoundFXManager.Instance.StopSound(soundKey);
            Assert.IsFalse(SoundFXManager.Instance.isPlaying(soundKey), $"Sound '{soundKey}' did not stop on iteration {i}.");
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator VolumePersistenceAcrossScenesTest()
    {
        float initialVolume = 0.5f;
        SoundFXManager.Instance.SetVolume(initialVolume);

        // Transition to another scene
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(1.0f);

        // Assert that the volume is still the same
        Assert.AreEqual(initialVolume, SoundFXManager.Instance.GetVolume("PlayerShoot"), "Volume did not persist across scenes.");
    }
    
    [UnityTest]
    public IEnumerator AudioOverlapTest()
    {
        string soundKey1 = "PlayerShoot";
        string soundKey2 = "EnemyDeath";

        // Play two sounds simultaneously
        SoundFXManager.Instance.PlaySound(soundKey1);
        SoundFXManager.Instance.PlaySound(soundKey2);

        // Assert that both sounds are playing
        Assert.IsTrue(SoundFXManager.Instance.isPlaying(soundKey1), $"Sound '{soundKey1}' is not playing.");
        Assert.IsTrue(SoundFXManager.Instance.isPlaying(soundKey2), $"Sound '{soundKey2}' is not playing.");

        yield return null;
    }
}
