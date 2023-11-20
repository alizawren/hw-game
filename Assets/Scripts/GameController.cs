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
        IN_CLOSET,
        WATCHING_CLOSELY, // at desk
        YELLING,
        DAD,
        UNDER_DESK,
        UNDECIDED
    }

    private enum Cue
    {
        NONE,
        GOING,
        COMING
    }

    public float score = 0.0f;
    public float momSus = 0.0f;
    public Mom momState = Mom.INSIDE;
    private Mom nextState = Mom.OUTSIDE;
    public float transitionLockout = 3.0f;
    private float soundCueTime = 0.0f;
    private Cue soundCue = Cue.NONE;
    public AudioSource audio;
    public AudioClip clipGoing;
    public AudioClip clipComing;

    public GameObject mom;
    public GameObject dad;
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

    private void playCue ()
    {
        if (Cue.NONE == soundCue)
            return;

        if (transitionLockout < soundCueTime)
        {
            // play cue sound
            switch (soundCue)
            {
                case Cue.COMING:
                    audio.PlayOneShot(clipComing, 1.0f);
                    break;
                case Cue.GOING:
                    audio.PlayOneShot(clipGoing, 1.0f);
                    break;
                default:
                    break;
            }
            soundCue = Cue.NONE;
        }
    }

    // debugging functions
    public string getCue ()
    {
        return soundCue.ToString();
    }

    public string getCueTime ()
    {
        return soundCueTime.ToString();
    }

    public string getNextState ()
    {
        return nextState.ToString();
    }

    //helper function
    private void setNextTransition (Mom state, float lockout, float cueTime = 0.0f, Cue cue = Cue.NONE)
    {
        // the next state
        nextState = state;
        // how long to stay at the current state
        transitionLockout = lockout + Random.Range(-0.5f, 0.5f);
        // when to play the sound cue
        soundCueTime = cueTime;
        // which sound cue to play
        soundCue = cue;
    }

    private void transitionMom ()
    {
        transitionLockout = Mathf.Max(0.0f, transitionLockout - Time.deltaTime);

        if (0 != Time.timeScale && transitionLockout <= 0.0f)
        {
            // Transition mom
            momState = nextState;
            nextState = Mom.UNDECIDED;

            float rng = Random.Range(0.0f, 100.0f);
            // Pick next transition:
            // pick the next state
            // pick how long that state lasts
            // pick when to play transition sounds
            // pick what transition sound
            switch (momState)
            {
                case Mom.AWAY:
                    // 50% chance for mom to stay away
                    // reduces down to 0% based on suspicion
                    if (50.0f + 100.0f * momSus <= rng)
                        setNextTransition(Mom.AWAY, 1.0f);
                    else
                        setNextTransition(Mom.OUTSIDE, 6.0f, 5.0f, Cue.COMING);
                    break;
                case Mom.OUTSIDE:
                    // 50% chance for mom to just walk by, decreasing to 0% at 50% sus
                    // 15% chance for dad
                    // 35% chance to go inside
                    if (50.0f + 100.0f * momSus <= rng)
                        setNextTransition(Mom.AWAY, 6.0f, 6.0f, Cue.GOING);
                    else if (35.0f <= rng)
                        setNextTransition(Mom.DAD, 3.0f);
                    else
                        setNextTransition(Mom.INSIDE, 2.0f);
                    break;
                case Mom.INSIDE:
                    // 10% chance for mom to start watching closely, increasing up to 30%
                    // 5% chance for mom to stay inside, increasing up to 40%
                    // 10% chance for mom to enter closet
                    // 5% chance for mom to yell
                    // 70% chance for mom to step out, decreasing to 30%
                    // 100% chance for mom to watch closely if you are gaming
                    if (isGaming || 90.0f - 20.0f * momSus <= rng)
                        setNextTransition(Mom.WATCHING_CLOSELY, 1.0f);
                    else if (85.0f - 40.0f * momSus <= rng)
                        setNextTransition(Mom.INSIDE, 2.0f);
                    else if (75f <= rng)
                        setNextTransition(Mom.IN_CLOSET, 4.0f); 
                    else if (70f <= rng)
                        setNextTransition(Mom.YELLING, 2.0f);
                    else
                        setNextTransition(Mom.OUTSIDE, 1.0f);
                    break;
                case Mom.IN_CLOSET:
                    // half chance to go to watching closely (up to 80% depending on suspicion)
                    // half chance to go back to inside (down to 20%)
                    if (50f - 30f * momSus <= rng)
                        setNextTransition(Mom.WATCHING_CLOSELY, 2.0f);
                    else 
                        setNextTransition(Mom.INSIDE, 2.0f);
                    break;
                case Mom.WATCHING_CLOSELY:
                    // mom will keep watching until sus < 50
                    // otherwise 50% chance to stop or continue
                    if (0.5f < momSus || 50.0f <= rng)
                        setNextTransition(Mom.WATCHING_CLOSELY, 1.0f);
                    else
                        setNextTransition(Mom.INSIDE, 1.0f);
                    break;
                case Mom.YELLING:
                    // mom will keep yelling until sus < 50
                    // otherwise 50% chance to stop or continue
                    if (0.5f < momSus || 50.0f <= rng)
                        setNextTransition(Mom.YELLING, 1.0f);
                    else
                        setNextTransition(Mom.INSIDE, 1.0f);
                    break;
                case Mom.DAD:
                    dad.GetComponent<DadScript>().Appear();
                    setNextTransition(Mom.OUTSIDE, 5.0f);
                    break;
                case Mom.UNDER_DESK:
                    break;
            }
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
                momSus += 0.30f * Time.deltaTime;
            else if (!isWorking)
                momSus += 0.10f * Time.deltaTime;
            else
                momSus -= 0.10f * Time.deltaTime;
        }
        else if (Mom.YELLING == momState)
        {
            if (isGaming)
                momSus += 0.4f * Time.deltaTime;
            else if (!isWorking)
                momSus += 0.2f * Time.deltaTime;
            else
                momSus -= 0.1f * Time.deltaTime;
        }
        else if (Mom.IN_CLOSET == momState)
        {
            if (isGaming)
                momSus += 0.1f * Time.deltaTime;
            else if (isWorking)
                momSus -= 0.05f * Time.deltaTime;
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
        soundCue = Cue.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        playCue();
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
