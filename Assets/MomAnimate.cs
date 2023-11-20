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
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        switch (momState)
        {
            case GameController.Mom.AWAY: case GameController.Mom.OUTSIDE: case GameController.Mom.DAD:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            break;
            case GameController.Mom.INSIDE:
                string spritePath = "";
                Sprite newSprite = Resources.Load<Sprite>(spritePath);
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            break;
            case GameController.Mom.IN_CLOSET:
            break;
            case GameController.Mom.WATCHING_CLOSELY:
            break;
            case GameController.Mom.YELLING:
            break;
            case GameController.Mom.UNDER_DESK:
            break;
        }
    }
}
