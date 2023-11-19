using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sliderMaster;
    public Slider sliderBGM;
    public Slider sliderSFX;

    // Start is called before the first frame update
    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("VolumeMaster");
        sliderBGM.value = PlayerPrefs.GetFloat("VolumeBGM");
        sliderSFX.value = PlayerPrefs.GetFloat("VolumeSFX");
    }

    public void SavePreferences ()
    {
        PlayerPrefs.SetFloat("VolumeMaster", sliderMaster.value);
        PlayerPrefs.SetFloat("VolumeBGM", sliderBGM.value);
        PlayerPrefs.SetFloat("VolumeSFX", sliderSFX.value);
    }
}
