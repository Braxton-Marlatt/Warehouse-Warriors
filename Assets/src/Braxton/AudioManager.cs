using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Sound Effects")]
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _playerHit;
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
    }

    public void PlayPlayerShoot()
    {
        _playerShoot.Play();
    }
    public void PlayPlayerHit()
    {
        if(_player)
        _playerHit.Play();
    }
}
