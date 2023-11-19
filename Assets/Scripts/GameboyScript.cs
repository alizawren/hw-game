using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboyScript : MonoBehaviour
{
    private Vector3 positionCamera;
    public Camera camera;

    void Start () {
        camera = GetComponent<Camera>();
        positionCamera = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}