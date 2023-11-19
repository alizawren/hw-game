using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public float score = 0.0f;
    private bool isGaming = false;
    private bool momIsHere = false;
    public GameObject mom;

    public void SetGaming (bool gaming)
    {
        isGaming = gaming;
    }

    public void SetMomPresence (bool present)
    {
        momIsHere = present;
    }

    void Start()
    {
        score = 0.0f;
        isGaming = false;
        momIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGaming)
        {
            if (momIsHere) 
            {
                // Game Over
            }
            else
            {
                score += 5.0f * Time.deltaTime;
            }
        }
    }
}
