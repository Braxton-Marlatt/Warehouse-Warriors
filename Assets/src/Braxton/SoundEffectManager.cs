
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : AudioManager
{
    // Singleton instance
    private static SoundEffectManager _instance;

    public static SoundEffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundEffectManager instance is null. Ensure SoundEffectManager is in the scene.");
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    [Header("Sound Effects")]
    [SerializeField] private AudioSource playershoot;
    [SerializeField] private AudioSource playerhit;
    [SerializeField] private AudioSource playermelee;
    [SerializeField] private AudioSource boom;
    [SerializeField] private AudioSource buttonClick;

    // Dictionary to store audio sources
    public Dictionary<string, AudioSource> audioSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            playerhit = gameObject.AddComponent<AudioSource>();
            playermelee = gameObject.AddComponent<AudioSource>();
            playershoot = gameObject.AddComponent<AudioSource>();
            boom = gameObject.AddComponent<AudioSource>();
            buttonClick = gameObject.AddComponent<AudioSource>();

            playerhit.clip = Resources.Load<AudioClip>("PlayerHit");
            playermelee.clip = Resources.Load<AudioClip>("PlayerMelee");
            playershoot.clip = Resources.Load<AudioClip>("PlayerShoot");
            boom.clip = Resources.Load<AudioClip>("Boom");
            buttonClick.clip = Resources.Load<AudioClip>("ButtonClick");
            // Initialize the dictionary
            audioSources = new Dictionary<string, AudioSource>
            {
                { "playershoot", playershoot },
                { "playerhit", playerhit },
                { "playermelee", playermelee },
                { "boom", boom },
                { "buttonClick", buttonClick }
            };
        }
    }

}