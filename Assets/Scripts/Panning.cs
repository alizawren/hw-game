using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
    private Vector3 positionCamera;
    private Camera camera;

    void Start () {
        camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        float changeRatio = Mathf.Min(1.25f, Time.deltaTime / 0.00166f) * 0.01f + 0.002f;
        float goalX = (mousePos.x - 360) * 0.0035f;
        float goalY = (mousePos.y - 320) * 0.003f;
        
        float distX = goalX - positionCamera.x;
        float distY = goalY - positionCamera.y;
        float finalX = positionCamera.x + distX * changeRatio;
        float finalY = positionCamera.y + distY * changeRatio;
        positionCamera = new Vector3(finalX, finalY, -10f);
        camera.transform.position = positionCamera;
    }
}
