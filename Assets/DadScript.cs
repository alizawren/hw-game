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

    public void Appear()
    {
        AudioSource source = GameObject.Find("DadPhrase").GetComponent<AudioSource>();
        source.Play();
        GameObject.Find("DadSprite").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
    }

    public void Disappear()
    {
        GameObject.Find("DadSprite").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
    }
}
