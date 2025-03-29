using UnityEngine;

public class PlayShootAudio : MonoBehaviour
{
    [SerializeField] private AudioSource playershoot1;

    public virtual void PlayerShoot()
    {
        playershoot1.Play();
        Debug.Log("Playing shoot sound");
    }
}
