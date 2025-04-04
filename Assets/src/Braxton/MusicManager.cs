using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicManager : AudioManager
{
    // Singleton instance
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MusicManager instance is null. Ensure MusicManager is in the scene.");
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    // Dictionary to hold audio sources
    private Dictionary<string, AudioSource> musicSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeMusicSources();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Initialize the music sources dictionary
    private void InitializeMusicSources()
    {
        musicSources = new Dictionary<string, AudioSource>();

        AddMusicSource("WeBringTheBoom", "Webringtheboom");
        AddMusicSource("GameMusic", "Gamemusic");

        Debug.Log("Music sources initialized successfully.");
    }

    // Helper method to add a music source
    private void AddMusicSource(string key, string resourceName)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(resourceName);

        if (audioSource.clip == null)
        {
            Debug.LogError($"Audio clip '{resourceName}' could not be loaded! Ensure it is in the Resources folder.");
            return;
        }

        musicSources[key] = audioSource;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    // Play music based on the scene name
    private void PlayMusicForScene(string sceneName)
    {
        StopAllMusic();

        if (sceneName == "Start_Menu")
        {
            PlaySound("WeBringTheBoom");
        }
        else if (sceneName == "Game")
        {
            PlaySound("GameMusic");
        }
        else
        {
            Debug.LogWarning($"No music configured for scene '{sceneName}'.");
        }
    }

    // Stop all currently playing music
    private void StopAllMusic()
    {
        foreach (var audioSource in musicSources.Values)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public override void PlaySound(string soundKey)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                if (audioSource.clip == null)
                {
                    Debug.LogError($"Audio clip for key '{soundKey}' is not assigned!");
                    return;
                }

                Debug.Log($"Playing sound for key '{soundKey}' with volume {audioSource.volume}");
                audioSource.loop = true; // Ensure music loops
                audioSource.Play();
            }
            else
            {
                Debug.LogError($"AudioSource for key '{soundKey}' is null!");
            }
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in AudioManager!");
        }
    }
}

