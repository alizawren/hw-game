using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomAnimate : MonoBehaviour
{
    Vector3 usualPosition = new Vector3(-2.43f, -0.15f, 0f);
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Transform>().position = usualPosition;
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpriteFromState(GameController.Mom momState)
    {
        string spritePath = "";
        Sprite newSprite;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        gameObject.GetComponent<Transform>().position = usualPosition;
        gameObject.GetComponent<Transform>().localScale = new Vector3(1.25f,1.25f,1f);
        switch (momState)
        {
            case GameController.Mom.AWAY: case GameController.Mom.OUTSIDE: case GameController.Mom.DAD:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            break;
            case GameController.Mom.INSIDE:
                spritePath = "Sprites/Mom/mom_inside";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
                Debug.Log("sprite: " + newSprite);

                source.clip = Resources.Load<AudioClip>("Sounds/better_not_be_playing_games");
                source.Play();
            break;
            case GameController.Mom.IN_CLOSET:
                Vector3 closetPosition = new Vector3(2.61f, 2.93f, 0f);
                gameObject.GetComponent<Transform>().position = closetPosition;

                spritePath = "Sprites/Mom/mom_in_closet";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            break;
            case GameController.Mom.WATCHING_CLOSELY:
                spritePath = "Sprites/Mom/mom_watching_closely";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            break;
            case GameController.Mom.YELLING:
                gameObject.GetComponent<Transform>().position = new Vector3(0f, -0.6f, -2f);
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 1f);

                spritePath = "Sprites/Mom/mom_yelling";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

                source.clip = Resources.Load<AudioClip>("Sounds/demon");
                source.Play();
            break;
            case GameController.Mom.UNDER_DESK:
                gameObject.GetComponent<Transform>().position = new Vector3(-0.11f, -0.48f, 0f);
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 1f);

                spritePath = "Sprites/Mom/mom_under_desk";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

                source.clip = Resources.Load<AudioClip>("Sounds/disappointed");
                source.Play();
            break;
        }
    }
}
