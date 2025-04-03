using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public virtual void PlaySound(string soundKey, Dictionary<string, AudioSource> audioSources)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                audioSource.loop = true; // Ensure the music loops
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogError($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }


    // Get the volume of a specific sound effect
    public float GetVolume(string soundKey, Dictionary<string, AudioSource> audioSources)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                return audioSource.volume;
            }
        }
        Debug.LogError($"Sound key '{soundKey}' not found or AudioSource is null!");
        return 0f;
    }

    public void SetVolume(Dictionary<string, AudioSource> audioSources, float volume)
    {
        foreach (var kvp in audioSources)
        {
            if (audioSources.ContainsKey(kvp.Key))
            {
                audioSources[kvp.Key].volume = volume;
            }
            else
            {
                Debug.LogError($"Sound key '{kvp.Key}' not found in AudioManager!");
            }
        }
    }

    public void SetPitch(Dictionary<string, AudioSource> audioSources, float pitch)
    {
        foreach (var kvp in audioSources)
        {
            if (audioSources.ContainsKey(kvp.Key))
            {
                audioSources[kvp.Key].pitch = pitch;
            }
            else
            {
                Debug.LogError($"Sound key '{kvp.Key}' not found in AudioManager!");
            }
            
        }
    }

    public float GetPitch(string soundKey, Dictionary<string, AudioSource> audioSources)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                return audioSource.pitch;
            }
        }
        Debug.LogError($"Sound key '{soundKey}' not found or AudioSource is null!");
        return 0f;
    }
}