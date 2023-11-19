using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum Mom
    {
        AWAY,
        OUTSIDE,
        INSIDE,
        WATCHING_CLOSELY,
    }

    public float score = 0.0f;
    public float momSus = 0.0f;
    public Mom momState = Mom.INSIDE;
    public float transitionLockout = 0.0f;
    public GameObject mom;
    public bool isGaming = false;
    public bool isWorking = false;


    public void SetGaming (bool gaming)
    {
        isGaming = gaming;
    }

    public void SetWorking (bool working)
    {
        isWorking = working;
    }

    private void transitionMom ()
    {
        transitionLockout = Mathf.Max(0.0f, transitionLockout - Time.deltaTime);

        if (0.0f >= transitionLockout)
        {
            float rng = Random.Range(0.0f, 100.0f);
            // Transition Mom
            switch (momState)
            {
                case Mom.AWAY:
                    if (90.0f < rng) // 10% chance to stay away
                        momState = Mom.AWAY;
                    else
                        momState = Mom.OUTSIDE;
                    break;
                case Mom.OUTSIDE:
                    if (90.0f < rng)
                        momState = Mom.AWAY;
                    else
                        momState = Mom.INSIDE;
                    break;
                case Mom.INSIDE:
                    if (90.0f < rng)
                        momState = Mom.WATCHING_CLOSELY;
                    else if (70.0f < rng)
                        momState = Mom.INSIDE;
                    else if (50.0f < rng)
                        momState = Mom.OUTSIDE;
                    else
                        momState = Mom.AWAY;
                    break;
                case Mom.WATCHING_CLOSELY:
                    if (50.0f < rng)
                        momState = Mom.WATCHING_CLOSELY;
                    else
                        momState = Mom.INSIDE;
                    break;
            }

            transitionLockout += 3.0f;
        }
    }

    private void calculateSus ()
    {
        if (Mom.INSIDE == momState)
        {
            if (isGaming)
                momSus += 10.0f * Time.deltaTime; // arbitrary number
            else if (isWorking)
                momSus -= 5.0f * Time.deltaTime;
        }
        else if (Mom.WATCHING_CLOSELY == momState)
        {
            if (isGaming)
                momSus += 20.0f * Time.deltaTime;
            else if (!isWorking)
                momSus += 10.0f * Time.deltaTime;
            else
                momSus -= 10.0f * Time.deltaTime;
        }
        
        momSus = Mathf.Max(momSus, 0.0f);
    }

    void Start()
    {
        score = 0.0f;
        momSus = 0.0f;
        momState = Mom.INSIDE;
    }

    // Update is called once per frame
    void Update()
    {
        transitionMom();
        calculateSus();

        if (100.0f <= momSus)
        {
            // Game Over
            Debug.Log("GAME OVER");
        }

        if (isGaming)
        {
            score += 5.0f * Time.deltaTime;
        }
    }
}
