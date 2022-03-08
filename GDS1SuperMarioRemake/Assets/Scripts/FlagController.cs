using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    //Properties
    public Vector2 flagBase = new Vector2(199.5f, 3.5f);
    public float descentSpeed = 3f;

    //Variables
    public bool flagLowering = false;

    //References
    public GameObject cinematicMarioPrefab;

    GameObject cinematicMario;

    //Engine-called
    void Start(){

    }

    void Update(){
        UpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            OnMarioCollision(other.gameObject);
        }
    }


    //Private methods
    void UpdatePosition(){
        if (flagLowering){
            if (transform.position.y > flagBase.y){
                transform.Translate(0f, descentSpeed * Time.deltaTime * -1f, 0f);
            } else {
                flagLowering = false;
            }
        }
    }

    void OnMarioCollision(GameObject mario){
        //Store Mario info
        string marioState = mario.GetComponent<MarioStateController>().GetStateAsString();
        Vector3 marioPosition = new Vector3(199.5f, mario.transform.position.y, 0f);
        mario.SetActive(false);

        //Spawn and set up cinematic clone
        cinematicMario = Instantiate(cinematicMarioPrefab, marioPosition, Quaternion.identity);
        cinematicMario.GetComponent<CinematicController>().marioState = marioState;

        //Manage the flag's animation
        GetComponent<BoxCollider2D>().enabled = false;
        flagLowering = true;
    }
}
