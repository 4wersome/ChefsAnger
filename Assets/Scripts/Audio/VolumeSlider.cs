using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType
    {
        Master,
        Music,
        SFX
    }

    [Header("Type")]
    [SerializeField]
    private VolumeType volumeType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                slider.value = Audiomngr.Instance.mastervolume;
                break;
            case VolumeType.Music:
                slider.value = Audiomngr.Instance.MusicVolume;

                break;
            case VolumeType.SFX:
                slider.value = Audiomngr.Instance.SFXVolume;

                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                Audiomngr.Instance.mastervolume = slider.value;
                break;
            case VolumeType.Music:
                Audiomngr.Instance.MusicVolume = slider.value;

                break;
            case VolumeType.SFX:
                Audiomngr.Instance.SFXVolume = slider.value;

                break;
        }
    }
}