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
        AddAudioSource("ButtonClick", "Buttonclick");
        AddAudioSource("ShoppingCart", "Shoppingcart");
        AddAudioSource("EnemyHit", "Enemyhit");
        AddAudioSource("EnemyShoot", "Enemyshoot");
        AddAudioSource("EnmmyReload", "Enemyreload");

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
            PlaySound("ShoppingCart"); // Play the sound if melee enemies are present
        }
    }

    public System.Collections.IEnumerator PlaySoundWithDelay(string soundKey, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(soundKey);
    }
}


