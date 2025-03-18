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
            DontDestroyOnLoad(gameObject);
        }
    }

    // Play a sound effect
    public void Playershoot()
    {
        playershoot.Play();
    }

    public void Playerhit()
    {
        playerhit.Play();
    }

    public void PlayMelee()
    {
        playermelee.Play();
    }

}