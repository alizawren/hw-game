using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
    private Vector3 positionCamera;
    new private Camera camera;
    private float zoomAmt;
    private float panAmt = 0.0085f;
    private float gameboyFocus = 0;

    void Start () {
        camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
        zoomAmt = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f == Time.timeScale) return;
        positionCamera = camera.transform.position;
        Vector3 mousePos = Input.mousePosition;
        float changeRatio = Mathf.Min(1.25f, Time.deltaTime / 0.00166f) * 0.01f + 0.002f;
        float goalX = (mousePos.x - 360) * panAmt;
        Debug.Log(gameboyFocus);
        float goalY = (mousePos.y - 320) * panAmt - 2.75f * gameboyFocus;
    
        float distX = goalX - positionCamera.x;
        float distY = goalY - positionCamera.y;
        float finalX = positionCamera.x + distX * changeRatio;
        float finalY = positionCamera.y + distY * changeRatio;
        finalX = Mathf.Max(-3, Mathf.Min(3, finalX));
        finalY = Mathf.Max(-4, Mathf.Min(3.1f, finalY));

        positionCamera = new Vector3(finalX, finalY, -10f);
        // Special case for gameboy

        if (positionCamera.x > -0.45f && positionCamera.x < 0.45f && positionCamera.y < -1.3f) {
            float gameboyPosX = 0f;
            float gameboyPosY = -2.2f;// - 3 * gameboyFocus;
            float distToGameboyX = gameboyPosX - positionCamera.x;
            float distToGameboyY = gameboyPosY - positionCamera.y;
            float distToGameboy = Mathf.Sqrt(distToGameboyX * distToGameboyX + distToGameboyY * distToGameboyY);
            float attractStrength = Mathf.Min(1.25f, Time.deltaTime / 0.00166f) * 0.03f / (distToGameboy + 5); // further away = weaker grasp
            attractStrength *= 1 + 1f * gameboyFocus;

            finalX = positionCamera.x + distToGameboyX * attractStrength;
            finalY = positionCamera.y + distToGameboyY * attractStrength * 2f;
            positionCamera = new Vector3(finalX, finalY, -10f);
        }
        if (gameboyFocus > 0) {
            // float targetCameraPosY = -4.5f;
            // float distY = targetCameraPosY - camera.transform.position.y;
            // float cameraFocusMult = focus * 0.003f;
            // float cameraNewPosY = camera.transform.position.y + targetCameraPosY * cameraFocusMult;
            // camera.transform.position = new Vector3(camera.transform.position.x, cameraNewPosY, camera.transform.position.z);
        }

        camera.transform.position = positionCamera;
    }

    public void UpdateGameboyFocus(float amt) {
        gameboyFocus = Mathf.Max(0, amt);
    }
}
