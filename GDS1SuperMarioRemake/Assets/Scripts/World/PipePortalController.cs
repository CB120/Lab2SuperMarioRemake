using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePortalController : MonoBehaviour
{
    //Properties


    //Variables
    bool isOverground = true;
    bool marioInTrigger = false;
    bool placeholderAnimating = false;


    //References
    GameObject marioReference;


    //Engine-called
    void Start(){
        marioReference = GameObject.FindGameObjectWithTag("Player");

        //determine if Overground > Underground portal or Underground > Overground portal
    }

    void Update(){
        CheckForActivation();
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
    void CheckForActivation(){
        if (Input.GetButtonDown("Down") && isOverground){
            OnPortalActivation();
        }
    }

    void OnPortalActivation(){
        //get mario's State
        //disable mario
        //Instantiate placeholder
        //set placeholder sprite
        placeholderAnimating = true;
    }
}
