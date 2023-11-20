using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textScore;
    public Text debugMom;
    public Text debugNext;
    public Text debugLockout;
    public Text debugCueTime;
    public Text debugCue;
    public Text debugSus;
    public GameController gameController;

    void Update ()
    {
        textScore.text = "Score: " + (int) gameController.score;

        // Debug text
        debugMom.text = "Mom: " + gameController.momState.ToString();
        debugSus.text = string.Format("SUS: {0:f}", gameController.momSus);
        debugLockout.text = string.Format("Lockout: {0:f}s", gameController.transitionLockout);
        debugNext.text = "Next: " + gameController.getNextState();
        debugCueTime.text = "Cue at: " + gameController.getCueTime();
        debugCue.text = "Cue: " + gameController.getCue();
    }
}
