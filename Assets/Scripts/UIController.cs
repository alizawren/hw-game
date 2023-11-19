using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textScore;
    public GameController gameController;

    void Update ()
    {
        textScore.text = "Score: " + (int) gameController.score;
    }
}
