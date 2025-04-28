using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SFXVolumeController : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider sfxSlider;

    [Header("Audio")]
    public List<AudioSource> sfxSources = new List<AudioSource>();

    private void Start()
    {
        
        if (sfxSources.Count > 0 && sfxSlider != null)
        {
            sfxSlider.value = sfxSources[0].volume;
            sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        }
    }

    private void ChangeSFXVolume(float value)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (source != null)
                source.volume = value;
        }
    }

    private void OnDestroy()
    {
      
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(ChangeSFXVolume);
    }
}