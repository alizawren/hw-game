using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwingScript : MonoBehaviour
{
    private Vector3 RotateVector = new Vector3(0, -1f, 0);  // degrees per second to rotate in each axis. Set in inspector.

    private float RotateSpeed = -0.25f;
    private float RotateAmount = 0;
    private float OpenRotateAmount = -112;
    private float AjarAmount = -8;

    public float state = 0; // neutral
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayAction(2));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
        case 1:
            Open();
            break;
        case 2:
            Ajar();
            break;
        case 3:
            Close();
            break;
        default:
            // Do nothing
            break;
        }
    }

    void Open() {
        if (RotateAmount > OpenRotateAmount) {
            RotateAmount += RotateSpeed;
            transform.Rotate(RotateVector * RotateSpeed);
            if (RotateAmount < OpenRotateAmount) {
                float RotateDiff = OpenRotateAmount - RotateAmount;
                RotateAmount = OpenRotateAmount;
                transform.Rotate(RotateVector * RotateDiff);
            }
        }
    }

    void Ajar() {

    }

    void Close() {

    }
}
