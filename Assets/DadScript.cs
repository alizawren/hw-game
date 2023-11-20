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
        Debug.Log("Playing dad phrase");
        AudioSource source = GameObject.Find("DadPhrase").GetComponent<AudioSource>();
        Debug.Log(source.ToString());
        source.Play();
        GameObject.Find("DadSprite").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);

        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("DadSprite").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
    }
}
