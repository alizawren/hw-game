using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    private Color tmp;
    private float accelerateFade = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp = spriteRenderer.color;
        accelerateFade += Time.deltaTime * 0.004f;
        tmp.a = Mathf.Max(0, spriteRenderer.color.a - Time.deltaTime * 0.1f - accelerateFade);
        spriteRenderer.color = tmp;
        if (tmp.a <= 0) {
            Destroy(gameObject);
        }
    }
}
