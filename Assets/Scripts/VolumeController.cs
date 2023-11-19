using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    
    public AudioMixer mixer;

    float convertValue (float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }

    public void SetLevelMaster (float sliderValue)
    {
        mixer.SetFloat("VolMaster", convertValue(sliderValue));
    }

    public void SetLevelSFXMaster (float sliderValue)
    {
        mixer.SetFloat("VolSFXMaster", convertValue(sliderValue));
    }

    public void SetLevelSFX (float sliderValue)
    {
        mixer.SetFloat("VolSFX", convertValue(sliderValue));
    }

    public void SetLevelBGM (float sliderValue)
    {
        mixer.SetFloat("VolBGM", convertValue(sliderValue));
    }

}
