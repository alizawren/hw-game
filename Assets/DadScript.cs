using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DadSprite").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
