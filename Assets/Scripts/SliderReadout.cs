using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderReadout : MonoBehaviour
{
    public Text text;

    public void SetTextFromSlider (float value)
    {
        text.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
