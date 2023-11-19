using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameboyScript : MonoBehaviour
{

    // 0 == put away
    public Animator animator;
    new public Camera camera;
    public GameController controller;
    private bool attemptingToPlay = true;
    // takeoutProgress represents where the gameboy is.
    // At 0, it is hidden under desk
    // At 0.5, it is above desk
    // At 1, it is being played.
    private float takeoutProgress = 0;
    private float takeoutProgressMidway = 0.49f;
    private float takeoutProgressFinish = 1;
    private float takeoutProgressSpeed = 0.9f;
    private float startX;
    private float startY;
    private float startScale;
    private float score = 0;
    private float scoreMultiplier = 1;
    private bool isGaming = false;
    void Start () {
        startX = transform.position.x;
        startY = transform.position.y;
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.transform.position.x > -0.9f && camera.transform.position.x < 0.9f && camera.transform.position.y < -1.85f) {
            attemptingToPlay = true;
        } else {
            scoreMultiplier = 1;
            attemptingToPlay = false;
        }


        if (attemptingToPlay) {
            takeoutProgress = Mathf.Min(takeoutProgressFinish, takeoutProgress + Time.deltaTime * takeoutProgressSpeed);
        } else {
            takeoutProgress = Mathf.Max(0, takeoutProgress - Time.deltaTime * takeoutProgressSpeed * 2f);
        }
        if (takeoutProgress < takeoutProgressMidway) {
            // under desk
            transform.position = new Vector3(startX, startY - takeoutProgress * 2f, 0);
            transform.localScale = new Vector3(startScale, startScale, startScale);
        } else if (takeoutProgress > takeoutProgressMidway) {
            // above desk
            float localProgress = takeoutProgress - takeoutProgressMidway;
            transform.position = new Vector3(startX, startY - takeoutProgressMidway * 2f + localProgress * 2.5f, -2);
            float newScale = startScale + localProgress * 1.25f;
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }

        if (!isGaming && takeoutProgress >= takeoutProgressFinish - 0.04f) {
            isGaming = true;
            animator.Play("loop");
        } else if (isGaming && takeoutProgress < takeoutProgressFinish - 0.04f) {
            isGaming = false;
            animator.Play("off");
        }
        controller.SetGaming(isGaming);
         
    }
}