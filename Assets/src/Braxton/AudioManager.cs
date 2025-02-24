using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Sound Effects")]
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _playerHit;
    [SerializeField] private AudioSource _meleeSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayPlayerShoot();
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlayMeleeSound();
        }
    }

    public void PlayPlayerShoot()
    {
        _playerShoot.Play();
    }
    public void PlayMeleeSound()
    {
        _meleeSound.Play();
    }

}
