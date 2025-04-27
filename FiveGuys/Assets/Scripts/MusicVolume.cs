using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;

    [Header("Audio")]
    public AudioSource musicSource;

    private void Start()
    {
        
        if (musicSource != null && volumeSlider != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    private void ChangeVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }

    private void OnDestroy()
    {
        
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
    }
}