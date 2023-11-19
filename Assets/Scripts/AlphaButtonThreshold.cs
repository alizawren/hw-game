using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaButtonThreshold : MonoBehaviour
{
    public Image button;
    public float alphaThreshold = 0.5f;

    void Start()
    {
        button.alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
