using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
    private Vector3 positionCamera;
    new private Camera camera;
    private float zoomAmt;
    private float panAmt = 0.0085f;

    void Start () {
        camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
        zoomAmt = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f == Time.timeScale) return;
        Vector3 mousePos = Input.mousePosition;
        float changeRatio = Mathf.Min(1.25f, Time.deltaTime / 0.00166f) * 0.01f + 0.002f;
        float goalX = (mousePos.x - 360) * panAmt;
        float goalY = (mousePos.y - 320) * panAmt;
        
        float distX = goalX - positionCamera.x;
        float distY = goalY - positionCamera.y;
        float finalX = positionCamera.x + distX * changeRatio;
        float finalY = positionCamera.y + distY * changeRatio;
        finalX = Mathf.Max(-3, Mathf.Min(3, finalX));
        finalY = Mathf.Max(-4, Mathf.Min(3.1f, finalY));

        positionCamera = new Vector3(finalX, finalY, -10f);
        // Special case for gameboy

        if (positionCamera.x > -0.4f && positionCamera.x < 0.4f && positionCamera.y < -1.3f) {
            float gameboyPosX = 0f;
            float gameboyPosY = -2.2f;
            float distToGameboyX = gameboyPosX - positionCamera.x;
            float distToGameboyY = gameboyPosY - positionCamera.y;
            float distToGameboy = Mathf.Sqrt(distToGameboyX * distToGameboyX + distToGameboyY * distToGameboyY);
            float attractStrength = Mathf.Min(1.25f, Time.deltaTime / 0.00166f) * 0.03f / (distToGameboy + 5); // further away = weaker grasp

            finalX = positionCamera.x + distToGameboyX * attractStrength;
            finalY = positionCamera.y + distToGameboyY * attractStrength * 2f;
            positionCamera = new Vector3(finalX, finalY, -10f);
        }

        camera.transform.position = positionCamera;
    }
}
