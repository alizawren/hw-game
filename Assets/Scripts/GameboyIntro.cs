using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboyIntro : MonoBehaviour
{
    new public Camera camera;
    public GameController controller;
    public GameObject player;
    // Start is called before the first frame update
    private Vector3 cameraPos;
    private string state = "";
    void Start()
    {
        cameraPos = camera.transform.position;
        StartCoroutine(DelayHide(1.35f));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Force camera position until this is gone
        camera.transform.position = cameraPos;
        if (state == "moving") {
            float newPosY = transform.position.y - Time.deltaTime * 5.5f;
            transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            if (newPosY < -8f) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DelayHide(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        state = "moving";
    }


    void OnDestroy() {
        player.SetActive(true);
    }
}
