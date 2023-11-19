using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskScript : MonoBehaviour
{
    private Vector3 positionCamera;
    new public Camera camera;

    void Start () {
        // camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        positionCamera = camera.transform.position;
        float xPos = positionCamera.x * 0.01f;
        transform.position = new Vector3(- xPos, -0.5f, -1);
    }
}
