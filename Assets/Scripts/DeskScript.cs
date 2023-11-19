using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskScript : MonoBehaviour
{
    private Vector3 positionCamera;
    public Camera camera;

    void Start () {
        // camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        positionCamera = camera.transform.position;
        float xPos = positionCamera.x * 0.05f;
        transform.position = new Vector3(1.25f - xPos, -1.3f, -1);
    }
}
