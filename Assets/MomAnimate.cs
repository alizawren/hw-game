using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomAnimate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        switch (momState)
        {
            case GameController.Mom.AWAY: case GameController.Mom.OUTSIDE: case GameController.Mom.DAD:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            break;
            case GameController.Mom.INSIDE:
                spritePath = "Sprites/Mom/mom_enter";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
                Debug.Log("sprite: " + newSprite);
            break;
            case GameController.Mom.IN_CLOSET:
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
                spritePath = "Sprites/Mom/mom_yelling";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            break;
            case GameController.Mom.UNDER_DESK:
                spritePath = "Sprites/Mom/mom_under_desk";
                newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            break;
        }
    }
}
