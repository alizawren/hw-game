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
        positionCamera = new Vector3((mousePos.x - 360) * 0.0035f, (mousePos.y - 320) * 0.0035f, -10f);
        camera.transform.position = positionCamera;
    }
}
