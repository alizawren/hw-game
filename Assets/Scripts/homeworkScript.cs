using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeworkScript : MonoBehaviour
{
    // 0 == put away
    new public Camera camera;
    public GameController controller;
    private bool isDoingWork = true;
    // takeoutProgress represents where the gameboy is.
    // At 0, it is hidden under desk
    // At 0.5, it is above desk
    // At 1, it is being played.
    private float workProgress = 0;
    private float workProgressFinish = 1;
    private float workProgressSpeed = 2.5f;
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
        if (camera.transform.position.x > -1.3f && camera.transform.position.x < 1.5f && camera.transform.position.y > -0.8f) {
            isDoingWork = true;
        } else {
            isDoingWork = false;
        }

        if (isDoingWork) {
            float workDone = Time.deltaTime * workProgressSpeed;
            if (workProgress < 0.25f) {
                workProgress = 0.25f;
            } else if (workProgress < 0.5f) {
                workDone *= 2;
            }
            workProgress = Mathf.Min(workProgressFinish, workProgress + workDone);
        } else {
            workProgress = Mathf.Max(0, workProgress - Time.deltaTime * workProgressSpeed * 1.25f);
        }

        float newScale = startScale - workProgress * 1.25f;
        transform.localScale = new Vector3(newScale, newScale, 1);
        float newYPos = startY + workProgress * 1.9f;
        transform.position = new Vector3(startX, newYPos, -2);

        controller.SetWorking(isDoingWork);
    }
}
