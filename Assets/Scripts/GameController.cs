using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float transitionLockout = 3.0f;
    public GameObject mom;
    public bool isGaming = false;
    public bool isWorking = false;
    public bool isPaused = false;
    public PauseMenuController pauseMenu;

    public void SetGaming (bool gaming)
    {
        isGaming = gaming;
    }

    public void SetWorking (bool working)
    {
        isWorking = working;
    }

    public void PauseGame ()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        pauseMenu.OpenMenu();
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenu.CloseMenu();
    }

    public void QuitToMenu ()
    {
        ResumeGame();
        SceneManager.LoadScene("MenuScene");
    }

    private void transitionMom ()
    {
        transitionLockout = Mathf.Max(0.0f, transitionLockout - Time.deltaTime);

        if (0 != Time.timeScale && transitionLockout <= 0.0f)
        {
            float rng = Random.Range(0.0f, 100.0f);
            // Transition Mom
            switch (momState)
            {
                case Mom.AWAY:
                    // 50% chance for mom to stay away
                    // reduces down to 0% based on suspicion
                    if (50.0f + 100.0f * momSus <= rng)
                        momState = Mom.AWAY;
                    else
                        momState = Mom.OUTSIDE;
                    break;
                case Mom.OUTSIDE:
                    // 50% chance for mom to just walk by, decreasing to 0% at 50% sus
                    if (50.0f + 100.0f * momSus <= rng)
                        momState = Mom.AWAY;
                    else
                        momState = Mom.INSIDE;
                    break;
                case Mom.INSIDE:
                    // 10% chance for mom to start watching closely, increasing up to 30%
                    // 20% chance for mom to stay inside, increasing up to 40%
                    // 50% chance for mom to step out, decreasing to 30%
                    // 20% chance for mom to leave, decreasing to 0%
                    // 100% chance for mom to watch closely if you are gaming
                    if (isGaming || 90.0f - 20.0f * momSus <= rng)
                        momState = Mom.WATCHING_CLOSELY;
                    else if (70.0f - 40.0f * momSus <= rng)
                        momState = Mom.INSIDE;
                    else if (20.0f - 20.0f * momSus <= rng)
                        momState = Mom.OUTSIDE;
                    else
                        momState = Mom.AWAY;
                    break;
                case Mom.WATCHING_CLOSELY:
                    // mom will keep watching until sus < 50
                    // otherwise 50% chance to stop or continue
                    if (0.5f < momSus || 50.0f <= rng)
                        momState = Mom.WATCHING_CLOSELY;
                    else
                        momState = Mom.INSIDE;
                    break;
            }

            transitionLockout += Random.Range(2.5f, 4.0f);
        }
    }

    private void calculateSus ()
    {
        if (Mom.INSIDE == momState)
        {
            if (isGaming)
                momSus += 0.10f * Time.deltaTime; // arbitrary number
            else if (isWorking)
                momSus -= 0.05f * Time.deltaTime;
        }
        else if (Mom.WATCHING_CLOSELY == momState)
        {
            if (isGaming)
                momSus += 0.40f * Time.deltaTime;
            else if (!isWorking)
                momSus += 0.10f * Time.deltaTime;
            else
                momSus -= 0.10f * Time.deltaTime;
        }
        else
        {
            // mom is worried about whether you are actually working
            // hard cap so you cannot just passively lose
            if (momSus < 0.90f)
                momSus += 0.01f * Time.deltaTime;
        }
        
        momSus = Mathf.Max(momSus, 0.0f);
    }

    void Start()
    {
        score = 0.0f;
        momSus = 0.0f;
        momState = Mom.INSIDE;
        Time.timeScale = 1.0f;
        transitionLockout = 3.0f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        transitionMom();
        calculateSus();

        if (1.00f <= momSus)
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
