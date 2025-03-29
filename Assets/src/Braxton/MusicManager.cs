using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
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
        private set
        {
            _instance = value;
        }
    }

    [Header("Sound Effects")]
    [SerializeField] private AudioSource WeBringTheBoom;

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic();
    }
    
    // Play a sound effect
    public void PlayWeBringTheBoom()
    {
        if (WeBringTheBoom != null)
        {
            WeBringTheBoom.Play();
        }
        else
        {
            Debug.LogError("WeBringTheBoom AudioSource is null!");
        }
    }

    public void PlayMusic()
    {
        if (SceneManager.GetActiveScene().name == "Start_Menu")
        {
            PlayWeBringTheBoom();
        }
        else
        {
            WeBringTheBoom.Stop();
        }
    }

    public void SetMusicVolume(float volume)
    {
        WeBringTheBoom.volume = volume;
    }

}
