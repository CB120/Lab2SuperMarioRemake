using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePortalController : MonoBehaviour
{
    //Properties
    public Vector2 undergroundSpawn;
    public Vector2 overgroundSpawn;
    public float dummyMovementSpeed = 3f;
    public float animationDuration = 1f;

    //Variables
    bool isOverground = true;
    bool marioInTrigger = false;
    bool dummyAnimating = false;
    string marioState = "small";


    //References
    public GameObject dummyMarioPrefab;
    public Sprite smallSprite;
    public Sprite largeSprite;
    public Sprite fireSprite;
    public Sprite largeFireSprite;

    GameObject marioReference;
    GameObject dummyMarioReference;


    //Engine-called
    void Start(){
        marioReference = GameObject.FindGameObjectWithTag("Player");
        isOverground = transform.position.x > 0;
    }

    void Update(){
        CheckForActivation();
        AnimateDummy();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            marioInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            marioInTrigger = false;
        }
    }


    //Private methods
        //Update
    void CheckForActivation(){
        if (!marioInTrigger) return;

        if (Input.GetAxis("Vertical") < 0 && isOverground){
            OnPortalActivation();
        } else if (Input.GetAxis("Horizontal") > 0 && !isOverground){
            OnPortalActivation();
        }
    }

    void OnPortalActivation(){
        //Manage Mario
        marioState = marioReference.GetComponent<MarioStateController>().GetStateAsString();
        marioReference.SetActive(false);

        //Instantiate placeholder
        dummyMarioReference = Instantiate(dummyMarioPrefab, transform.position, Quaternion.identity);
        dummyMarioReference.GetComponent<SpriteRenderer>().sprite = GetSpriteForState(marioState);

        //set dummy sprite
        dummyAnimating = true;
        Invoke("StopAnimation", animationDuration);
    }

    void AnimateDummy(){
        if (!dummyAnimating) return;

        if (isOverground){
            dummyMarioReference.transform.Translate(0f, dummyMovementSpeed * Time.deltaTime * -1f, 0f);
        } else {
            dummyMarioReference.transform.Translate(dummyMovementSpeed * Time.deltaTime, 0f, 0f);
        }
    }


        //Invoked
    void StopAnimation(){
        Debug.Log("StopAnimation() Invoked");
        dummyAnimating = false;

        if (isOverground){
            marioReference.transform.position = undergroundSpawn;
        } else {
            if (marioState == "large" || marioState == "largeFire"){
                marioReference.transform.position = overgroundSpawn + new Vector2(0f, 0.5f); //accounts for the extra height of Large Mario
            } else {
                marioReference.transform.position = overgroundSpawn;
            }
        }

        Destroy(dummyMarioReference);
        marioReference.SetActive(true);
    }


    //Private functions
    Sprite GetSpriteForState(string state){
        switch (state){
            case "large":
                return largeSprite;
            case "fire":
                return fireSprite;
            case "largeFire":
                return largeFireSprite;
            default:
                return smallSprite;
        }
    }
}
