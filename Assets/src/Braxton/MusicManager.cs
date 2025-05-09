using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MusicManager : AudioManager
{
    // Singleton instance
    private static MusicManager _instance;
    private IAudioSourceFactory musicSourceFactory = new AudioSourceFactory();

    // This allows other classes to access the instance of MusicManager
    // while ensuring that only one instance exists (singleton pattern).
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
    [SerializeField] private AudioMixerGroup audioMixerGroup;

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
    // Initialize the music sources dictionary
    private void InitializeMusicSources()
    {
        AddMusicSource("WeBringTheBoom", "Webringtheboom");
        AddMusicSource("GameMusic", "Gamemusic");

        Debug.Log("Music sources initialized successfully.");
    }
    
    private void AddMusicSource(string key, string resourceName)
    {
        AudioSource audioSource = musicSourceFactory.CreateAudioSource(resourceName, audioMixerGroup);
        audioSources[key] = audioSource;

        Debug.Log($"Audio source '{key}' added successfully.");
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        // Stop all currently playing music
        StopAllMusic();

        // Play music based on the scene name
        if (sceneName == "Start_Menu")
        {
            Debug.Log("Playing Start_Menu music.");
            PlaySound("WeBringTheBoom");
        }
        else if (sceneName == "Game")
        {
            Debug.Log("Playing Game music.");
            PlaySound("GameMusic");
        }
        else
        {
            Debug.LogWarning($"No music configured for scene '{sceneName}'.");
        }
    }

    private void StopAllMusic()
    {
        foreach (var audioSource in audioSources.Values)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                Debug.Log($"Stopping music: {audioSource.clip.name}");
                audioSource.Stop();
            }
        }
    }
}