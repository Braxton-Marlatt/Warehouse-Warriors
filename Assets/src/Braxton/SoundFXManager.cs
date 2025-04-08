using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.Audio;

public class SoundFXManager : AudioManager
{
    // Singleton instance
    private static SoundFXManager _instance;
    private IAudioSourceFactory soundFXSourceFactory = new AudioSourceFactory();
    [SerializeField] public AudioMixerGroup audioMixerGroup; // Reference to the AudioMixerGroup
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

    
    public void AddAudioSource(string key, string resourceName, AudioMixerGroup audioMixer = null)
    {
        AudioSource audioSource = soundFXSourceFactory.CreateAudioSource(resourceName, audioMixer);
        audioSources[key] = audioSource;

        Debug.Log($"Audio source '{key}' added successfully.");
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
        AddAudioSource("Buttonclick", "Buttonclick", audioMixerGroup);
        AddAudioSource("ShoppingCart", "Shoppingcart", audioMixerGroup);
        AddAudioSource("EnemyHit", "Enemyhit");
        AddAudioSource("EnemyShoot", "Enemyshoot");
        AddAudioSource("EnemyReload", "Enemyreload", audioMixerGroup);
        AddAudioSource("PlayerDeath", "Playerdeath");
        AddAudioSource("Playerdash", "Playerdash");
        AddAudioSource("Playermove", "Playermove");

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

    public void PlayShoppingCartIfMeleeEnemies(List<Enemy> enemies)
    {
        bool hasMeleeEnemies = false;
        // Check if the list contains any melee enemies
        foreach(Enemy e in enemies)
        {
            if(e.enemyType == 0)
            {
                hasMeleeEnemies = true;
                break; // Exit the loop if a melee enemy is found
            }
        }
        if(hasMeleeEnemies)
        {
            SoundFXManager.Instance.audioSources["ShoppingCart"].loop = true;
            PlaySound("ShoppingCart"); // Play the sound if melee enemies are present
        }
    }

    public void StopShoppingCart(List<Enemy> enemies)
    {
        foreach (Enemy e in enemies)
        {
            if (e.enemyType == 0) // Check if there are no melee enemies left
            {
                return; // Exit the method if there are still melee enemies
            }
        }
        StopSoundEffect("ShoppingCart"); // Stop the sound if no melee enemies are present
    }

    
    public System.Collections.IEnumerator PlaySoundWithDelay(string soundKey, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(soundKey);
    }

    public void PlaysoundwithLoop(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            audioSource.loop = true; // Set the loop property
            PlaySound(soundKey); // Play the sound
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }

}


