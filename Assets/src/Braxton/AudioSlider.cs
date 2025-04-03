using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Ensure the slider is not null
        if (volumeSlider != null)
        {
            // Set the initial value of the slider to the current volume
            volumeSlider.value = SoundEffectManager.Instance.GetVolume("playershoot", SoundEffectManager.Instance.audioSources);

            // Add a listener to call the OnVolumeChange method whenever the slider value changes
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        }
        else
        {
            Debug.LogError("Volume slider is not assigned.");
        }
    }

    // Method to be called when the slider value changes
    public void OnVolumeChange(float value)
    {
        // Set the volume in the AudioManager
        MusicManager.Instance.SetVolume(MusicManager.Instance.musicSources, value);
        SoundEffectManager.Instance.SetVolume(SoundEffectManager.Instance.audioSources, value);

    }

}