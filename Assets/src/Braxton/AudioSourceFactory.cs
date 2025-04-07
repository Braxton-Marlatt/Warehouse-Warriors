using UnityEngine;

public interface IAudioSourceFactory
{
    AudioSource CreateAudioSource(string resourceName);
}

public class AudioSourceFactory : IAudioSourceFactory
{
    public AudioSource CreateAudioSource(string resourceName)
    {
        GameObject gameObject = new GameObject(resourceName);
        AudioClip clip = Resources.Load<AudioClip>(resourceName);
        if (clip == null)
        {
            Debug.LogError($"Audio clip '{resourceName}' could not be loaded! Ensure it is in the Resources folder.");
            return null;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false; // Set to false to prevent auto-playing
        return audioSource;
    }
}