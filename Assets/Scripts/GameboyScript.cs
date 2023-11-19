using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboyScript : MonoBehaviour
{

    // 0 == put away
    new public Camera camera;
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
    void Start () {
        startX = transform.position.x;
        startY = transform.position.y;
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (camera.transform.position.x > -0.3f && camera.transform.position.x < 0.3f && camera.transform.position.y < -1.6f) {
            attemptingToPlay = true;
        } else {
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
            transform.position = new Vector3(startX, startY - takeoutProgressMidway * 2f + localProgress * 1.5f, -2);
            float newScale = startScale + localProgress * 1.75f;
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
         
    }
}