using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MusicManager : AudioManager
{
    // Singleton instance
    private static MusicManager _instance;

    // Public property to access the instance
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
    }

    [Header("Sound Effects")]
    [SerializeField] private AudioSource WeBringTheBoom;
    [SerializeField] private AudioSource GameMusic;

    public Dictionary<string, AudioSource> musicSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize the musicSources dictionary only if it hasn't been initialized yet
        if (musicSources == null)
        {
            // Create and configure AudioSources dynamically
            WeBringTheBoom = gameObject.AddComponent<AudioSource>();
            GameMusic = gameObject.AddComponent<AudioSource>();

            // Load audio clips from resources
            WeBringTheBoom.clip = Resources.Load<AudioClip>("WeBringTheBoom");
            GameMusic.clip = Resources.Load<AudioClip>("GameMusic");
            WeBringTheBoom.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("AudioMixers/MusicMixerGroup");
            GameMusic.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("AudioMixers/MusicMixerGroup");
            musicSources = new Dictionary<string, AudioSource>
            {
                { "WeBringTheBoom", WeBringTheBoom },
                { "GameMusic", GameMusic }
            };
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        StopMusic();

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Start_Menu")
        {
            PlaySound("WeBringTheBoom", musicSources);
        }
        if (sceneName == "Game" || sceneName == "helpMenu")
        {
            PlaySound("GameMusic", musicSources);
        }
    }

    public override void PlaySound(string soundKey, Dictionary<string, AudioSource> audioSources)
    {
        if (audioSources.TryGetValue(soundKey, out var audioSource))
        {
            if (audioSource != null)
            {
                audioSource.loop = true; // Ensure the music loops
                audioSource.Play();
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

    public void StopMusic()
    {
        foreach (var audioSource in musicSources.Values)
        {
            audioSource?.Stop();
        }
    }
}



