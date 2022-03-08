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
    public string marioState;

    bool atBottomOfPole = false;

    //References
    [HideInInspector]
    public GameObject endFlagReference;

    //Engine-called
    void Start(){

    }

    void Update(){
    }


    //Private methods

}
