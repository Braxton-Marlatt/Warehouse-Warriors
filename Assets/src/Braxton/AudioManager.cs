using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Sound Effects")]
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _playerHit;
    [SerializeField] private AudioSource _meleeSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlayPlayerShoot()
    {
        _playerShoot.Play();
    }
    public void PlayMeleeSound()
    {
        _meleeSound.Play();
    }

}
