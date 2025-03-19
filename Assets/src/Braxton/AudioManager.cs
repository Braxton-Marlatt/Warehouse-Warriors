using UnityEngine;

public class AudioManager : MonoBehaviour
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
                Debug.LogError("AudioManager instance is null. Ensure AudioManager is in the scene.");
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
    public void Playershoot()
    {
        if (playershoot != null)
        {
            playershoot.Play();
        }
        else
        {
            Debug.LogError("Player shoot AudioSource is null!");
        }
    }
    
    public void Playerhit()
    {
        if (playershoot != null)
        {
            playerhit.Play();
        }
        else
        {
            Debug.LogError("Player shoot AudioSource is null!");
        }
    }
    public void Playermelee()
    {
        if (playershoot != null)
        {
            playermelee.Play();
        }
        else
        {
            Debug.LogError("Player shoot AudioSource is null!");
        }
    }


    // Set volume for all audio sources
    public void SetVolume(float volume)
    {
        if (playershoot != null)
        {
            playershoot.volume = volume;
        }
        if (playerhit != null)
        {
            playerhit.volume = volume;
        }
        if (playermelee != null)
        {
            playermelee.volume = volume;
        }
    }
    //Set pitch for all audio sources
    public void SetPitch(float pitch)
    {
        if (playershoot != null)
        {
            playershoot.pitch = pitch;
        }
        if (playerhit != null)
        {
            playerhit.pitch = pitch;
        }
        if (playermelee != null)
        {
            playermelee.pitch = pitch;
        }
    }

    public float GetPitch(){
        if (playershoot != null)
        {
            return playershoot.pitch;
        }
        else
        {
            Debug.LogError("Player shoot AudioSource is null!");
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
            Debug.LogError("Player shoot AudioSource is null!");
            return 0f;
        }
    }
    public AudioSource GetPlayerHitAudioSource(){
        return playerhit;
    }

    public AudioSource GetPlayerMeleeAudioSource(){
        return playermelee;
    }

    public AudioSource GetPlayerShootAudioSource(){
        return playershoot;
    }

}