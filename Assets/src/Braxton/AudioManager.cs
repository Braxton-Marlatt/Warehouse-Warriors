using UnityEngine;

public class AudioManager : PlayShootAudio
{
    // Singleton instance
    private static AudioManager _instance;

    // Public property to access the instance
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning("AudioManager instance is null. Ensure AudioManager is in the scene.");
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

    // Ensure only one instance exists
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            
        }
    }

    // Play a sound effect
    // Override the Playershoot method from PlayShootAudio, Comment for griddy song
    public override void PlayerShoot()
    {
        if (playershoot != null)
        {
            playershoot.Play();
        }
        else
        {
            Debug.LogWarning("Player shoot AudioSource is null!");
        }
    }

    public void PlayerHit()
    {
        if (playerhit != null)
        {
            playerhit.Play();
        }
        else
        {
            Debug.LogWarning("Player hit AudioSource is null!");
        }
    }

    // Play the boom sound effect
    public void PlayBoom()
    {
        if (boom != null)
        {
            boom.Play();
        }
        else
        {
            Debug.LogWarning("Boom AudioSource is null!");
        }
    }
    public void PlayerMelee()
    {
        if (playermelee != null)
        {
            playermelee.Play();
        }
        else
        {
            Debug.LogWarning("Player melee AudioSource is null!");
        }
    }

    // Set volume for all audio sources
    public void SetVolume(float volume)
    {
        foreach (var audioSource in new[] { playershoot, playerhit, playermelee, boom })
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }

    // Set pitch for all audio sources
    public void SetPitch(float pitch)
    {
        foreach (var audioSource in new[] { playershoot, playerhit, playermelee, boom })
        {
            if (audioSource != null)
            {
                audioSource.pitch = pitch;
            }
        }
    }

    public float GetPitch()
    {
        if (playershoot != null)
        {
            return playershoot.pitch;
        }
        else
        {
            Debug.LogWarning("Player shoot AudioSource is null!");
            return 0f;
        }
    }

    // Get the volume of the playershoot AudioSource, used for testing
    public float GetVolume()
    {
        if (playershoot != null)
        {
            return playershoot.volume;
        }
        else
        {
            Debug.LogWarning("Player shoot AudioSource is null!");
            return 0f;
        }
    }

    public AudioSource GetPlayerHitAudioSource()
    {
        return playerhit;
    }

    public AudioSource GetPlayerMeleeAudioSource()
    {
        return playermelee;
    }

    public AudioSource GetPlayerShootAudioSource()
    {
        return playershoot;
    }
}