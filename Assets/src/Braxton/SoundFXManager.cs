using UnityEngine;
using System.Collections.Generic;

public class SoundFXManager : AudioManager
{
    // Singleton instance
    private static SoundFXManager _instance;

    // Public property to access the instance
    public static SoundFXManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundFXManager instance is null. Ensure SoundFXManager is in the scene.");
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    // Dictionary to hold audio sources
    public Dictionary<string, AudioSource> soundFXSources = new Dictionary<string, AudioSource>();

    // Ensure only one instance exists
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSoundFXSources();
    }

    // Initialize sound effects
    private void InitializeSoundFXSources()
    {
        Debug.Log("Initializing soundFXSources...");

        // Add sound effects to the audioSources dictionary
        AddAudioSource("PlayerHit", "Playerhit");
        AddAudioSource("PlayerShoot", "Playershoot");
        AddAudioSource("EnemyDeath", "Enemydeath");
        AddAudioSource("PlayerMelee", "Playermelee");

        Debug.Log("soundFXSources initialized successfully.");
    }

    // Play a specific sound effect
    public void PlaySoundEffect(string soundKey)
    {
        PlaySound(soundKey);
    }

    // Stop a specific sound effect
    public void StopSoundEffect(string soundKey)
    {
        StopSound(soundKey);
    }

    
}


