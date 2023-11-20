using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameboyScript : MonoBehaviour
{
    public AudioSource music;
    public AudioSource musicTrue;

    // 0 == put away
    public Animator animator;
    new public Camera camera;
    public GameController controller;
    public SpriteRenderer blackScreen;
    private bool attemptingToPlay = true;
    // takeoutProgress represents where the gameboy is.
    // At 0, it is hidden under desk
    // At 0.5, it is above desk
    // At 1, it is being played.
    private float takeoutProgress = 0;
    private float takeoutProgressMidway = 0.49f;
    private float takeoutProgressFinish = 1;
    private float takeoutProgressSpeed = 0.9f;
    private float focus = -0.5f;
    private float masterMusicVolume = 0;
    private float startX;
    private float startY;
    private float startScale;
    private float score = 0;
    private bool isGaming = false;
    private float cameraStartZoom;
    void Start () {
        startX = transform.position.x;
        startY = transform.position.y;
        startScale = transform.localScale.x;
        music.Play();
        music.volume = 0;
        musicTrue.Play();
        musicTrue.volume = 0;
        cameraStartZoom = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.transform.position.x > -0.9f && camera.transform.position.x < 0.9f && camera.transform.position.y < -1.85f) {
            attemptingToPlay = true;
            focus = Mathf.Min(1, focus + Time.deltaTime * 0.15f);
        } else if (camera.transform.position.y > -1.8f) {
            focus = Mathf.Max(-0.5f, focus - Time.deltaTime);
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
            float newScale = startScale + localProgress * 1.2f;
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }

        if (!isGaming && takeoutProgress >= takeoutProgressFinish - 0.04f) {
            isGaming = true;
            masterMusicVolume = 1;
            animator.Play("loop");
        } else if (isGaming && takeoutProgress < takeoutProgressFinish - 0.04f) {
            isGaming = false;
            animator.Play("off");
        }
        if (!attemptingToPlay) {
            masterMusicVolume = Mathf.Max(0, takeoutProgress * 2 - 1);
        }

        if (focus <= 0) {
            music.volume = masterMusicVolume;
            camera.orthographicSize = cameraStartZoom;
            Color newColor = new Color (0, 0, 0, 0);
            blackScreen.color = newColor;
        } else {
            music.volume = masterMusicVolume * (1-focus);
            musicTrue.volume = masterMusicVolume * focus;
            camera.orthographicSize = cameraStartZoom - focus;
            float targetCameraPosY = -4.5f;
            float distY = targetCameraPosY - camera.transform.position.y;
            float cameraFocusMult = focus * 0.003f;
            float cameraNewPosY = camera.transform.position.y + targetCameraPosY * cameraFocusMult;
            camera.transform.position = new Vector3(camera.transform.position.x, cameraNewPosY, camera.transform.position.z);
            Color newColor = new Color (0, 0, 0, focus * 0.6f);
            blackScreen.color = newColor;
        }

        controller.SetGaming(isGaming);
         
    }
}