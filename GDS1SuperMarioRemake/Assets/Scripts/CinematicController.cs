using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    //Properties
    public Vector2 flagBase = new Vector2(199.5f, 3.5f);
    public float descentSpeed = 3f;

    //Variables
    [HideInInspector]
    public string marioState = "small";

    bool atBottomOfPole = false;

    //References
    public Sprite smallClimbing;
    public Sprite largeClimbing;
    public Sprite fireClimbing;
    public Sprite largeFireClimbing;

    [HideInInspector]
    public GameObject endFlagReference;

    SpriteRenderer sprite;
    Animator animator;

    //Engine-called
    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        SetSprite();
    }

    void Update(){
        UpdatePosition();
    }


    //Private methods
        //Start
    void SetSprite(){
        switch (marioState){
            case "large":
                sprite.sprite = largeClimbing; break;
            case "fire":
                sprite.sprite = fireClimbing; break;
            case "largeFire":
                sprite.sprite = largeFireClimbing; break;
            default:
                sprite.sprite = smallClimbing; break;
        }
    }


        //Update
    void UpdatePosition(){
        if (!atBottomOfPole){
            if (transform.position.y > flagBase.y){
                transform.Translate(0f, descentSpeed * Time.deltaTime * -1f, 0f);
            } else {
                atBottomOfPole = true;
                //Play animation
            }
        }
    }
}
